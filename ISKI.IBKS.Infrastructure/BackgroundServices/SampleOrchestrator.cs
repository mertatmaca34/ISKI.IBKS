using ISKI.IBKS.Application.Common.Configuration;
using ISKI.IBKS.Application.Common.IoT.Plc;
using ISKI.IBKS.Application.Common.IoT.Snapshots;
using ISKI.IBKS.Domain.Entities;
using ISKI.IBKS.Domain.Enums;
using ISKI.IBKS.Domain.IoT;
using Microsoft.EntityFrameworkCore;
using ISKI.IBKS.Application.Common.RemoteApi.SAIS;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using ISKI.IBKS.Infrastructure.Persistence.Contexts;

namespace ISKI.IBKS.Infrastructure.BackgroundServices;

public sealed class SampleOrchestrator : BackgroundService
{
    private readonly IServiceScopeFactory _scopeFactory;
    private readonly ISaisApiClient _saisApiClient;
    private readonly IStationSnapshotCache _snapshotCache;
    private readonly IPlcClient _plcClient;
    private readonly IStationConfiguration _stationConfig;
    private readonly ILogger<SampleOrchestrator> _logger;
    private readonly TimeSpan _checkInterval = TimeSpan.FromSeconds(30);

    public SampleOrchestrator(
        IServiceScopeFactory scopeFactory,
        ISaisApiClient saisApiClient,
        IStationSnapshotCache snapshotCache,
        IPlcClient plcClient,
        IStationConfiguration stationConfig,
        ILogger<SampleOrchestrator> logger)
    {
        _scopeFactory = scopeFactory;
        _saisApiClient = saisApiClient;
        _snapshotCache = snapshotCache;
        _plcClient = plcClient;
        _stationConfig = stationConfig;
        _logger = logger;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        _logger.LogInformation("SampleOrchestrator started.");

        while (!stoppingToken.IsCancellationRequested)
        {
            try
            {
                await ProcessPendingRequestsAsync(stoppingToken);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in SampleOrchestrator loop.");
            }

            await Task.Delay(_checkInterval, stoppingToken);
        }
    }

    private async Task ProcessPendingRequestsAsync(CancellationToken ct)
    {
        using var scope = _scopeFactory.CreateScope();
        var dbContext = scope.ServiceProvider.GetRequiredService<IbksDbContext>();

        var stationId = _stationConfig.StationId;
        if (stationId == Guid.Empty) return;

        var pendingRequests = await dbContext.SampleRequests
            .Where(r => r.Status == SampleStatus.Pending)
            .ToListAsync(ct);

        if (!pendingRequests.Any()) return;

        var snapshot = await _snapshotCache.Get(stationId);
        bool isCabinReady = IsCabinReady(snapshot);

        foreach (var request in pendingRequests)
        {
            var age = DateTime.Now - request.StartedAt;

            if (age.TotalHours >= 2)
            {
                await HandleTimeoutAsync(request, dbContext, stationId, ct);
                continue;
            }

            if (isCabinReady)
            {
                await TryStartSampleAsync(request, dbContext, stationId, ct);
            }
        }

        await dbContext.SaveChangesAsync(ct);
    }

    private bool IsCabinReady(PlcDataSnapshot? snapshot)
    {
        return snapshot != null &&
               snapshot.KabinBakimModu != true &&
               snapshot.KabinKalibrasyonModu != true &&
               snapshot.KabinSaatlikYikamada != true &&
               snapshot.KabinHaftalikYikamada != true;
    }

    private async Task HandleTimeoutAsync(SampleRequest request, IbksDbContext dbContext, Guid stationId, CancellationToken ct)
    {
        _logger.LogWarning("Sample request timed out (2h). Code: {SampleCode}", request.SampleCode);
        request.MarkAsFailed("Timed out after 2 hours (Cabin busy or system error).");

        await _saisApiClient.SampleRequestErrorAsync(new SampleErrorRequest
        {
            StationId = stationId,
            SampleCode = request.SampleCode,
            ErrorCode = "TIMEOUT_2H",
            ErrorMessage = "Sampling could not be started within 2 hours."
        }, ct);
    }

    private async Task TryStartSampleAsync(SampleRequest request, IbksDbContext dbContext, Guid stationId, CancellationToken ct)
    {
        _logger.LogInformation("Starting sample. Code: {SampleCode}", request.SampleCode);

        try
        {
            if (!_plcClient.IsConnected)
            {
                await _plcClient.ConnectAsync(_stationConfig.PlcIp, _stationConfig.PlcRack, _stationConfig.PlcSlot, ct);
            }

            if (_plcClient.IsConnected)
            {
                await _plcClient.WriteBitAsync(42, 2, 5, true, ct);

                request.SetSampleCode(request.SampleCode);

                await _saisApiClient.SampleRequestStartAsync(new SampleRequestStartRequest
                {
                    StationId = stationId,
                    SampleCode = request.SampleCode,
                    StartDate = DateTime.Now
                }, ct);

                _logger.LogInformation("Sample started and SAIS notified. Code: {SampleCode}", request.SampleCode);
            }
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "PLC error while starting sample. Code: {SampleCode}", request.SampleCode);
        }
    }
}
