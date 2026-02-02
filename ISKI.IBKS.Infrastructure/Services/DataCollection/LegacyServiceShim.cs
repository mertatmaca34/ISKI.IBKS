using ISKI.IBKS.Application.Common.IoT.Snapshots;
using ISKI.IBKS.Domain.IoT;
using ISKI.IBKS.Shared.Localization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace ISKI.IBKS.Infrastructure.Services.DataCollection;

public class LegacyServiceShim :
    IAnalogSensorService,
    IDigitalSensorService,
    IStationStatusService,
    IHealthSummaryService
{
    private readonly IStationSnapshotCache _cache;

    public LegacyServiceShim(IStationSnapshotCache cache)
    {
        _cache = cache;
    }

    async Task<IEnumerable<ChannelReadingDto>> IAnalogSensorService.GetReadingsAsync(Guid stationId, CancellationToken ct)
    {
        var snapshot = await _cache.Get(stationId);
        if (snapshot == null) return Enumerable.Empty<ChannelReadingDto>();

        return new List<ChannelReadingDto>
        {
            new(Strings.Sensor_TesisDebi, snapshot.TesisDebi, "m³/h", 0, 1000, false),
            new(Strings.Sensor_Debi, snapshot.NumuneDebi, "L/h", 0, 100, false),
            new(Strings.Sensor_Ph, snapshot.Ph, "pH", 0, 14, false),
            new(Strings.Sensor_Iletkenlik, snapshot.Iletkenlik, "µS/cm", 0, 5000, false),
            new(Strings.Sensor_CozunmusOksijen, snapshot.CozunmusOksijen, "mg/L", 0, 20, false),
            new(Strings.Sensor_Koi, snapshot.Koi, "mg/L", 0, 1000, false),
            new(Strings.Sensor_Akm, snapshot.Akm, "mg/L", 0, 1000, false)
        };
    }

    public Task<IEnumerable<ChannelReadingDto>> GetChannelsAsync(Guid stationId, CancellationToken ct = default) => ((IAnalogSensorService)this).GetReadingsAsync(stationId, ct);

    async Task<IEnumerable<DigitalReadingDto>> IDigitalSensorService.GetReadingsAsync(Guid stationId, CancellationToken ct)
    {
        var snapshot = await _cache.Get(stationId);
        if (snapshot == null) return Enumerable.Empty<DigitalReadingDto>();

        return new List<DigitalReadingDto>
        {
            new("Kabin Oto", snapshot.KabinOtoModu, false),
            new(Strings.Sim_Maintenance, snapshot.KabinBakimModu, false),
            new(Strings.Sim_Smoke, snapshot.KabinDumanAlarmi, snapshot.KabinDumanAlarmi),
            new(Strings.Sim_Flood, snapshot.KabinSuBaskiniAlarmi, snapshot.KabinSuBaskiniAlarmi),
            new("Kapı Alarmı", snapshot.KabinKapiAlarmi, snapshot.KabinKapiAlarmi),
            new(Strings.Sim_NoEnergy, snapshot.KabinEnerjiAlarmi, snapshot.KabinEnerjiAlarmi),
            new(Strings.Sim_EmergencyStop, snapshot.KabinAcilStopBasiliAlarmi, snapshot.KabinAcilStopBasiliAlarmi)
        };
    }

    public Task<IEnumerable<DigitalReadingDto>> GetDigitalSensorsAsync(Guid stationId, CancellationToken ct = default) => ((IDigitalSensorService)this).GetReadingsAsync(stationId, ct);

    public async Task<StationStatusDto> GetStatusAsync(Guid stationId, CancellationToken ct = default)
    {
        var snapshot = await _cache.Get(stationId);
        return new StationStatusDto(
            snapshot != null,
            0,
            TimeSpan.Zero,
            TimeSpan.Zero,
            snapshot?.SystemTime ?? DateTime.Now
        );
    }

    public Task<StationStatusDto> GetStationStatusAsync(Guid stationId, CancellationToken ct = default) => GetStatusAsync(stationId, ct);

    public async Task<HealthSummaryDto> GetSummaryAsync(Guid stationId, CancellationToken ct = default)
    {
        var snapshot = await _cache.Get(stationId);
        bool healthy = snapshot != null && !snapshot.KabinDumanAlarmi && !snapshot.KabinSuBaskiniAlarmi;
        return new HealthSummaryDto(healthy, 0, 0, healthy ? Strings.Status_SystemHealthy : Strings.Status_SystemAlarms);
    }
}
