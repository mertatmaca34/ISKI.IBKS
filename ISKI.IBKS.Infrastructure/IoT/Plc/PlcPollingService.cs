using ISKI.IBKS.Domain.Abstractions;
using ISKI.IBKS.Infrastructure.IoT.Plc.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using Microsoft.Extensions.Logging;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace ISKI.IBKS.Infrastructure.IoT.Plc;

public class PlcPollingService : BackgroundService
{
    private readonly IStationSnapshotReader _stationSnapshotReader;
    private readonly IStationSnapshotCache _stationSnapshotCache;
    private readonly PlcSettings _plcSettings;
    private readonly ILogger<PlcPollingService> _logger;

    public PlcPollingService(
        IStationSnapshotReader stationSnapshotReader,
        IStationSnapshotCache stationSnapshotCache,
        IOptions<PlcSettings> plcSettings,
        ILogger<PlcPollingService> logger)
    {
        _stationSnapshotReader = stationSnapshotReader;
        _stationSnapshotCache = stationSnapshotCache;
        _plcSettings = plcSettings.Value;
        _logger = logger;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        _logger.LogInformation("PLC polling service started.");

        var stations = _plcSettings.Stations?.Count > 0 ? _plcSettings.Stations : new List<PlcStationConfig> { _plcSettings.Station };

        while (!stoppingToken.IsCancellationRequested)
        {
            foreach (var station in stations)
            {
                if (stoppingToken.IsCancellationRequested)
                    break;

                try
                {
                    var snapshot = await _stationSnapshotReader.Read(station.IpAddress);

                    if (snapshot is not null)
                    {
                        await _stationSnapshotCache.Set(station.StationId, snapshot);
                    }
                    else
                    {
                        _logger.LogWarning("Snapshot was null for station {StationIp}", station.IpAddress);
                    }
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "PLC polling error at station {StationIp}", station.IpAddress);
                }
            }

            await Task.Delay(TimeSpan.FromSeconds(5), stoppingToken);
        }

        _logger.LogInformation("PLC polling service stopped.");
    }
}
