using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace ISKI.IBKS.Application.Features.AnalogSensors.Dtos;

public sealed record ChannelDto
{
    public Guid? Id { get; init; }
    public string? Brand { get; init; }
    public string? BrandModel { get; init; }
    public string? FullName { get; init; }
    public string? Parameter { get; init; }
    public string? ParameterText { get; init; }
    public Guid? Unit { get; init; }
    public string? UnitText { get; init; }
    public bool IsActive { get; init; }
    public double? ChannelMinValue { get; init; }
    public double? ChannelMaxValue { get; init; }
    public short? ChannelNumber { get; init; }
    public double? CalibrationFormulaA { get; init; }
    public double? CalibrationFormulaB { get; init; }
    public string? SerialNumber { get; init; }
}
