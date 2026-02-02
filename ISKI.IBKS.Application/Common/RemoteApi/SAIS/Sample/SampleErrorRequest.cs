using System.Text.Json.Serialization;

namespace ISKI.IBKS.Application.Common.RemoteApi.SAIS;

public sealed record SampleErrorRequest
{
    [JsonPropertyName("StationId")]
    public Guid StationId { get; init; }

    [JsonPropertyName("SampleCode")]
    public required string SampleCode { get; init; }

    [JsonPropertyName("ErrorCode")]
    public string? ErrorCode { get; init; }

    [JsonPropertyName("ErrorMessage")]
    public string? ErrorMessage { get; init; }
}

