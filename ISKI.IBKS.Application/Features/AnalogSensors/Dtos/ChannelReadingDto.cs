using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ISKI.IBKS.Application.Features.AnalogSensors.Dtos;

public sealed record ChannelReadingDto
{
    public Guid? ChannelId { get; init; }
    public string? ChannelName { get; init; }
    public double? Value { get; init; }
    public string? UnitName { get; init; }
    public int? Status { get; init; }
}