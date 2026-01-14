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
    /// PLC saatini sorgulayıp döndürür.
    /// </summary>
    [HttpGet("GetServerDateTime")]
    public async Task<ActionResult<SaisApiResponse<DateTime>>> GetServerDateTime([FromQuery] Guid stationId)
    {
        try
        {
            var snapshot = await _snapshotCache.Get(_plcSettings.Station.StationId);
            if (snapshot?.SystemTime == null || snapshot.SystemTime == DateTime.MinValue)
            {
                return Ok(SaisApiResponse<DateTime>.Success(DateTime.Now));
            }

            return Ok(SaisApiResponse<DateTime>.Success(snapshot.SystemTime.Value));
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "GetServerDateTime hatası");
            return Ok(SaisApiResponse<DateTime>.Failure(ex.Message));
        }
    }

    /// <summary>
    /// Verilen iki tarih arasındaki, verilen periyottaki verileri döndürür.
    /// </summary>
    [HttpGet("GetData")]
    public async Task<ActionResult<SaisApiResponse<List<object>>>> GetData(
        [FromQuery] Guid stationId,
        [FromQuery] DateTime startDate,
        [FromQuery] DateTime endDate,
        [FromQuery] int period = 1)
    {
        try
        {
            var data = await _dbContext.SensorDatas
                .AsNoTracking()
                .Where(s => s.ReadTime >= startDate && s.ReadTime <= endDate)
                .OrderBy(s => s.ReadTime)
                .Select(s => new
                {
                    Period = period,
                    s.ReadTime,
                    AKM = s.Akm,
                    AKM_Status = s.Akm_Status,
                    Debi = s.TesisDebi,
                    Debi_Status = s.TesisDebi_Status,
                    KOi = s.Koi,
                    KOi_Status = s.Koi_Status,
                    pH = s.Ph,
                    pH_Status = s.Ph_Status,
                    Iletkenlik = s.Iletkenlik,
                    Iletkenlik_Status = s.Iletkenlik_Status,
                    CozunmusOksijen = s.CozunmusOksijen,
                    CozunmusOksijen_Status = s.CozunmusOksijen_Status
                })
                .ToListAsync();

            return Ok(SaisApiResponse<List<object>>.Success(data.Cast<object>().ToList()));
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "GetData hatası");
            return Ok(SaisApiResponse<List<object>>.Failure(ex.Message));
        }
    }

    /// <summary>
    /// Anlık olarak okunan değerleri döndürür.
    /// </summary>
    [HttpGet("GetInstantData")]
    public async Task<ActionResult<SaisApiResponse<object>>> GetInstantData([FromQuery] Guid stationId)
    {
        try
        {
            var dto = await _snapshotCache.Get(_plcSettings.Station.StationId);
            
            if (dto == null)
            {
                // Try force read if cache is empty
                var plcData = await _dataService.ReadCurrentDataAsync();
                if (plcData == null) 
                    return Ok(SaisApiResponse<object>.Failure("PLC bağlantısı yok"));

                // Map PlcDataSnapshot to StationSnapshotDto (Simple mapping for fallback)
                dto = new ISKI.IBKS.Application.Features.StationSnapshots.Dtos.StationSnapshotDto
                {
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

            // Calculate Status logic (Simple version based on specs)
            int status = 1; // VeriGecerli
            if (dto.KabinEnerjiAlarmi == true) status = 0; // VeriYok
            else if (dto.KabinKalibrasyonModu == true) status = 9; // SistemKal
            else if (dto.KabinSaatlikYikamada == true) status = 23; // Yikama
            else if (dto.KabinHaftalikYikamada == true) status = 24; // HaftalikYikama
            else if (dto.KabinBakimModu == true) status = 25; // IstasyonBakimda

            var data = new
            {
                Period = 1,
                ReadTime = DateTime.Now,
                AKM = dto.Akm ?? 0,
                AKM_Status = status,
                Debi = dto.TesisDebi ?? 0,
                Debi_Status = status,
                KOi = dto.Koi ?? 0,
                KOi_Status = status,
                pH = dto.Ph ?? 0,
                pH_Status = status,
                Iletkenlik = dto.Iletkenlik ?? 0,
                Iletkenlik_Status = status,
                CozunmusOksijen = dto.CozunmusOksijen ?? 0,
                CozunmusOksijen_Status = status
            };

            return Ok(SaisApiResponse<object>.Success(data));
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "GetInstantData hatası");
            return Ok(SaisApiResponse<object>.Failure(ex.Message));
        }
    }

    /// <summary>
    /// En son veritabanına kaydettiğimiz dakikalık verinin tarihini döndürür.
    /// </summary>
    [HttpGet("GetLastDataDate")]
    public async Task<ActionResult<SaisApiResponse<DateTime?>>> GetLastDataDate([FromQuery] Guid stationId)
    {
        try
        {
            var lastData = await _dbContext.SensorDatas
                .OrderByDescending(s => s.ReadTime)
                .FirstOrDefaultAsync();

            return Ok(SaisApiResponse<DateTime?>.Success(lastData?.ReadTime));
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "GetLastDataDate hatası");
            return Ok(SaisApiResponse<DateTime?>.Failure(ex.Message));
        }
    }

    /// <summary>
    /// Kanal bilgilerini döndürür.
    /// </summary>
    [HttpGet("GetChannelInformation")]
    public async Task<ActionResult<SaisApiResponse<List<object>>>> GetChannelInformation([FromQuery] Guid stationId)
    {
        try
        {
            var channels = await _dbContext.ChannelInformations
                .AsNoTracking()
                .Where(c => c.IsActive)
                .Select(c => new 
                {
                    id = c.Id,
                    c.Parameter,
                    c.ParameterText,
                    c.IsActive,
                    c.ChannelNumber,
                    Unit = c.UnitId, // Fix: UnitId mapped to Unit
                    c.UnitText,
                    c.ChannelMinValue,
                    c.ChannelMaxValue,
                    c.CalibrationFormulaA,
                    c.CalibrationFormulaB,
                    c.SerialNumber,
                    c.Brand,
                    c.BrandModel,
                    c.FullName
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
    /// İstasyon bilgilerini döndürür.
    /// </summary>
    [HttpGet("GetStationInformation")]
    public async Task<ActionResult<SaisApiResponse<object>>> GetStationInformation([FromQuery] Guid stationId)
    {
        try
        {
            var station = await _dbContext.StationSettings.AsNoTracking().FirstOrDefaultAsync();
            if (station == null)
            {
                return Ok(SaisApiResponse<object>.Failure("İstasyon ayarları bulunamadı"));
            }

            var stationInfo = new
            {
                StationId = station.StationId,
                Code = station.Code ?? "0000000",
                Name = station.Name, // Fix: Name
                DataPeriodMinute = 1,
                LastDataDate = await _dbContext.SensorDatas.MaxAsync(s => (DateTime?)s.ReadTime) ?? DateTime.Now.AddDays(-1),
                ConnectionDomainAddress = station.ConnectionDomainAddress,
                ConnectionPort = station.ConnectionPort,
                ConnectionUser = station.ConnectionUser,
                ConnectionPassword = station.ConnectionPassword,
                Company = station.Company,
                BirtDate = station.BirthDate,
                SetupDate = station.SetupDate,
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
    /// Kalibrasyon sorgulama servisi.
    /// </summary>
    [HttpGet("GetCalibration")]
    public async Task<ActionResult<SaisApiResponse<List<object>>>> GetCalibration(
        [FromQuery] Guid stationId,
        [FromQuery] DateTime startDate,
        [FromQuery] DateTime endDate)
    {
        try
        {
            var calibrations = await _dbContext.Calibrations
                .AsNoTracking()
                .Where(c => c.CalibrationDate >= startDate && c.CalibrationDate <= endDate)
                .OrderByDescending(c => c.CalibrationDate)
                .Select(c => new
                {
                    StationId = stationId,
                    DBColumnName = c.DbColumnName,
                    c.CalibrationDate,
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
    /// Verilen iki tarih arasında kapalı olunan saatleri döndürür.
    /// </summary>
    [HttpGet("GetPowerOffTimes")]
    public async Task<ActionResult<SaisApiResponse<List<object>>>> GetPowerOffTimes(
        [FromQuery] Guid stationId,
        [FromQuery] DateTime startDate,
        [FromQuery] DateTime endDate)
    {
        try
        {
            var powerOffTimes = await _dbContext.PowerOffTimes
                .AsNoTracking()
                .Where(p => p.StartDate >= startDate && (p.EndDate == null || p.EndDate <= endDate))
                .Select(p => new
                {
                    stationId,
                    startDate = p.StartDate,
                    endDate = p.EndDate
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
    /// Sistemde oluşturulmuş olan logları verilen tarihler arasında döndürür.
    /// </summary>
    [HttpGet("GetLog")]
    public async Task<ActionResult<SaisApiResponse<List<object>>>> GetLog(
        [FromQuery] Guid stationId,
        [FromQuery] DateTime startDate,
        [FromQuery] DateTime endDate)
    {
        try
        {
            var logs = await _dbContext.LogEntries
                .AsNoTracking()
                .Where(l => l.LogCreatedDate >= startDate && l.LogCreatedDate <= endDate)
                .OrderByDescending(l => l.LogCreatedDate)
                .Select(l => new
                {
                    logTitle = l.LogTitle,
                    LogDescription = l.LogDescription,
                    LogCreatedDate = l.LogCreatedDate
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
    /// PLC NumuneTetik Bitine true göndererek sistemin numune almasını sağlar.
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
            
            if (success)
                return Ok(SaisApiResponse<bool>.Success(true));
            else
                return Ok(SaisApiResponse<bool>.Failure("PLC'ye yazılamadı"));
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "StartSample hatası");
            return Ok(SaisApiResponse<bool>.Failure(ex.Message));
        }
    }
}

