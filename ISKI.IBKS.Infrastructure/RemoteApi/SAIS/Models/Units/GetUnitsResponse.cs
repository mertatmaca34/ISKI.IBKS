using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace ISKI.IBKS.Infrastructure.RemoteApi.SAIS.Models.Units;

public sealed record GetUnitsResponse
{
    [JsonPropertyName("id")]
    public Guid Id { get; init; }
    [JsonPropertyName("Name")]
    public string? Name { get; init; }
}
