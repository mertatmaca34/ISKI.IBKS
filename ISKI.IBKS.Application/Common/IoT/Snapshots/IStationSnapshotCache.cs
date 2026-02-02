using ISKI.IBKS.Domain.IoT;

namespace ISKI.IBKS.Application.Common.IoT.Snapshots;

public interface IStationSnapshotCache
{
    Task<PlcDataSnapshot?> Get(Guid stationId);
    Task Set(Guid stationId, PlcDataSnapshot snapshot);
}

