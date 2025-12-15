using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace ISKI.IBKS.Infrastructure.RemoteApi.SAIS.Contracts;

public sealed record SaisResultEnvelope<T>
{
    [JsonPropertyName("result")]
    public bool Result { get; init; }

    [JsonPropertyName("message")]
    public string? Message { get; init; }

    [JsonPropertyName("objects")]
    public T? Objects { get; init; }
}
