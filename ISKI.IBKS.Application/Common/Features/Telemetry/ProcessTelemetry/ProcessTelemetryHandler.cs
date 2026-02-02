using ISKI.IBKS.Application.Common.IoT;
using ISKI.IBKS.Application.Common.RemoteApi.SAIS;
using ISKI.IBKS.Domain.IoT;
using Microsoft.Extensions.Logging;

namespace ISKI.IBKS.Application.Common.Features.Telemetry.ProcessTelemetry;

public class ProcessTelemetryHandler
{
    private readonly ILogger<ProcessTelemetryHandler> _logger;
    private readonly ISaisApiClient _saisClient;

    public ProcessTelemetryHandler(
        ILogger<ProcessTelemetryHandler> logger,
        ISaisApiClient saisClient)
    {
        _logger = logger;
        _saisClient = saisClient;
    }

    public async Task Handle(ProcessTelemetryCommand command, CancellationToken ct)
    {
        _logger.LogInformation("Processing telemetry for snapshot at {Time}", command.Snapshot.SystemTime);
        await Task.CompletedTask;
    }
}
