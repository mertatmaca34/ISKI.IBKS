using System;

namespace ISKI.IBKS.Application.Features.DigitalSensors.Dtos;

public sealed record DigitalReadingDto
{
    public string Key { get; init; } = string.Empty;
    public string Label { get; init; } = string.Empty;
    public bool? Value { get; init; }
}
