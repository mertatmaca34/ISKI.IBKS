using Microsoft.Extensions.Logging;

namespace ISKI.IBKS.Application.Common.Features.Operations.StartSample;

public class TriggerPlcSampleHandler
{
    private readonly ILogger<TriggerPlcSampleHandler> _logger;

    public TriggerPlcSampleHandler(ILogger<TriggerPlcSampleHandler> logger)
    {
        _logger = logger;
    }

    public async Task Handle(StartSampleCommand command, CancellationToken ct)
    {
        _logger.LogInformation("Triggering PLC Sample for {StationId}", command.StationId);
        await Task.CompletedTask;
    }
}
