using ISKI.IBKS.Domain.Abstractions;
using ISKI.IBKS.Domain.Entities;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ISKI.IBKS.Infrastructure.IoT.Plc;

public sealed class StationSnapshotCache : IStationSnapshotCache
{
    private readonly ConcurrentDictionary<Guid, StationSnapshot> _cache = new();

    public Task<StationSnapshot> Set(Guid? stationId, StationSnapshot stationSnapshot)
    {
        if (stationId is null)
            throw new ArgumentNullException(nameof(stationId));

        _cache[stationId.Value] = stationSnapshot;

        return Task.FromResult(stationSnapshot);
    }

    public bool TryGet(Guid? stationId, out StationSnapshot? snapshot)
    {
        snapshot = null;

        if (stationId is null)
            return false;

        return _cache.TryGetValue(stationId.Value, out snapshot);
    }
}
