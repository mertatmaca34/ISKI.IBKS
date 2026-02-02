using System;
using System.Text.Json.Serialization;

namespace ISKI.IBKS.Application.Common.RemoteApi.SAIS;

public sealed record SendDataResponse
{
    [JsonPropertyName("id")]
    public Guid? Id { get; init; }
}
