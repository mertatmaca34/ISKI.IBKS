using System;

namespace ISKI.IBKS.Application.Features.HealthSummary.Dtos;

public sealed record HealthSummaryDto
{
    public bool PlcConnected { get; init; }
    public bool ApiHealthy { get; init; }
    public DateTime? LastPhCalibration { get; init; }
    public DateTime? LastIletkenlikCalibration { get; init; }
}
