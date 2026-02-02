using ISKI.IBKS.Domain.IoT;
using ISKI.IBKS.Application.Common.IoT.Snapshots;
using System.Collections.Concurrent;

namespace ISKI.IBKS.Infrastructure.IoT.Plc;

public class StationSnapshotCache : IStationSnapshotCache
{
    private readonly ConcurrentDictionary<Guid, PlcDataSnapshot> _cache = new();

    public Task<PlcDataSnapshot?> Get(Guid stationId)
    {
        if (!_cache.TryGetValue(stationId, out PlcDataSnapshot? snapshot))
            return Task.FromResult<PlcDataSnapshot?>(null);

        return Task.FromResult<PlcDataSnapshot?>(snapshot);
    }

    public Task Set(Guid stationId, PlcDataSnapshot snapshot)
    {
        _cache.AddOrUpdate(stationId, snapshot, (k, v) => snapshot);
        return Task.CompletedTask;
    }
}
