using ISKI.IBKS.Application.Features.StationSnapshots.Abstractions;
using ISKI.IBKS.Application.Features.StationSnapshots.Dtos;
using ISKI.IBKS.Domain.Entities;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Concurrent;
using System.Threading.Tasks;

namespace ISKI.IBKS.Infrastructure.IoT.Plc;

public class StationSnapshotCache() : IStationSnapshotCache
{
    private readonly ConcurrentDictionary<Guid, StationSnapshotDto> _cache = new();

    public Task<StationSnapshotDto?> Get(Guid? stationId)
    {
        if (stationId is null)
            return Task.FromResult<StationSnapshotDto?>(null);

        if (!_cache.TryGetValue(stationId.Value, out StationSnapshotDto? snapshot))
            return Task.FromResult<StationSnapshotDto?>(null);

        return Task.FromResult<StationSnapshotDto?>(snapshot);
    }

    public Task<StationSnapshotDto> Set(Guid? stationId, StationSnapshotDto stationSnapshot)
    {
        if (stationId is null) throw new ArgumentNullException(nameof(stationId));
        if (stationSnapshot is null)
        {
            throw new ArgumentNullException(nameof(stationSnapshot));
        }

        _cache.AddOrUpdate(stationId.Value, stationSnapshot, (k, v) => stationSnapshot);
        return Task.FromResult(stationSnapshot);
    }

    public bool TryGet(Guid? stationId, out StationSnapshotDto? snapshot)
    {
        if (stationId is null)
        {
            snapshot = null;
            return false;
        }

        return _cache.TryGetValue(stationId.Value, out snapshot);
    }
}
