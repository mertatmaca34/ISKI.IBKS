using ISKI.IBKS.Application.Features.StationSnapshots.Abstractions;
using ISKI.IBKS.Application.Services.DataCollection; // NEW
using ISKI.IBKS.Domain.Entities;
using ISKI.IBKS.Infrastructure.IoT.Plc.Configuration;
using ISKI.IBKS.Persistence.Contexts;
using ISKI.IBKS.WebAPI.Contracts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace ISKI.IBKS.WebAPI.Controllers;

/// <summary>
/// Controller exposing endpoints for SAIS to consume.
/// All endpoints require Basic Authentication.
/// </summary>
[ApiController]
[Authorize]
public class SaisController : ControllerBase
{
    private readonly IbksDbContext _dbContext;
    private readonly IStationSnapshotCache _snapshotCache;
    private readonly IDataCollectionService _dataService; // NEW
    private readonly PlcSettings _plcSettings;
    private readonly ILogger<SaisController> _logger;

    public SaisController(
        IbksDbContext dbContext,
        IStationSnapshotCache snapshotCache,
        IDataCollectionService dataService, // NEW
        IOptions<PlcSettings> plcSettings,
        ILogger<SaisController> logger)
    {
        _dbContext = dbContext;
        _snapshotCache = snapshotCache;
        _dataService = dataService;
        _plcSettings = plcSettings.Value;
        _logger = logger;
    }

