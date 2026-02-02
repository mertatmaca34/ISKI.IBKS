using System.Text.Json.Serialization;

namespace ISKI.IBKS.Application.Common.RemoteApi.SAIS;

public sealed record SendHostChangedRequest
{
    [JsonPropertyName("StationId")]
    public Guid StationId { get; init; }

    [JsonPropertyName("ConnectionUser")]
    public required string ConnectionUser { get; init; }

    [JsonPropertyName("ConnectionPassword")]
    public required string ConnectionPassword { get; init; }

    [JsonPropertyName("ConnectionDomainAddress")]
    public required string ConnectionDomainAddress { get; init; }

    [JsonPropertyName("ConnectionPort")]
    public required string ConnectionPort { get; init; }
}

