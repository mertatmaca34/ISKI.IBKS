using ISKI.IBKS.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ISKI.IBKS.Domain.Abstractions;

public interface IStationSnapshotCache
{
    Task<StationSnapshot> Set(string stationIp, StationSnapshot stationSnapshot);
    Task<StationSnapshot?> GetLast(string? stationIp);
}
