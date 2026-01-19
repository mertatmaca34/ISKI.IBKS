using ISKI.IBKS.Application.Services.DataCollection;
using ISKI.IBKS.Application.Services.Mail;
using ISKI.IBKS.Application.Features.Plc.Abstractions;
using ISKI.IBKS.Domain.Entities;
using ISKI.IBKS.Infrastructure.IoT.Plc;
using ISKI.IBKS.Infrastructure.RemoteApi.SAIS.Abstractions;
using ISKI.IBKS.Infrastructure.RemoteApi.SAIS.Contracts.Data;
using ISKI.IBKS.Infrastructure.RemoteApi.SAIS.Contracts.SendData;
using ISKI.IBKS.Persistence.Contexts;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.DependencyInjection; // Added for IServiceScopeFactory and GetRequiredService
using ISKI.IBKS.Application.Features.StationSnapshots.Abstractions;
using ISKI.IBKS.Application.Features.Alarms; // Added

namespace ISKI.IBKS.Infrastructure.Services.DataCollection;

public sealed class DataCollectionService : IDataCollectionService
{
    private readonly IPlcClient _plcClient;
    private readonly IServiceScopeFactory _scopeFactory;
    private readonly ISaisApiClient _saisApiClient;
    private readonly IStationSnapshotCache _snapshotCache;
    private readonly ILogger<DataCollectionService> _logger;

    public DataCollectionService(
        IPlcClient plcClient,
        IServiceScopeFactory scopeFactory,
        ISaisApiClient saisApiClient,
        IStationSnapshotCache snapshotCache,
        ILogger<DataCollectionService> logger)
    {
        _plcClient = plcClient;
        _scopeFactory = scopeFactory;
        _saisApiClient = saisApiClient;
        _snapshotCache = snapshotCache;
        _logger = logger;
    }

    private const int MaxRetryAttempts = 3;
    private const int RetryDelayMs = 2000;
    private string _lastPlcIp = "10.33.3.253";
    private int _lastPlcRack = 0;
    private int _lastPlcSlot = 1;

    public async Task<PlcDataSnapshot?> ReadCurrentDataAsync(CancellationToken ct = default)
    {
        // 1. Bağlantı Kontrolü
        if (!_plcClient.IsConnected)
        {
            try 
            {
                 // Ayarlar veritabanından veya yapılandırmadan alınmalı
                 // Şimdilik default 10.33.3.253 (Dökümandan)
                 // TODO: Get from Configuration/Db
                _logger.LogInformation("PLC bağlantısı kuruluyor: {PlcIp}", _lastPlcIp);
                _plcClient.Connect(_lastPlcIp, _lastPlcRack, _lastPlcSlot);
                _logger.LogInformation("PLC bağlantısı başarıyla kuruldu");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "PLC Bağlantısı kurulamadı: {Message}. PLC IP adresi ve ağ bağlantısı kontrol edilmeli.", ex.Message);
                return null;
            }
            
            if (!_plcClient.IsConnected)
            {
                _logger.LogWarning("PLC bağlantısı kurulamadı - IsConnected=false");
                return null;
            }
        }

        Exception? lastException = null;

        for (int attempt = 1; attempt <= MaxRetryAttempts; attempt++)
        {
            try
            {
                return await ReadPlcDataInternalAsync(ct);
            }
            catch (Exception ex)
            {
                lastException = ex;
                _logger.LogWarning(ex, "PLC Veri Okuma Hatası (Deneme {Attempt}/{MaxAttempts}): {Message}", 
                    attempt, MaxRetryAttempts, ex.Message);

                if (attempt < MaxRetryAttempts)
                {
                    _logger.LogInformation("PLC bağlantısı yeniden kuruluyor...");
                    try
                    {
                        _plcClient.ForceReconnect(_lastPlcIp, _lastPlcRack, _lastPlcSlot);
                        _logger.LogInformation("PLC bağlantısı başarıyla yeniden kuruldu. {RetryDelayMs}ms sonra tekrar denenecek.", RetryDelayMs);
                    }
                    catch (Exception reconnectEx)
                    {
                        _logger.LogError(reconnectEx, "PLC yeniden bağlantı hatası");
                    }
                    
                    await Task.Delay(RetryDelayMs, ct);
                }
            }
        }

