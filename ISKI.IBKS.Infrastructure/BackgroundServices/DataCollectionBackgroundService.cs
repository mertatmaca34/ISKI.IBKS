using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using ISKI.IBKS.Application.Services.DataCollection;

namespace ISKI.IBKS.Infrastructure.BackgroundServices;

/// <summary>
/// Dakikalık PLC veri toplama ve SAIS senkronizasyonu background servisi
/// </summary>
public sealed class DataCollectionBackgroundService : BackgroundService
{
    private readonly IServiceScopeFactory _scopeFactory;
    private readonly ILogger<DataCollectionBackgroundService> _logger;
    private readonly TimeSpan _dataCollectionInterval = TimeSpan.FromMinutes(1);
    private readonly TimeSpan _saisHealthCheckInterval = TimeSpan.FromMinutes(15);
    private DateTime _lastSaisHealthCheck = DateTime.MinValue;
    private bool _powerOffChecked = false;
    
    // Rate limiting for error logs (log only once per 5 minutes)
    private DateTime _lastPlcErrorLog = DateTime.MinValue;
    private DateTime _lastSaisErrorLog = DateTime.MinValue;
    private readonly TimeSpan _errorLogInterval = TimeSpan.FromMinutes(5);

    public DataCollectionBackgroundService(
        IServiceScopeFactory scopeFactory,
        ILogger<DataCollectionBackgroundService> logger)
    {
        _scopeFactory = scopeFactory;
        _logger = logger;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        _logger.LogInformation("Veri Toplama Servisi başlatıldı");

        while (!stoppingToken.IsCancellationRequested)
        {
            try
            {
                await CollectAndProcessDataAsync(stoppingToken);
                await CheckSaisHealthAsync(stoppingToken);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Veri toplama döngüsünde hata oluştu");
            }

            // Dakikalık veri toplama periyodu (bir sonraki tam dakikayı bekle)
            var now = DateTime.Now;
            var nextMinute = now.AddMinutes(1).AddSeconds(-now.Second).AddMilliseconds(-now.Millisecond);
            var delay = nextMinute - now;
            
            if (delay > TimeSpan.Zero)
            {
                await Task.Delay(delay, stoppingToken);
            }
        }

        _logger.LogInformation("Veri Toplama Servisi durduruldu");
    }

    private async Task CollectAndProcessDataAsync(CancellationToken ct)
    {
        using var scope = _scopeFactory.CreateScope();
        var dataCollectionService = scope.ServiceProvider.GetService<IDataCollectionService>();

        if (dataCollectionService == null)
        {
            _logger.LogWarning("IDataCollectionService bulunamadı");
            return;
        }

        // 1. Güç kesintisi kontrolü (Sadece servisin ilk açılışında bir kez)
        if (!_powerOffChecked)
        {
            _logger.LogInformation("Güç kesintisi kontrolü yapılıyor...");
            await dataCollectionService.CheckPowerOffAsync(ct);
            _powerOffChecked = true;
        }

        // 2. PLC'den veri oku
        _logger.LogInformation("PLC'den veri okunuyor...");
        var data = await dataCollectionService.ReadCurrentDataAsync(ct);
        if (data == null)
        {
            // Rate limited warning - only log every 5 minutes
            if (DateTime.Now - _lastPlcErrorLog > _errorLogInterval)
            {
                _logger.LogWarning("PLC'den veri okunamadı - PLC bağlantısı kontrol edilmeli");
                _lastPlcErrorLog = DateTime.Now;
            }
            return;
        }

        _logger.LogInformation("PLC verisi başarıyla okundu: pH={Ph:F2}, Debi={Debi:F2}, KOI={Koi:F2}, AKM={Akm:F2}", 
            data.Ph, data.TesisDebi, data.Koi, data.Akm);

        // 2. Veritabanına kaydet
        _logger.LogInformation("Veri veritabanına kaydediliyor...");
        var savedEntity = await dataCollectionService.SaveSensorDataAsync(data, ct);
        if (savedEntity == null)
        {
            _logger.LogError("Veri veritabanına kaydedilemedi - Veritabanı bağlantısı kontrol edilmeli");
        }
        else
        {
            _logger.LogInformation("Veri başarıyla veritabanına kaydedildi. ID: {Id}", savedEntity.Id);
        }

        // 3. SAIS API'ye gönder
        _logger.LogInformation("Veri SAIS API'ye gönderiliyor...");
        var sent = await dataCollectionService.SendDataToSaisAsync(data, ct);
        if (!sent)
        {
            // Rate limited warning - only log every 5 minutes
            if (DateTime.Now - _lastSaisErrorLog > _errorLogInterval)
            {
                _logger.LogWarning("Veri SAIS'e gönderilemedi - API ayarları ve bağlantı kontrol edilmeli");
                _lastSaisErrorLog = DateTime.Now;
            }
        }
        else if (savedEntity != null)
        {
             // Başarılı gönderildi, DB'de işaretle
             await dataCollectionService.MarkDataAsSentAsync(savedEntity.Id, ct);
             _logger.LogInformation("Veri SAIS'e başarıyla gönderildi ve işaretlendi. ID: {Id}", savedEntity.Id);
        }

        // 4. Alarmları kontrol et
        _logger.LogInformation("Alarmlar kontrol ediliyor...");
        await dataCollectionService.CheckAndTriggerAlarmsAsync(data, ct);
        _logger.LogInformation("Alarm kontrolü tamamlandı");
    }

    private async Task CheckSaisHealthAsync(CancellationToken ct)
    {
        if (DateTime.Now - _lastSaisHealthCheck < _saisHealthCheckInterval)
        {
            return;
        }

        _lastSaisHealthCheck = DateTime.Now;

        try
        {
            // SAIS sunucu saati kontrolü ve senkronizasyon
            _logger.LogDebug("SAIS sağlık kontrolü yapılıyor...");
            // Bu kısım ISaisApiClient.GetServerDateTimeAsync() ile yapılacak
        }
        catch (Exception ex)
        {
            _logger.LogWarning(ex, "SAIS sağlık kontrolü başarısız");
        }
    }
}
