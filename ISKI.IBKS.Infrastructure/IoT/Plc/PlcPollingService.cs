using ISKI.IBKS.Domain.Abstractions;
using ISKI.IBKS.Infrastructure.IoT.Plc.Configuration;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ISKI.IBKS.Infrastructure.IoT.Plc;

public class PlcPollingService : BackgroundService
{
    private readonly IStationSnapshotReader _stationSnapshotReader;
    private readonly IStationSnapshotCache _stationSnapshotCache;
    private readonly PlcSettings _plcSettings;

    public PlcPollingService(
        IStationSnapshotReader stationSnapshotReader,
        IStationSnapshotCache stationSnapshotCache,
        PlcSettings plcSettings)
    {
        _stationSnapshotReader = stationSnapshotReader;
        _stationSnapshotCache = stationSnapshotCache;
        _plcSettings = plcSettings;
    }


    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        while (!stoppingToken.IsCancellationRequested)
        {
            foreach (var station in _plcSettings.Stations)
            {
                if (stoppingToken.IsCancellationRequested)
                    break;

                try
                {
                    // Read: IP ile çalışıyoruz (istersen StationCode'a çevir)
                    var snapshot = await _stationSnapshotReader.Read(station.IpAddress);

                    if (snapshot is null)
                    {
                        //TODO
                    }

                    _stationSnapshotCache.Set(station.IpAddress, snapshot!);
                }
                catch (Exception ex)
                {

                    throw new NotImplementedException();
                }
            }
        }
    }
}
