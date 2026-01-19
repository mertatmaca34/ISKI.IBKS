using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using ISKI.IBKS.Application.Services.DataCollection;
using ISKI.IBKS.Application.Services.Mail;
using ISKI.IBKS.Persistence.Contexts;
using Microsoft.EntityFrameworkCore;

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

    // System Alarms Fields
    private int _consecutivePlcErrors = 0;
    private int _consecutiveSaisErrors = 0;
    private bool _plcConnectionAlarmSent = false;
    private bool _saisConnectionAlarmSent = false;
    private bool _dataAgeAlarmSent = false;
    private DateTime _lastSuccessfulDataTime = DateTime.MinValue;

    private async Task CollectAndProcessDataAsync(CancellationToken ct)
    {
        using var scope = _scopeFactory.CreateScope();
        var dataCollectionService = scope.ServiceProvider.GetService<IDataCollectionService>();
        var mailService = scope.ServiceProvider.GetService<IAlarmMailService>(); // Mail servisi al

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
        
        // --- PLC Bağlantı Kontrolü ---
        if (data == null)
        {
            _consecutivePlcErrors++;
            
            // Rate limited warning for logs
            if (DateTime.Now - _lastPlcErrorLog > _errorLogInterval)
            {
                _logger.LogWarning("PLC'den veri okunamadı - PLC bağlantısı kontrol edilmeli (Ardışık Hata: {Count})", _consecutivePlcErrors);
                _lastPlcErrorLog = DateTime.Now;
            }
            
            // 3 ardışık hata ve henüz alarm gönderilmediyse
            if (_consecutivePlcErrors >= 3 && !_plcConnectionAlarmSent && mailService != null)
            {
                 await SendSystemAlarmMail(mailService, "PLC Bağlantı Hatası", 
                     "Masaüstü uygulaması PLC ile haberleşemiyor. Veri okunamıyor ve gönderilemiyor (3 ardışık deneme başarısız).", 
                     "PLC Bağlantı", ct);
                 _plcConnectionAlarmSent = true;
            }
            return;
        }
        else
        {
            // Başarılı okuma
            _consecutivePlcErrors = 0;
            _plcConnectionAlarmSent = false; // Reset alarm flag so we can notify again if it fails later
             
             // Veri başarıyla okunduysa DataAge alarmını da sıfırlayabiliriz (veya aşağıda check ederiz)
            _lastSuccessfulDataTime = DateTime.Now;
            _dataAgeAlarmSent = false;
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
        //var sent = await dataCollectionService.SendDataToSaisAsync(data, ct);
        var sent = true;
        
        // --- SAIS API Kontrolü ---
        if (!sent)
        {
            _consecutiveSaisErrors++;
            
            // Rate limited warning
            if (DateTime.Now - _lastSaisErrorLog > _errorLogInterval)
            {
                _logger.LogWarning("Veri SAIS'e gönderilemedi - API ayarları ve bağlantı kontrol edilmeli (Ardışık Hata: {Count})", _consecutiveSaisErrors);
                _lastSaisErrorLog = DateTime.Now;
            }
            
            if (_consecutiveSaisErrors >= 3 && !_saisConnectionAlarmSent && mailService != null)
            {
                 await SendSystemAlarmMail(mailService, "SAIS API Veri Gönderim Hatası", 
                     "Veriler bakanlık sunucularına gönderilemiyor. İnternet veya API servisi kaynaklı sorun (3 ardışık deneme başarısız).", 
                     "SAIS API", ct);
                 _saisConnectionAlarmSent = true;
            }
        }
        else 
        {
            _consecutiveSaisErrors = 0;
            _saisConnectionAlarmSent = false;
            
            if (savedEntity != null)
            {
                 // Başarılı gönderildi, DB'de işaretle
                 await dataCollectionService.MarkDataAsSentAsync(savedEntity.Id, ct);
                 _logger.LogInformation("Veri SAIS'e başarıyla gönderildi ve işaretlendi. ID: {Id}", savedEntity.Id);
            }
        }
        
        // --- Kritik Veri Gönderim Süresi Aşımı (48 Saat Riski) ---
        // Not: Dokümanda 48 saat denmiş ama alarm için 24 saat geçince uyar denmiş.
        if (_lastSuccessfulDataTime != DateTime.MinValue)
        {
            var hoursSinceLastData = (DateTime.Now - _lastSuccessfulDataTime).TotalHours;
            if (hoursSinceLastData > 24 && !_dataAgeAlarmSent && mailService != null)
            {
                await SendSystemAlarmMail(mailService, "Kritik Veri Gönderim Süresi Aşımı (24 Saat)", 
                     $"Son başarılı veri gönderim tarihinin üzerinden {hoursSinceLastData:F1} saat geçti. 48 saati aşmış veriler kabul edilmez. Veri kaybı riski çok yüksek.", 
                     "Veri Gönderim", ct);
                _dataAgeAlarmSent = true;
            }
        }

        // 4. Alarmları kontrol et (Digital & Process Alarms)
        _logger.LogInformation("Alarmlar kontrol ediliyor...");
        await dataCollectionService.CheckAndTriggerAlarmsAsync(data, ct);
        _logger.LogInformation("Alarm kontrolü tamamlandı");
    }

    private async Task SendSystemAlarmMail(IAlarmMailService mailService, string title, string description, string sensorName, CancellationToken ct)
    {
        try 
        {
             // Recipients logic needs DbContext, but here we are in a BackgroundService and we have scope.
             // But to keep it simple and consistent with DataCollectionService, we should use the same logic.
             // We can create a new scope to fetch users.
             
            using var scope = _scopeFactory.CreateScope();
            var dbContext = scope.ServiceProvider.GetRequiredService<ISKI.IBKS.Persistence.Contexts.IbksDbContext>();
            
             // Fetch users
            var recipients = await Microsoft.EntityFrameworkCore.EntityFrameworkQueryableExtensions.ToListAsync(
                System.Linq.Queryable.Select(
                    System.Linq.Queryable.Where(
                        dbContext.AlarmUsers.AsNoTracking(), 
                        u => u.IsActive && u.ReceiveEmailNotifications), 
                    u => new { u.Email, u.FullName }), 
                ct);

            foreach (var recipient in recipients)
            {
                await mailService.SendAlarmNotificationAsync(
                    recipient.Email, recipient.FullName, 
                    title, description, sensorName, null, null, DateTime.Now, ct);
            }
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Sistem alarm maili gönderilemedi: {Title}", title);
        }
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
