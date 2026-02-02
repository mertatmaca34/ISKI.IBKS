using System.Text.Json.Serialization;

namespace ISKI.IBKS.Application.Common.RemoteApi.SAIS;

public sealed record SendDiagnosticRequest
{
    [JsonPropertyName("stationId")]
    public Guid StationId { get; init; }

    [JsonPropertyName("details")]
    public required string Details { get; init; }

    [JsonPropertyName("startDate")]
    public DateTime StartDate { get; init; }
}