        _logger.LogError(lastException, "PLC Veri Okuma {MaxAttempts} deneme sonrasında başarısız oldu. PLC bağlantısı ve data blokları kontrol edilmeli.", MaxRetryAttempts);
        return null;
    }

    private async Task<PlcDataSnapshot?> ReadPlcDataInternalAsync(CancellationToken ct)
    {
        using var scope = _scopeFactory.CreateScope();
        var dbContext = scope.ServiceProvider.GetRequiredService<IbksDbContext>();
        
        var stationSettings = await dbContext.StationSettings.AsNoTracking().FirstOrDefaultAsync(ct);
        var snapshot = new PlcDataSnapshot 
        { 
            ReadTime = DateTime.Now,
            StationId = stationSettings?.StationId ?? Guid.Empty 
        };

        _logger.LogInformation("PLC'den veri blokları okunuyor (DB41, DB42, DB43)...");

        // 2. DB41 (Analog) Okuma - 168 Byte
        byte[] db41Buffer = new byte[168];
        db41Buffer = _plcClient.ReadBytes(41, 0, db41Buffer);
        PlcDataMapper.MapAnalogData(db41Buffer, snapshot);

        // 3. DB42 (Digital) Okuma - 3 Byte
        byte[] db42Buffer = new byte[3];
        db42Buffer = _plcClient.ReadBytes(42, 0, db42Buffer);
        PlcDataMapper.MapDigitalData(db42Buffer, snapshot);

        // 4. DB43 (System) Okuma - 19 Byte
        byte[] db43Buffer = new byte[19];
        db43Buffer = _plcClient.ReadBytes(43, 0, db43Buffer);
        PlcDataMapper.MapSystemData(db43Buffer, snapshot);

        _logger.LogInformation("PLC veri blokları başarıyla okundu ve eşleştirildi");
        return snapshot;
    }

    public async Task<SensorData?> SaveSensorDataAsync(PlcDataSnapshot data, CancellationToken ct = default)
    {
        try
        {
            using var scope = _scopeFactory.CreateScope();
            var dbContext = scope.ServiceProvider.GetRequiredService<IbksDbContext>();
            
            var stationSettings = await dbContext.StationSettings.FirstOrDefaultAsync(ct);
            if (stationSettings == null) return null;

            var sensorData = new SensorData(stationSettings.StationId, data.ReadTime);
            
            // Map Analog Values
            sensorData.SetAnalogValues(
                tesisDebi: data.TesisDebi, 
                tesisDebiStatus: 1,
                akisHizi: data.NumuneHiz, 
                akisHiziStatus: 1,
                ph: data.Ph, 
                phStatus: data.PhTetik ? 0 : 1,
                iletkenlik: data.Iletkenlik, 
                iletkenlikStatus: 1,
                cozunmusOksijen: data.CozunmusOksijen, 
                cozunmusOksijenStatus: 1,
                koi: data.Koi, 
                koiStatus: data.KoiTetik ? 0 : 1,
                akm: data.Akm, 
                akmStatus: data.AkmTetik ? 0 : 1,
                sicaklik: data.NumuneSicaklik, 
                sicaklikStatus: 1
            );

            // Map Optional Values
            sensorData.SetOptionalValues(
                desarjDebi: data.DesarjDebi, 
                desarjDebiStatus: 1,
                hariciDebi: data.HariciDebi, 
                hariciDebiStatus: 1,
                hariciDebi2: data.HariciDebi2,
                hariciDebi2Status: 1
            );
            
            dbContext.SensorDatas.Add(sensorData);
            await dbContext.SaveChangesAsync(ct);
            return sensorData;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Veri kaydedilemedi");
            return null;
        }
    }

    public async Task MarkDataAsSentAsync(Guid sensorDataId, CancellationToken ct = default)
    {
        try
        {
            using var scope = _scopeFactory.CreateScope();
            var dbContext = scope.ServiceProvider.GetRequiredService<IbksDbContext>();
            
            var entity = await dbContext.SensorDatas.FindAsync(new object[] { sensorDataId }, ct);
            if (entity != null)
            {
                entity.MarkAsSentToSais();
                await dbContext.SaveChangesAsync(ct);
            }
        }
        catch (Exception ex)
        {
             _logger.LogError(ex, "Data sent status update failed for {Id}", sensorDataId);
        }
    }

    /// <summary>
    /// SAIS API'ye sensör verilerini gönderir.
    /// SAIS Gereksinimleri:
    /// - Sadece 1 dakikalık periyot kabul edilir
    /// - 48 saat geçmiş veriler kabul edilmez
    /// - Değeri olan her parametre için mutlaka status bilgisi gönderilmelidir
    /// </summary>
    public async Task<bool> SendDataToSaisAsync(PlcDataSnapshot data, CancellationToken ct = default)
    {
        try
        {
            using var scope = _scopeFactory.CreateScope();
            var dbContext = scope.ServiceProvider.GetRequiredService<IbksDbContext>();

            var stationSettings = await dbContext.StationSettings.FirstOrDefaultAsync(ct);
            if (stationSettings == null)
            {
                _logger.LogError("SAIS'e veri gönderilemedi: StationSettings bulunamadı");
                return false;
            }

            // SAIS Validasyonu 1: 48 saat kontrolü
            var dataAge = DateTime.Now - data.ReadTime;
            if (dataAge.TotalHours > 48)
            {
                _logger.LogWarning("SAIS'e veri gönderilemedi: Veri 48 saatten eski (ReadTime: {ReadTime}, Age: {Age} saat)", 
                    data.ReadTime, dataAge.TotalHours);
                return false;
            }

            // SAIS Validasyonu 2: Period kontrolü (sadece 1 dakikalık periyot kabul edilir)
            const int period = 1;

            // SendDataRequest oluştur - Sadece ölçülen parametreleri gönder
            var request = new SendDataRequest
            {
                StationId = stationSettings.StationId,
                ReadTime = data.ReadTime,
                SoftwareVersion = "1.0.0", // TODO: Add SoftwareVersion to StationSettings entity
                Period = period,

                // Tesis Debi (Debi)
                Debi = data.TesisDebi,
                Debi_Status = 1, // 1 = Normal/Geçerli

                // Akış Hızı
                AkisHizi = data.NumuneHiz,
                AkisHizi_Status = 1,

                // pH
                pH = data.Ph,
                pH_Status = data.PhTetik ? 0 : 1, // 0 = Alarm/Hata, 1 = Normal

                // İletkenlik
                Iletkenlik = data.Iletkenlik,
                Iletkenlik_Status = 1,

                // Çözünmüş Oksijen
                CozunmusOksijen = data.CozunmusOksijen,
                CozunmusOksijen_Status = 1,

                // KOI (Kimyasal Oksijen İhtiyacı)
                KOi = data.Koi,
                KOi_Status = data.KoiTetik ? 0 : 1,

                // AKM (Askıda Katı Madde)
                AKM = data.Akm,
                AKM_Status = data.AkmTetik ? 0 : 1,

                // Sıcaklık
                Sicaklik = data.NumuneSicaklik,
                Sicaklik_Status = 1,

                // Opsiyonel Sensörler (sadece değer varsa gönder)
                DesarjDebi = data.DesarjDebi > 0 ? data.DesarjDebi : null,
                DesarjDebi_Status = data.DesarjDebi > 0 ? 1 : null,

                HariciDebi = data.HariciDebi > 0 ? data.HariciDebi : null,
                HariciDebi_Status = data.HariciDebi > 0 ? 1 : null,

                HariciDebi2 = data.HariciDebi2 > 0 ? data.HariciDebi2 : null,
                HariciDebi2_Status = data.HariciDebi2 > 0 ? 1 : null
            };

            _logger.LogInformation(
                "SAIS API'ye veri gönderiliyor - StationId: {StationId}, ReadTime: {ReadTime}, " +
                "pH: {Ph}, Debi: {Debi}, KOI: {Koi}, AKM: {Akm}, Sicaklik: {Sicaklik}",
                request.StationId, request.ReadTime, 
                request.pH, request.Debi, request.KOi, request.AKM, request.Sicaklik);

            var response = await _saisApiClient.SendDataAsync(request, ct);
            
            if (response.Result)
            {
                _logger.LogInformation("SAIS API başarılı yanıt döndü - Message: {Message}", response.Message);
                return true;
            }
            else
            {
                _logger.LogWarning("SAIS API başarısız yanıt döndü - Result: false, Message: {Message}", response.Message);
                return false;
            }
        }
        catch (HttpRequestException ex)
        {
            _logger.LogError(ex, "SAIS API HTTP hatası: {Message}. API URL'si ve ağ bağlantısı kontrol edilmeli.", ex.Message);
            return false;
        }
        catch (TaskCanceledException ex)
        {
            _logger.LogError(ex, "SAIS API zaman aşımı: {Message}. Ağ bağlantısı ve API sunucusu kontrol edilmeli.", ex.Message);
            return false;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "SAIS veri gönderimi başarısız: {Message}", ex.Message);
            return false;
        }
    }

    public async Task CheckAndTriggerAlarmsAsync(PlcDataSnapshot currentData, CancellationToken ct = default)
    {
        using var scope = _scopeFactory.CreateScope();
        
        // Use the new Alarm Manager Service
        var alarmManager = scope.ServiceProvider.GetRequiredService<IAlarmManager>();
        
        // Also need caching for "Sampling" logic? 
        // Wait, "Sampling Logic (Numune Alımı)... It should simply be treated as a 'Digital Alarm' defined in the database."
        // The requirements say Numune Alımı should be handled by the generic engine.
        // So I don't need to manually check triggers here anymore if they are defined in DB.
        // BUT, I do need to update the Cache (StationSnapshotCache) because UI/API might need it?
        // "DataCollectionService" still needs to update the cache for SAIS etc.
        // Let's keep the Cache Update part.
        
        // 1. Process Alarms via AlarmManager
        await alarmManager.ProcessAlarmsAsync(currentData, ct);

        // 2. Cache Update & SAIS
        // The old code also did Rising Edge checks for SAIS Diagnostics (CheckRisingEdge).
        // Requirement 5 says "Numune Alımı... handled by this generic engine".
        // This likely refers to the MAIL Notification part of Sampling Trigger (which was sending separate mails).
        // What about SAIS Diagnostics? These are strictly SAIS requirements. 
        // I should probably keep the SAIS diagnostics here OR move them to another handler. 
        // Since I'm refactoring "Alarm and Notification Service", SAIS is "Data Collection / Reporting".
        // I will keep the SAIS Diagnostic checks here for now to ensure we don't break SAIS, 
        // but REMOVE the mail sending parts (Digital Alarms checking).
        
        var dbContext = scope.ServiceProvider.GetRequiredService<IbksDbContext>();
        var stationSettings = await dbContext.StationSettings.AsNoTracking().FirstOrDefaultAsync(ct);
        if (stationSettings == null) return;
        
        var previousDto = await _snapshotCache.Get(stationSettings.StationId);
        
        if (previousDto != null)
        {
             // SAIS Diagnostik Gönderimi (Mevcut kod - Değişmedi)
            await CheckRisingEdge(previousDto.KabinBakimModu, currentData.KabinBakim, 2, stationSettings.StationId, ct);
            await CheckRisingEdge(previousDto.KabinOtoModu, currentData.KabinOto, 3, stationSettings.StationId, ct);
            await CheckRisingEdge(previousDto.KabinKalibrasyonModu, currentData.KabinKalibrasyon, 4, stationSettings.StationId, ct);
            await CheckRisingEdge(previousDto.KabinDumanAlarmi, currentData.KabinDuman, 5, stationSettings.StationId, ct); // SAIS için
            await CheckRisingEdge(previousDto.KabinSuBaskiniAlarmi, currentData.KabinSuBaskini, 6, stationSettings.StationId, ct); // SAIS için
            await CheckRisingEdge(previousDto.KabinKapiAlarmi, currentData.KabinKapiAcildi, 7, stationSettings.StationId, ct); // SAIS için
            await CheckRisingEdge(previousDto.KabinEnerjiAlarmi, currentData.KabinEnerjiYok, 8, stationSettings.StationId, ct); // SAIS için
        }

        // 3. Cache Update
        var newDto = new ISKI.IBKS.Application.Features.StationSnapshots.Dtos.StationSnapshotDto
        {
            SystemTime = currentData.SystemTime,
            TesisDebi = currentData.TesisDebi,
            TesisGunlukDebi = currentData.TesisGunlukDebi,
            Akm = currentData.Akm,
            Koi = currentData.Koi,
            Ph = currentData.Ph,
            Iletkenlik = currentData.Iletkenlik,
            CozunmusOksijen = currentData.CozunmusOksijen,
            KabinOtoModu = currentData.KabinOto,
            KabinBakimModu = currentData.KabinBakim,
            KabinKalibrasyonModu = currentData.KabinKalibrasyon,
            KabinDumanAlarmi = currentData.KabinDuman,
            KabinSuBaskiniAlarmi = currentData.KabinSuBaskini,
            KabinKapiAlarmi = currentData.KabinKapiAcildi,
            KabinEnerjiAlarmi = currentData.KabinEnerjiYok,
            KabinSaatlikYikamada = currentData.SaatlikYikamada,
            KabinHaftalikYikamada = currentData.HaftalikYikamada,
            Pompa1Calisiyor = currentData.Pompa1Calisiyor,
            Pompa2Calisiyor = currentData.Pompa2Calisiyor,
            Pompa3Calisiyor = currentData.Pompa3Calisiyor,
            AkmNumuneTetik = currentData.AkmTetik,
            KoiNumuneTetik = currentData.KoiTetik,
            PhNumuneTetik = currentData.PhTetik,
            ManuelTetik = currentData.ManuelTetik,
            SimNumuneTetik = currentData.SimNumuneTetik,
            
            // Yeni Eklenen Alanlar
            KabinSicakligi = currentData.KabinSicaklik,
            Pompa1TermikAlarmi = currentData.Pompa1Termik,
            Pompa2TermikAlarmi = currentData.Pompa2Termik,
            Pompa3TermikAlarmi = currentData.Pompa3Termik,
            YikamaTankiBosAlarmi = currentData.TankDolu, 
            KabinNemi = currentData.KabinNem,
            OlcumCihaziSuSicakligi = currentData.NumuneSicaklik,
            KabinAcilStopBasiliAlarmi = currentData.KabinAcilStopBasili
        };

        // Cache the new snapshot
        await _snapshotCache.Set(stationSettings.StationId, newDto);
    }
    
    // CheckAlarmRisingEdge and SendAlarmMail removed (Replaced by AlarmManager)
    
    private async Task CheckRisingEdge(bool? prev, bool current, int diagCode, Guid stationId, CancellationToken ct)
    {
        if (prev != true && current == true)
        {
            // Rising Edge -> Send Diagnostic
            try
            {
               var req = new ISKI.IBKS.Infrastructure.RemoteApi.SAIS.Contracts.Diagnostics.SendDiagnosticWithTypeRequest
               {
                   StationId = stationId,
                   DiagnosticTypeNo = diagCode.ToString(), // API string bekliyor olabilir veya SendDiagnosticRequest
                   DiagnosticDate = DateTime.Now
               };
               // Not: SendDiagnosticAsync ve SendDiagnosticWithTypeNoAsync var. 
               // Tip Numarası ile gönderim: SendDiagnosticWithTypeNoAsync
               await _saisApiClient.SendDiagnosticWithTypeNoAsync(req, ct);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Diagnostic {Code} gönderilemedi", diagCode);
            }
        }
    }

    public async Task<bool> StartSampleAsync(string sampleCode, CancellationToken ct = default)
    {
        try
        {
            if (!_plcClient.IsConnected)
            {
                 try { _plcClient.Connect("10.33.3.253", 0, 1); } catch { return false; }
            }

            _plcClient.WriteBit(42, 2, 5, true);
            _logger.LogInformation("StartSample PLC tetiklendi. SampleCode: {SampleCode}", sampleCode);

            using var scope = _scopeFactory.CreateScope();
            var dbContext = scope.ServiceProvider.GetRequiredService<IbksDbContext>();
            
            var stationSettings = await dbContext.StationSettings.AsNoTracking().FirstOrDefaultAsync(ct);
            if (stationSettings != null)
            {
                var request = new ISKI.IBKS.Infrastructure.RemoteApi.SAIS.Contracts.Sample.SampleRequestStartRequest
                {
                     StationId = stationSettings.StationId,
                     SampleCode = sampleCode,
                     StartDate = DateTime.Now
                };
                await _saisApiClient.SampleRequestStartAsync(request, ct);
            }

            return true;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "StartSample işlemi başarısız");
            return false;
        }
    }

    public async Task CheckPowerOffAsync(CancellationToken ct = default)
    {
        try 
        {
            using var scope = _scopeFactory.CreateScope();
            var dbContext = scope.ServiceProvider.GetRequiredService<IbksDbContext>();

            var stationSettings = await dbContext.StationSettings.AsNoTracking().FirstOrDefaultAsync(ct);
            if (stationSettings == null) return;

            // En son sensor verisi ne zaman?
            var lastData = await dbContext.SensorDatas.OrderByDescending(s => s.ReadTime).FirstOrDefaultAsync(ct);
            if (lastData == null) return; // Hiç veri yok

            var diff = DateTime.Now - lastData.ReadTime;
            // Eğer 15 dakikadan fazla fark varsa, bir kesinti oldu demektir.
            if (diff.TotalMinutes > 15)
            {
                var powerOff = new PowerOffTime(stationSettings.StationId, lastData.ReadTime);
                powerOff.SetEndDate(DateTime.Now);
                
                // DB'ye kaydet
                dbContext.PowerOffTimes.Add(powerOff);
                await dbContext.SaveChangesAsync(ct);

                // SAIS'e gönder
                var req = new ISKI.IBKS.Infrastructure.RemoteApi.SAIS.Contracts.Diagnostics.SendPowerOffTimeRequest
                {
                    StationId = stationSettings.StationId,
                    StartDate = powerOff.StartDate,
                    EndDate = powerOff.EndDate
                };
                await _saisApiClient.SendPowerOffTimeAsync(req, ct);
                
                _logger.LogInformation("Güç kesintisi tespit edildi ve bildirildi: {Duration} dk", diff.TotalMinutes);
            }
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "PowerOff kontrolü hatası");
        }
    }

    public async Task SaveAndSendCalibrationAsync(Domain.Entities.Calibration calibration, CancellationToken ct = default)
    {
        try
        {
            using var scope = _scopeFactory.CreateScope();
            var dbContext = scope.ServiceProvider.GetRequiredService<IbksDbContext>();
            
            var stationSettings = await dbContext.StationSettings.AsNoTracking().FirstOrDefaultAsync(ct);
            if (stationSettings == null) throw new Exception("İstasyon ayarları bulunamadı");
            
            // 1. DB'ye kaydet
            dbContext.Calibrations.Add(calibration);
            await dbContext.SaveChangesAsync(ct);

            // 2. SAIS'e bildir
            var req = new ISKI.IBKS.Infrastructure.RemoteApi.SAIS.Contracts.Calibration.SendCalibrationRequest
            {
                StationId = stationSettings.StationId,
                DBColumnName = calibration.DbColumnName,
                CalibrationDate = calibration.CalibrationDate,
                ZeroRef = calibration.ZeroRef,
                ZeroMeas = calibration.ZeroMeas,
                SpanRef = calibration.SpanRef,
                SpanMeas = calibration.SpanMeas,
                ZeroDiff = calibration.ZeroDiff,
                SpanDiff = calibration.SpanDiff,
                ZeroSTD = calibration.ZeroStd,
                SpanSTD = calibration.SpanStd,
                ResultFactor = calibration.ResultFactor,
                ResultZero = calibration.ResultZero,
                ResultSpan = calibration.ResultSpan,
                Result = calibration.Result
            };

            await _saisApiClient.SendCalibrationAsync(req, ct);
            _logger.LogInformation("Kalibrasyon sonucu kaydedildi ve SAIS'e gönderildi: {Sensor}", calibration.DbColumnName);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Kalibrasyon kaydı/gönderimi hatası");
            throw;
        }
    }

    public async Task<Guid> GetStationIdAsync(CancellationToken ct = default)
    {
        using var scope = _scopeFactory.CreateScope();
        var dbContext = scope.ServiceProvider.GetRequiredService<IbksDbContext>();
        
        var settings = await dbContext.StationSettings.AsNoTracking().FirstOrDefaultAsync(ct);
        return settings?.StationId ?? Guid.Empty;
    }

    public async Task<Domain.Entities.StationSettings?> GetStationSettingsAsync(CancellationToken ct = default)
    {
        using var scope = _scopeFactory.CreateScope();
        var dbContext = scope.ServiceProvider.GetRequiredService<IbksDbContext>();
        return await dbContext.StationSettings.AsNoTracking().FirstOrDefaultAsync(ct);
    }
}