    /// <summary>
    /// PLC saatini sorgulayıp döndürür. (3.10.1)
    /// </summary>
    [HttpGet("GetServerDateTime")]
    public async Task<ActionResult<SaisApiResponse<string>>> GetServerDateTime([FromQuery] Guid stationId)
    {
        try
        {
            var snapshot = await _snapshotCache.Get(stationId);
            var dateTime = snapshot?.SystemTime ?? DateTime.Now;
            return Ok(SaisApiResponse<string>.Success(dateTime.ToString("yyyy-MM-ddTHH:mm:ss")));
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "GetServerDateTime hatası");
            return Ok(SaisApiResponse<string>.Failure(ex.Message));
        }
    }

    /// <summary>
    /// Verilen iki tarih arasındaki verileri döndürür. (3.10.2)
    /// </summary>
    [HttpGet("GetData")]
    public async Task<ActionResult<SaisApiResponse<List<object>>>> GetData(
        [FromQuery] Guid stationId,
        [FromQuery] string startDate,
        [FromQuery] string endDate,
        [FromQuery] int period = 1)
    {
        try
        {
            if (!DateTime.TryParse(startDate, out var start) || !DateTime.TryParse(endDate, out var end))
                return Ok(SaisApiResponse<List<object>>.Failure("Geçersiz tarih formatı. Beklenen: yyyy-MM-dd HH:mm:ss"));

            var stationSettings = await _dbContext.StationSettings.AsNoTracking().FirstOrDefaultAsync(s => s.StationId == stationId);
            var selectedSensors = stationSettings?.GetSelectedSensors() ?? new List<string>();

            var measurements = await _dbContext.SensorDatas
                .AsNoTracking()
                .Where(s => s.StationId == stationId && s.ReadTime >= start && s.ReadTime <= end)
                .OrderBy(s => s.ReadTime)
                .ToListAsync();

            var dataList = measurements.Select(m => MapDynamicSensorData(m, selectedSensors, period)).ToList();
            return Ok(SaisApiResponse<List<object>>.Success(dataList));
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "GetData hatası");
            return Ok(SaisApiResponse<List<object>>.Failure(ex.Message));
        }
    }

    /// <summary>
    /// Anlık olarak okunan değerleri döndürür. (3.10.3)
    /// </summary>
    [HttpGet("GetInstantData")]
    public async Task<ActionResult<SaisApiResponse<object>>> GetInstantData([FromQuery] Guid stationId)
    {
        try
        {
            var stationSettings = await _dbContext.StationSettings.AsNoTracking().FirstOrDefaultAsync(s => s.StationId == stationId);
            if (stationSettings == null) return Ok(SaisApiResponse<object>.Failure("İstasyon bulunamadı"));

            var selectedSensors = stationSettings.GetSelectedSensors();
            var snapshot = await _snapshotCache.Get(stationId);
            
            if (snapshot == null)
            {
                var plcData = await _dataService.ReadCurrentDataAsync();
                if (plcData == null) return Ok(SaisApiResponse<object>.Failure("PLC bağlantısı yok"));
                
                snapshot = new ISKI.IBKS.Application.Features.StationSnapshots.Dtos.StationSnapshotDto
                {
                    SystemTime = plcData.ReadTime,
                    TesisDebi = plcData.TesisDebi,
                    Akm = plcData.Akm,
                    Koi = plcData.Koi,
                    Ph = plcData.Ph,
                    Iletkenlik = plcData.Iletkenlik,
                    CozunmusOksijen = plcData.CozunmusOksijen,
                    KabinEnerjiAlarmi = plcData.KabinEnerjiYok,
                    KabinKalibrasyonModu = plcData.KabinKalibrasyon,
                    KabinBakimModu = plcData.KabinBakim,
                    KabinSaatlikYikamada = plcData.SaatlikYikamada,
                    KabinHaftalikYikamada = plcData.HaftalikYikamada
                };
            }

            // Status logic
            int status = 1; // Başarılı
            if (snapshot.KabinEnerjiAlarmi == true) status = 0; // Enerji Yok
            else if (snapshot.KabinKalibrasyonModu == true) status = 9; // Kalibrasyon
            else if (snapshot.KabinSaatlikYikamada == true) status = 23; // Saatlik Yıkama
            else if (snapshot.KabinHaftalikYikamada == true) status = 24; // Haftalık Yıkama
            else if (snapshot.KabinBakimModu == true) status = 25; // Bakım

            var responseData = MapDynamicSnapshotData(snapshot, selectedSensors, status);
            return Ok(SaisApiResponse<object>.Success(responseData));
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "GetInstantData hatası");
            return Ok(SaisApiResponse<object>.Failure(ex.Message));
        }
    }

    /// <summary>
    /// En son ölçüm verisi saatini döndürür. (3.10.4)
    /// </summary>
    [HttpGet("GetLastDataDate")]
    public async Task<ActionResult<SaisApiResponse<string>>> GetLastDataDate([FromQuery] Guid stationId)
    {
        try
        {
            var lastData = await _dbContext.SensorDatas
                .OrderByDescending(s => s.ReadTime)
                .FirstOrDefaultAsync(s => s.StationId == stationId);

            if (lastData == null) return Ok(SaisApiResponse<string>.Success(null));
            return Ok(SaisApiResponse<string>.Success(lastData.ReadTime.ToString("yyyy-MM-ddTHH:mm:ss")));
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "GetLastDataDate hatası");
            return Ok(SaisApiResponse<string>.Failure(ex.Message));
        }
    }

    /// <summary>
    /// Mevcut kanal bilgilerini döndürür. (3.10.5)
    /// </summary>
    [HttpGet("GetChannelInformation")]
    public async Task<ActionResult<SaisApiResponse<List<object>>>> GetChannelInformation([FromQuery] Guid stationId)
    {
        try
        {
            var channels = await _dbContext.ChannelInformations
                .AsNoTracking()
                .Where(c => c.StationId == stationId && c.IsActive && !c.IsDeleted)
                .Select(c => new 
                {
                    id = c.Id,
                    Brand = c.Brand ?? "Global Marka",
                    BrandModel = c.BrandModel ?? "Global Model",
                    FullName = c.FullName,
                    Parameter = c.Parameter,
                    ParameterText = c.ParameterText ?? c.Parameter,
                    Unit = c.UnitId,
                    UnitText = c.UnitText ?? "-",
                    IsActive = c.IsActive,
                    ChannelMinValue = c.ChannelMinValue,
                    ChannelMaxValue = c.ChannelMaxValue,
                    ChannelNumber = c.ChannelNumber,
                    CalibrationFormulaA = c.CalibrationFormulaA,
                    CalibrationFormulaB = c.CalibrationFormulaB,
                    SerialNumber = c.SerialNumber ?? "00000000"
                })
                .ToListAsync();

            return Ok(SaisApiResponse<List<object>>.Success(channels.Cast<object>().ToList()));
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "GetChannelInformation hatası");
            return Ok(SaisApiResponse<List<object>>.Failure(ex.Message));
        }
    }

    /// <summary>
    /// Mevcut SAİS bilgilerini döndürür. (3.10.6)
    /// </summary>
    [HttpGet("GetStationInformation")]
    public async Task<ActionResult<SaisApiResponse<object>>> GetStationInformation([FromQuery] Guid stationId)
    {
        try
        {
            var station = await _dbContext.StationSettings.AsNoTracking().FirstOrDefaultAsync(s => s.StationId == stationId);
            if (station == null) return Ok(SaisApiResponse<object>.Failure("İstasyon bulunamadı"));

            var stationInfo = new
            {
                StationId = station.StationId,
                Code = station.Code ?? "0000000",
                Name = station.Name,
                DataPeriodMinute = station.DataPeriodMinute,
                LastDataDate = await _dbContext.SensorDatas.Where(s => s.StationId == stationId).MaxAsync(s => (DateTime?)s.ReadTime),
                ConnectionDomainAddress = station.ConnectionDomainAddress,
                ConnectionPort = station.ConnectionPort,
                ConnectionUser = station.ConnectionUser,
                ConnectionPassword = station.ConnectionPassword,
                Company = station.Company,
                BirtDate = station.BirthDate?.ToString("yyyy-MM-ddTHH:mm:ss"),
                SetupDate = station.SetupDate?.ToString("yyyy-MM-ddTHH:mm:ss"),
                Adress = station.Address,
                Software = station.Software
            };

            return Ok(SaisApiResponse<object>.Success(stationInfo));
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "GetStationInformation hatası");
            return Ok(SaisApiResponse<object>.Failure(ex.Message));
        }
    }

    /// <summary>
    /// Kalibrasyon sorgulama servisi. (3.10.7)
    /// </summary>
    [HttpGet("GetCalibration")]
    public async Task<ActionResult<SaisApiResponse<List<object>>>> GetCalibration(
        [FromQuery] Guid stationId,
        [FromQuery] string startDate,
        [FromQuery] string endDate)
    {
        try
        {
            if (!DateTime.TryParse(startDate, out var start) || !DateTime.TryParse(endDate, out var end))
                return Ok(SaisApiResponse<List<object>>.Failure("Geçersiz tarih formatı"));

            var calibrations = await _dbContext.Calibrations
                .AsNoTracking()
                .Where(c => c.StationId == stationId && c.CalibrationDate >= start && c.CalibrationDate <= end)
                .OrderByDescending(c => c.CalibrationDate)
                .Select(c => new
                {
                    StationId = stationId,
                    DBColumnName = c.DbColumnName,
                    CalibrationDate = c.CalibrationDate.ToString("yyyy-MM-ddTHH:mm:ss"),
                    c.ZeroRef,
                    c.ZeroMeas,
                    c.ZeroDiff,
                    ZeroSTD = c.ZeroStd,
                    c.SpanRef,
                    c.SpanMeas,
                    c.SpanDiff,
                    SpanSTD = c.SpanStd,
                    c.ResultFactor,
                    c.ResultZero,
                    c.ResultSpan,
                    c.Result
                })
                .ToListAsync();

            return Ok(SaisApiResponse<List<object>>.Success(calibrations.Cast<object>().ToList()));
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "GetCalibration hatası");
            return Ok(SaisApiResponse<List<object>>.Failure(ex.Message));
        }
    }

    /// <summary>
    /// Kapalı saatleri sorgulama servisi. (3.10.8)
    /// </summary>
    [HttpGet("GetPowerOffTimes")]
    public async Task<ActionResult<SaisApiResponse<List<object>>>> GetPowerOffTimes(
        [FromQuery] Guid stationId,
        [FromQuery] string startDate,
        [FromQuery] string endDate)
    {
        try
        {
            if (!DateTime.TryParse(startDate, out var start) || !DateTime.TryParse(endDate, out var end))
                return Ok(SaisApiResponse<List<object>>.Failure("Geçersiz tarih formatı"));

            var powerOffTimes = await _dbContext.PowerOffTimes
                .AsNoTracking()
                .Where(p => p.StationId == stationId && p.StartDate >= start && (p.EndDate == null || p.EndDate <= end))
                .Select(p => new
                {
                    stationId,
                    startDate = p.StartDate.ToString("yyyy-MM-dd HH:mm:ss"),
                    endDate = p.EndDate.HasValue ? p.EndDate.Value.ToString("yyyy-MM-dd HH:mm:ss") : null
                })
                .ToListAsync();

            return Ok(SaisApiResponse<List<object>>.Success(powerOffTimes.Cast<object>().ToList()));
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "GetPowerOffTimes hatası");
            return Ok(SaisApiResponse<List<object>>.Failure(ex.Message));
        }
    }

    /// <summary>
    /// Log servisi. (3.10.9)
    /// </summary>
    [HttpGet("GetLog")]
    public async Task<ActionResult<SaisApiResponse<List<object>>>> GetLog(
        [FromQuery] Guid stationId,
        [FromQuery] string startDate,
        [FromQuery] string endDate)
    {
        try
        {
            if (!DateTime.TryParse(startDate, out var start) || !DateTime.TryParse(endDate, out var end))
                return Ok(SaisApiResponse<List<object>>.Failure("Geçersiz tarih formatı"));

            var logs = await _dbContext.LogEntries
                .AsNoTracking()
                .Where(l => l.StationId == stationId && l.LogCreatedDate >= start && l.LogCreatedDate <= end)
                .OrderByDescending(l => l.LogCreatedDate)
                .Select(l => new
                {
                    logTitle = l.LogTitle,
                    LogDescription = l.LogDescription,
                    LogCreatedDate = l.LogCreatedDate.ToString("yyyy-MM-ddTHH:mm:ss")
                })
                .ToListAsync();

            return Ok(SaisApiResponse<List<object>>.Success(logs.Cast<object>().ToList()));
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "GetLog hatası");
            return Ok(SaisApiResponse<List<object>>.Failure(ex.Message));
        }
    }

    /// <summary>
    /// Yeni numune alımına başla servisi. (3.10.10)
    /// </summary>
    [HttpGet("StartSample")]
    public async Task<ActionResult<SaisApiResponse<bool>>> StartSample(
        [FromQuery] Guid stationId,
        [FromQuery] string code)
    {
        try
        {
            _logger.LogInformation("StartSample tetiklendi. StationId: {StationId}, Code: {Code}", stationId, code);
            bool success = await _dataService.StartSampleAsync(code);
            return Ok(SaisApiResponse<bool>.Success(success));
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "StartSample hatası");
            return Ok(SaisApiResponse<bool>.Failure(ex.Message));
        }
    }

    private object MapDynamicSensorData(SensorData m, List<string> selectedSensors, int period)
    {
        var data = new Dictionary<string, object?>
        {
            { "Period", period },
            { "ReadTime", m.ReadTime.ToString("yyyy-MM-ddTHH:mm:ss") }
        };

        if (IsSelected(selectedSensors, "TesisDebi")) { data["Debi"] = m.TesisDebi; data["Debi_Status"] = m.TesisDebi_Status; }
        if (IsSelected(selectedSensors, "OlcumCihaziAkisHizi")) { data["AkisHizi"] = m.AkisHizi; data["AkisHizi_Status"] = m.AkisHizi_Status; }
        if (IsSelected(selectedSensors, "Ph")) { data["pH"] = m.Ph; data["pH_Status"] = m.Ph_Status; }
        if (IsSelected(selectedSensors, "Iletkenlik")) { data["Iletkenlik"] = m.Iletkenlik; data["Iletkenlik_Status"] = m.Iletkenlik_Status; }
        if (IsSelected(selectedSensors, "CozunmusOksijen")) { data["CozunmusOksijen"] = m.CozunmusOksijen; data["CozunmusOksijen_Status"] = m.CozunmusOksijen_Status; }
        if (IsSelected(selectedSensors, "Koi")) { data["KOi"] = m.Koi; data["KOi_Status"] = m.Koi_Status; }
        if (IsSelected(selectedSensors, "Akm")) { data["AKM"] = m.Akm; data["AKM_Status"] = m.Akm_Status; }
        if (IsSelected(selectedSensors, "KabinSicakligi")) { data["Sicaklik"] = m.Sicaklik; data["Sicaklik_Status"] = m.Sicaklik_Status; }
        
        return data;
    }

    private object MapDynamicSnapshotData(ISKI.IBKS.Application.Features.StationSnapshots.Dtos.StationSnapshotDto dto, List<string> selectedSensors, int status)
    {
        var data = new Dictionary<string, object?>
        {
            { "Period", 1 },
            { "ReadTime", DateTime.Now.ToString("yyyy-MM-ddTHH:mm:ss") }
        };

        if (IsSelected(selectedSensors, "TesisDebi")) { data["Debi"] = dto.TesisDebi; data["Debi_Status"] = status; }
        if (IsSelected(selectedSensors, "OlcumCihaziAkisHizi")) { data["AkisHizi"] = dto.OlcumCihaziAkisHizi; data["AkisHizi_Status"] = status; }
        if (IsSelected(selectedSensors, "Ph")) { data["pH"] = dto.Ph; data["pH_Status"] = status; }
        if (IsSelected(selectedSensors, "Iletkenlik")) { data["Iletkenlik"] = dto.Iletkenlik; data["Iletkenlik_Status"] = status; }
        if (IsSelected(selectedSensors, "CozunmusOksijen")) { data["CozunmusOksijen"] = dto.CozunmusOksijen; data["CozunmusOksijen_Status"] = status; }
        if (IsSelected(selectedSensors, "Koi")) { data["KOi"] = dto.Koi; data["KOi_Status"] = status; }
        if (IsSelected(selectedSensors, "Akm")) { data["AKM"] = dto.Akm; data["AKM_Status"] = status; }
        if (IsSelected(selectedSensors, "KabinSicakligi")) { data["Sicaklik"] = dto.KabinSicakligi; data["Sicaklik_Status"] = status; }

        return data;
    }

    private bool IsSelected(List<string> selected, string key) => 
        selected.Count == 0 || selected.Any(s => s.Equals(key, StringComparison.OrdinalIgnoreCase));
}

