using ISKI.IBKS.Domain.Abstractions;
using ISKI.IBKS.Domain.Entities;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Concurrent;
using System.Threading.Tasks;

namespace ISKI.IBKS.Infrastructure.IoT.Plc;

public class StationSnapshotCache : IStationSnapshotCache
{
    private readonly ConcurrentDictionary<Guid, StationSnapshot> _cache = new();
    private readonly ILogger<StationSnapshotCache> _logger;

    public StationSnapshotCache(ILogger<StationSnapshotCache> logger)
    {
        _logger = logger;
    }

    public Task<StationSnapshot> Set(Guid? stationId, StationSnapshot stationSnapshot)
    {
        if (stationId is null) throw new ArgumentNullException(nameof(stationId));
        if (stationSnapshot is null) throw new ArgumentNullException(nameof(stationSnapshot));

        _cache.AddOrUpdate(stationId.Value, stationSnapshot, (k, v) => stationSnapshot);
        return Task.FromResult(stationSnapshot);
    }

    public bool TryGet(Guid? stationId, out StationSnapshot? snapshot)
    {
        if (stationId is null)
        {
            snapshot = null;
            return false;
        }

        return _cache.TryGetValue(stationId.Value, out snapshot);
    }
}
