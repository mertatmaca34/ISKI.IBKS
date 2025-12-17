using ISKI.IBKS.Application.Features.StationStatus.Dtos;
using ISKI.IBKS.Application.Features.StationStatus.Services;
using ISKI.IBKS.Domain.Abstractions;
using ISKI.IBKS.Infrastructure.IoT.Plc.Abstractions;
using ISKI.IBKS.Infrastructure.IoT.Plc.Configuration;
using Microsoft.Extensions.Options;

namespace ISKI.IBKS.Infrastructure.Features.StationStatus;

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

    public Task<StationStatusDto> GetStationStatusAsync(Guid stationId, CancellationToken cancellationToken = default)
    {
        if (!_stationSnapshotCache.TryGet(stationId, out var snapshot) || snapshot is null)
        {
            return Task.FromResult<StationStatusDto>(null!);
        }

        if (snapshot.SaatlikYikamaSaati != null && snapshot.YikamaDakikasi != null && snapshot.YikamaSaniyesi != null)
        {
            var dailyWashRemainingTime = new TimeSpan((int)snapshot.SaatlikYikamaSaati, (int)snapshot.YikamaDakikasi, (int)snapshot.YikamaSaniyesi);
            var weeklyWashRemainingTime = snapshot.HaftalikYikamaGunu != null
                ? new TimeSpan((int)snapshot.HaftalikYikamaGunu, (int)snapshot.SaatlikYikamaSaati, (int)snapshot.YikamaDakikasi, (int)snapshot.YikamaSaniyesi)
                : TimeSpan.Zero;

            var statusDto = new StationStatusDto
            {
                IsConnected = _plcClient.IsConnected,
                UpTime = _plcClient.Uptime,
                DailyWashRemainingTime = dailyWashRemainingTime,
                WeeklyWashRemainingTime = weeklyWashRemainingTime,
                SystemTime = snapshot.SystemTime ?? DateTime.MinValue
            };
            return Task.FromResult(statusDto);
        }

        return Task.FromResult<StationStatusDto>(null!);
    }
}
