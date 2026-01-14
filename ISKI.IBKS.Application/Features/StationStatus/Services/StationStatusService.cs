using ISKI.IBKS.Application.Common.Results;
using ISKI.IBKS.Application.Features.DigitalSensors.Dtos;
using ISKI.IBKS.Application.Features.Plc.Abstractions;
using ISKI.IBKS.Application.Features.StationSnapshots.Abstractions;
using ISKI.IBKS.Application.Features.StationStatus.Dtos;
using ISKI.IBKS.Application.Features.StationStatus.Services;
using ISKI.IBKS.Infrastructure.IoT.Plc.Configuration;
using Microsoft.Extensions.Options;

namespace ISKI.IBKS.Infrastructure.Application.Features.StationStatus;

public class StationStatusService : IStationStatusService
{
    private readonly IStationSnapshotCache _stationSnapshotCache;
    private readonly IOptions<PlcSettings> _plcSettings;
    private readonly IPlcClient _plcClient;

    public StationStatusService(IStationSnapshotCache stationSnapshotCache, IOptions<PlcSettings> plcSettings, IPlcClient plcClient)
    {
        _stationSnapshotCache = stationSnapshotCache;
        _plcSettings = plcSettings;
        _plcClient = plcClient;
    }

    public async Task<IDataResult<StationStatusDto>> GetStationStatusAsync(Guid stationId, CancellationToken cancellationToken = default)
    {
        var stationSnapshot = await _stationSnapshotCache.Get(stationId);

        if (stationSnapshot == null)
        {
            return new ErrorDataResult<StationStatusDto>(null);
        }


        if (stationSnapshot.SaatlikYikamaSaati != null && stationSnapshot.YikamaDakikasi != null && stationSnapshot.YikamaSaniyesi != null)
        {
            var dailyWashRemainingTime = new TimeSpan((int)stationSnapshot.SaatlikYikamaSaati, (int)stationSnapshot.YikamaDakikasi, (int)stationSnapshot.YikamaSaniyesi);
            var weeklyWashRemainingTime = stationSnapshot.HaftalikYikamaGunu != null
                ? new TimeSpan((int)stationSnapshot.HaftalikYikamaGunu, (int)stationSnapshot.SaatlikYikamaSaati, (int)stationSnapshot.YikamaDakikasi, (int)stationSnapshot.YikamaSaniyesi)
                : TimeSpan.Zero;

            var statusDto = new StationStatusDto
            {
                IsConnected = _plcClient.IsConnected,
                UpTime = _plcClient.Uptime,
                DailyWashRemainingTime = dailyWashRemainingTime,
                WeeklyWashRemainingTime = weeklyWashRemainingTime,
                SystemTime = stationSnapshot.SystemTime ?? DateTime.MinValue
            };
            return new SuccesDataResult<StationStatusDto>(statusDto);
        }

        return new ErrorDataResult<StationStatusDto>(null);
    }
}
