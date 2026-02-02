using ISKI.IBKS.Application.Common.IoT.Snapshots;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace ISKI.IBKS.Application.Common.IoT.Snapshots;

public interface IAnalogSensorService
{
    Task<IEnumerable<ChannelReadingDto>> GetReadingsAsync(Guid stationId, CancellationToken ct = default);
    Task<IEnumerable<ChannelReadingDto>> GetChannelsAsync(Guid stationId, CancellationToken ct = default);
}

public interface IDigitalSensorService
{
    Task<IEnumerable<DigitalReadingDto>> GetReadingsAsync(Guid stationId, CancellationToken ct = default);
    Task<IEnumerable<DigitalReadingDto>> GetDigitalSensorsAsync(Guid stationId, CancellationToken ct = default);
}

public interface IStationStatusService
{
    Task<StationStatusDto> GetStatusAsync(Guid stationId, CancellationToken ct = default);
    Task<StationStatusDto> GetStationStatusAsync(Guid stationId, CancellationToken ct = default);
}

public interface IHealthSummaryService
{
    Task<HealthSummaryDto> GetSummaryAsync(Guid stationId, CancellationToken ct = default);
}
