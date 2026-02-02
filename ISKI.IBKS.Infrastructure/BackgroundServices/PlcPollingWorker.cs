using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using ISKI.IBKS.Domain.IoT;
using ISKI.IBKS.Application.Common.Configuration;
using ISKI.IBKS.Application.Common.IoT.Plc;
using ISKI.IBKS.Application.Common.Features.Telemetry.ProcessTelemetry;
using ISKI.IBKS.Infrastructure.Services.DataCollection;
using Wolverine;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace ISKI.IBKS.Infrastructure.BackgroundServices;

public sealed class PlcPollingWorker : BackgroundService
{
    private readonly IServiceScopeFactory _scopeFactory;
    private readonly IPlcClient _plcClient;
    private readonly IStationConfiguration _stationConfig;
    private readonly ILogger<PlcPollingWorker> _logger;

    public PlcPollingWorker(
        IServiceScopeFactory scopeFactory,
        IPlcClient plcClient,
        IStationConfiguration stationConfig,
        ILogger<PlcPollingWorker> logger)
    {
        _scopeFactory = scopeFactory;
        _plcClient = plcClient;
        _stationConfig = stationConfig;
        _logger = logger;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        _logger.LogInformation("PlcPollingWorker started.");

        while (!stoppingToken.IsCancellationRequested)
        {
            try
            {
                await PollAndProcessAsync(stoppingToken);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in PlcPollingWorker loop.");
            }

            await WaitForNextMinute(stoppingToken);
        }
    }

    private async Task PollAndProcessAsync(CancellationToken ct)
    {
        if (!_plcClient.IsConnected)
        {
            await _plcClient.ConnectAsync(_stationConfig.PlcIp, _stationConfig.PlcRack, _stationConfig.PlcSlot, ct);
        }

        if (!_plcClient.IsConnected)
        {
            _logger.LogWarning("PLC is not connected. Skipping this cycle.");
            return;
        }

        var snapshot = await ReadPlcDataAsync(ct);

        using var scope = _scopeFactory.CreateScope();
        var bus = scope.ServiceProvider.GetRequiredService<IMessageBus>();

        await bus.InvokeAsync(new ProcessTelemetryCommand(snapshot), ct);

        _logger.LogInformation("PLC data polled and ProcessTelemetryCommand dispatched.");
    }

    private async Task<PlcDataSnapshot> ReadPlcDataAsync(CancellationToken ct)
    {
        var snapshot = new PlcDataSnapshot
        {
            ReadTime = DateTime.Now,
            StationId = _stationConfig.StationId
        };

        try
        {
            byte[] db41Buffer = await _plcClient.ReadBytesAsync(41, 0, 168, ct);
            PlcDataMapper.MapAnalogData(db41Buffer, snapshot);

            byte[] db42Buffer = await _plcClient.ReadBytesAsync(42, 0, 3, ct);
            PlcDataMapper.MapDigitalData(db42Buffer, snapshot);

            byte[] db43Buffer = await _plcClient.ReadBytesAsync(43, 0, 19, ct);
            PlcDataMapper.MapSystemData(db43Buffer, snapshot);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error reading bytes from PLC.");
        }

        return snapshot;
    }

    private async Task WaitForNextMinute(CancellationToken ct)
    {
        var now = DateTime.Now;
        var nextMinute = now.AddMinutes(1).AddSeconds(-now.Second).AddMilliseconds(-now.Millisecond);
        var delay = nextMinute - now;

        if (delay > TimeSpan.Zero)
        {
            await Task.Delay(delay, ct);
        }
    }
}
