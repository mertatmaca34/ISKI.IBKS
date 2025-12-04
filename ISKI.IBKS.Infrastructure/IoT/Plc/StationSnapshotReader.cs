using ISKI.IBKS.Domain.Abstractions;
using ISKI.IBKS.Domain.Entities;
using ISKI.IBKS.Infrastructure.IoT.Plc.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ISKI.IBKS.Infrastructure.IoT.Plc;

public class StationSnapshotReader : IStationSnapshotReader
{
    private readonly IPlcClient _plcClient;

    public StationSnapshotReader(IPlcClient plcClient)
    {
        _plcClient = plcClient;
    }
    
    public Task<StationSnapshot?> GetLatestSnapshotAsync(string stationIp)
    {
        throw new NotImplementedException();
    }
}
