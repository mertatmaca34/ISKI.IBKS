using System.Text.Json.Serialization;

namespace ISKI.IBKS.Application.Common.RemoteApi.SAIS;

public sealed record SendDiagnosticWithTypeRequest
{
    [JsonPropertyName("StationId")]
    public Guid StationId { get; init; }

    [JsonPropertyName("Details")]
    public string? Details { get; init; }

    [JsonPropertyName("DiagnosticTypeNo")]
    public required string DiagnosticTypeNo { get; init; }

    [JsonPropertyName("DiagnosticDate")]
    public DateTime DiagnosticDate { get; init; }
}

