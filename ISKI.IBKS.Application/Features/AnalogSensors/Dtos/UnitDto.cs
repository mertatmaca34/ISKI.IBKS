using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace ISKI.IBKS.Application.Features.AnalogSensors.Dtos;

public sealed record UnitDto
{
    public Guid? Id { get; init; }
    public string? Name { get; init; }
}
