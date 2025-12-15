using ISKI.IBKS.Domain.Abstractions;
using ISKI.IBKS.Infrastructure.IoT.Plc.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using Serilog;
using static System.Collections.Specialized.BitVector32;

namespace ISKI.IBKS.Infrastructure.IoT.Plc;

public class PlcPollingService : BackgroundService
{
    private readonly IStationSnapshotReader _stationSnapshotReader;
    private readonly IStationSnapshotCache _stationSnapshotCache;
    private readonly IOptions<PlcSettings> _plcSettings;

    public PlcPollingService(
        IStationSnapshotReader stationSnapshotReader,
        IStationSnapshotCache stationSnapshotCache,
        IOptions<PlcSettings> plcSettings)
    {
        _stationSnapshotReader = stationSnapshotReader;
        _stationSnapshotCache = stationSnapshotCache;
        _plcSettings = plcSettings;
    }


    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        while (!stoppingToken.IsCancellationRequested)
        {

            if (_plcSettings.Value.Stations.Count > 1)
            {
                foreach (var station in _plcSettings.Value.Stations)
                {
                    if (stoppingToken.IsCancellationRequested)
                        break;

                    try
                    {
                        var snapshot = await _stationSnapshotReader.Read(station.IpAddress);

                        if (snapshot is null)
                        {
                            //TODO
                        }

                        await _stationSnapshotCache.Set(station.StationId, snapshot!);
                    }
                    catch (Exception)
                    {
                        Log.Information("PLC polling error at station {StationIp}", station.IpAddress);
                        //throw new NotImplementedException();
                    }
                }
            }
            else
            {
                if (stoppingToken.IsCancellationRequested)
                    break;

                var station = _plcSettings.Value.Station;

                try
                {

                    var snapshot = await _stationSnapshotReader.Read(station.IpAddress);

                    if (snapshot is null)
                    {
                        //TODO
                    }

                    await _stationSnapshotCache.Set(station.StationId, snapshot!);
                }
                catch (Exception)
                {
                    Log.Information("PLC polling error at station {StationIp}", station.IpAddress);

                    //throw new NotImplementedException();
                }
            }

            await Task.Delay(TimeSpan.FromSeconds(5), stoppingToken);
        }
    }
}
