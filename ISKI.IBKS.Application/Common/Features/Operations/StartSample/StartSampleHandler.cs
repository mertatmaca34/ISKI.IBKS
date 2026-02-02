using ISKI.IBKS.Application.Common.IoT.Plc;
using Microsoft.Extensions.Logging;

namespace ISKI.IBKS.Application.Common.Features.Operations.StartSample;

public class StartSampleHandler
{
    private readonly ILogger<StartSampleHandler> _logger;
    private readonly IPlcClient _plcClient;

    public StartSampleHandler(ILogger<StartSampleHandler> logger, IPlcClient plcClient)
    {
        _logger = logger;
        _plcClient = plcClient;
    }

    public async Task Handle(StartSampleCommand command, CancellationToken ct)
    {
        _logger.LogInformation("StartSample request received for station {StationId}", command.StationId);
        // TODO: Implement PLC trigger logic
        await Task.CompletedTask;
    }
}
