using ISKI.IBKS.Domain.IoT;

namespace ISKI.IBKS.Application.Features.Telemetry.ProcessTelemetry;

public record ProcessTelemetryCommand(PlcDataSnapshot Snapshot);
