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
                _logger.LogInformation("PLC bağlantısı kuruluyor: 10.33.3.253");
                _plcClient.Connect("10.33.3.253", 0, 1);
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

        try
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
        catch (Exception ex)
        {
            _logger.LogError(ex, "PLC Veri Okuma Hatası: {Message}. PLC bağlantısı ve data blokları kontrol edilmeli.", ex.Message);
            return null;
        }
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

            // SendDataRequest oluştur
            var request = new SendDataRequest
            {
                StationId = stationSettings.StationId,
                ReadTime = data.ReadTime,
                SoftwareVersion = "1.0.0",
                Debi = data.TesisDebi,
                Ph = data.Ph,
                PhStatus = data.PhTetik ? 0 : 1,
                Iletkenlik = data.Iletkenlik,
                IletkenlikStatus = "1",
                Period = 1
            };

            _logger.LogInformation("SAIS API'ye veri gönderiliyor: StationId={StationId}, ReadTime={ReadTime}", 
                request.StationId, request.ReadTime);

            var response = await _saisApiClient.SendDataAsync(request, ct);
            
            if (response.Result)
            {
                _logger.LogInformation("SAIS API başarılı yanıt döndü");
            }
            else
            {
                _logger.LogWarning("SAIS API başarısız yanıt döndü: Result=false");
            }
            
            return response.Result;
        }
        catch (HttpRequestException ex)
        {
            _logger.LogError(ex, "SAIS API HTTP hatası: {Message}. API URL'si kontrol edilmeli.", ex.Message);
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
        var dbContext = scope.ServiceProvider.GetRequiredService<IbksDbContext>();
        // mailService is also scoped
        // var mailService = scope.ServiceProvider.GetRequiredService<IAlarmMailService>();

        var stationSettings = await dbContext.StationSettings.AsNoTracking().FirstOrDefaultAsync(ct);
        if (stationSettings == null) return;

        // 1. Threshold Alarmları (AlarmDefinition)
        var alarms = await dbContext.AlarmDefinitions.Where(a => a.IsActive).ToListAsync(ct);
        foreach (var alarm in alarms)
        {
            // Eşik değer kontrolü
            bool triggered = false;
            double currentValue = 0;

            if (alarm.SensorName == "Ph") currentValue = currentData.Ph;
            else if (alarm.SensorName == "Akm") currentValue = currentData.Akm;
            else if (alarm.SensorName == "Koi") currentValue = currentData.Koi;
            // ... diğerleri

            if (alarm.Type == AlarmType.Threshold)
            {
                if ((alarm.MinThreshold.HasValue && currentValue < alarm.MinThreshold.Value) ||
                    (alarm.MaxThreshold.HasValue && currentValue > alarm.MaxThreshold.Value))
                {
                    triggered = true;
                }
            }

            if (triggered)
            {
                 // Mail gönder
                 // await mailService.SendAlarmMailAsync(...)
                 
                 // SAIS Diagnostic? Eğer mapping varsa.
            }
        }
        
        // 2. System Diagnostics (Rising Edge Detection) ve Cache Update
        var previousDto = await _snapshotCache.Get(stationSettings.StationId);
        
        // Diagnostik Kodları (Doküman Özeti)
        // 2: Bakım, 3: Oto, 4: Kalibrasyon
        // Diğerleri için varsayım: 5: Duman, 6: SuBaskini, 7: Kapi, 8: EnerjiYok
        
        if (previousDto != null)
        {
             await CheckRisingEdge(previousDto.KabinBakimModu, currentData.KabinBakim, 2, stationSettings.StationId, ct);
             await CheckRisingEdge(previousDto.KabinOtoModu, currentData.KabinOto, 3, stationSettings.StationId, ct);
             await CheckRisingEdge(previousDto.KabinKalibrasyonModu, currentData.KabinKalibrasyon, 4, stationSettings.StationId, ct);
             await CheckRisingEdge(previousDto.KabinDumanAlarmi, currentData.KabinDuman, 5, stationSettings.StationId, ct);
             await CheckRisingEdge(previousDto.KabinSuBaskiniAlarmi, currentData.KabinSuBaskini, 6, stationSettings.StationId, ct);
             await CheckRisingEdge(previousDto.KabinKapiAlarmi, currentData.KabinKapiAcildi, 7, stationSettings.StationId, ct);
             await CheckRisingEdge(previousDto.KabinEnerjiAlarmi, currentData.KabinEnerjiYok, 8, stationSettings.StationId, ct);
             // ... Diğerleri
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
            SimNumuneTetik = currentData.SimNumuneTetik
        };

        await _snapshotCache.Set(stationSettings.StationId, newDto);
    }
    
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
