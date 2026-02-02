using System.Text.Json.Serialization;

namespace ISKI.IBKS.Application.Common.RemoteApi.SAIS;

public sealed record SampleRequestStartRequest
{
    [JsonPropertyName("StationId")]
    public Guid StationId { get; init; }

    [JsonPropertyName("SampleCode")]
    public required string SampleCode { get; init; }

    [JsonPropertyName("StartDate")]
    public DateTime StartDate { get; init; }
}

