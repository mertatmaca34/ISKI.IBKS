using ISKI.IBKS.Application.Features.AnalogSensors.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ISKI.IBKS.Application.Features.AnalogSensors.Dtos;

public sealed record ChannelReadingDto
{
    public Guid? ChannelId { get; set; }
    public string? ChannelName { get; set; }
    public double? Value { get; set; }
    public string? UnitName { get; set; }
    public AnalogSignalStatus? Status { get; set; }
    public string? Format { get; set; }
}