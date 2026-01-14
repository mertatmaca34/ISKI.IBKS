using ISKI.IBKS.Application.Features.StationSnapshots.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ISKI.IBKS.Application.Features.StationSnapshots.Abstractions;

public interface IStationSnapshotCache
{
    Task<StationSnapshotDto> Set(Guid? stationId, StationSnapshotDto stationSnapshot);
    Task<StationSnapshotDto?> Get(Guid? stationId);
}
