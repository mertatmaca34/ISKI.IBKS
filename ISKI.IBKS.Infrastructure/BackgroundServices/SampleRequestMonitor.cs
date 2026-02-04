using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using ISKI.IBKS.Application.Features.StationSnapshots.Abstractions;
using ISKI.IBKS.Application.Features.Plc.Abstractions;
using ISKI.IBKS.Domain.Entities;
using ISKI.IBKS.Persistence.Contexts;
using Microsoft.EntityFrameworkCore;
using ISKI.IBKS.Infrastructure.RemoteApi.SAIS.Abstractions;
using ISKI.IBKS.Infrastructure.RemoteApi.SAIS.Contracts.Sample;

namespace ISKI.IBKS.Infrastructure.BackgroundServices;

/// <summary>
/// Numune taleplerini takip eden ve 2 saatlik zaman aşımını yöneten servis. (Madde 3.10.10)
/// </summary>
public sealed class SampleRequestMonitor : BackgroundService
{
    private readonly IServiceScopeFactory _scopeFactory;
    private readonly ISaisApiClient _saisApiClient;
    private readonly IStationSnapshotCache _snapshotCache;
    private readonly IPlcClient _plcClient;
    private readonly ILogger<SampleRequestMonitor> _logger;
    private readonly TimeSpan _checkInterval = TimeSpan.FromSeconds(30);

    public SampleRequestMonitor(
        IServiceScopeFactory scopeFactory,
        ISaisApiClient saisApiClient,
        IStationSnapshotCache snapshotCache,
        IPlcClient plcClient,
        ILogger<SampleRequestMonitor> logger)
    {
        _scopeFactory = scopeFactory;
        _saisApiClient = saisApiClient;
        _snapshotCache = snapshotCache;
        _plcClient = plcClient;
        _logger = logger;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        _logger.LogInformation("Numune Takip Servisi başlatıldı");

        while (!stoppingToken.IsCancellationRequested)
        {
            try
            {
                await ProcessPendingRequestsAsync(stoppingToken);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Numune takip döngüsünde hata oluştu");
            }

            await Task.Delay(_checkInterval, stoppingToken);
        }
    }

    private async Task ProcessPendingRequestsAsync(CancellationToken ct)
    {
        using var scope = _scopeFactory.CreateScope();
        var dbContext = scope.ServiceProvider.GetRequiredService<IbksDbContext>();
        
        var stationId = await dbContext.StationSettings
            .AsNoTracking()
            .Select(s => s.StationId)
            .FirstOrDefaultAsync(ct);

        if (stationId == Guid.Empty) return;

        // 1. Bekleyen talepleri getir
        var pendingRequests = await dbContext.SampleRequests
            .Where(r => r.Status == SampleStatus.Pending)
            .ToListAsync(ct);

        if (!pendingRequests.Any()) return;

        // 2. Kabin durumunu kontrol et (Yıkama/Bakım vb.)
        var snapshot = await _snapshotCache.Get(stationId);
        bool isCabinReady = snapshot != null && 
                           snapshot.KabinBakimModu != true && 
                           snapshot.KabinKalibrasyonModu != true && 
                           snapshot.KabinSaatlikYikamada != true && 
                           snapshot.KabinHaftalikYikamada != true;

        foreach (var request in pendingRequests)
        {
            var age = DateTime.UtcNow - request.StartedAt;

            // A. 2 Saatlik Zaman Aşımı Kontrolü (Madde 3.10.10)
            if (age.TotalHours >= 2)
            {
                _logger.LogWarning("Numune talebi 2 saatlik süreyi aştı ve iptal ediliyor. Code: {SampleCode}", request.SampleCode);
                
                request.MarkAsFailed("2 saat içinde başlatılamadı (Kabin meşgul/hata).");
                
                await _saisApiClient.SampleRequestErrorAsync(new SampleErrorRequest
                {
                    StationId = stationId,
                    SampleCode = request.SampleCode,
                    ErrorCode = "TIMEOUT_2H",
                    ErrorMessage = "Numune talebi 2 saat içinde başlatılamadığı için zaman aşımına uğradı."
                }, ct);
                
                continue;
            }

            // B. İşlemi Başlat (Kabin müsaitse)
            if (isCabinReady)
            {
                _logger.LogInformation("Kabin müsait, numune işlemi başlatılıyor. Code: {SampleCode}", request.SampleCode);

                try
                {
                    // PLC Tetikle
                    if (!_plcClient.IsConnected)
                    {
                        var settings = await dbContext.StationSettings.AsNoTracking().FirstOrDefaultAsync(ct);
                        if (settings != null) _plcClient.Connect(settings.PlcIpAddress, settings.PlcRack, settings.PlcSlot);
                    }

                    if (_plcClient.IsConnected)
                    {
                        // DB42.DBX2.5 (Önceki implementasyondan alınan adres)
                        _plcClient.WriteBit(42, 2, 5, true); 

                        // Başladı olarak işaretle
                        request.SetSampleCode(request.SampleCode); // Sets Status to Started
                        
                        // SAIS'e bildir
                        await _saisApiClient.SampleRequestStartAsync(new ISKI.IBKS.Infrastructure.RemoteApi.SAIS.Contracts.Sample.SampleRequestStartRequest
                        {
                            StationId = stationId,
                            SampleCode = request.SampleCode,
                            StartDate = DateTime.Now
                        }, ct);

                        _logger.LogInformation("Numune başlatıldı ve SAIS'e bildirildi. Code: {SampleCode}", request.SampleCode);
                    }
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "Numune başlatılırken PLC hatası oluştu. Code: {SampleCode}", request.SampleCode);
                }
            }
        }

        await dbContext.SaveChangesAsync(ct);
    }
}
