using ISKI.IBKS.Domain.IoT;

namespace ISKI.IBKS.Application.Common.Features.Telemetry.ProcessTelemetry;

public record ProcessTelemetryCommand(PlcDataSnapshot Snapshot);
