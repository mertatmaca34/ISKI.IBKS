using System.Text.Json.Serialization;

namespace ISKI.IBKS.Infrastructure.RemoteApi.SAIS.Contracts.Diagnostics;

/// <summary>
/// Diagnostik tip bilgisi
/// </summary>
public sealed record DiagnosticTypeResponse
{
    [JsonPropertyName("DiagnosticTypeNo")]
    public int DiagnosticTypeNo { get; init; }

    [JsonPropertyName("DiagnosticTypeName")]
    public string? DiagnosticTypeName { get; init; }

    [JsonPropertyName("DiagnosticLevel")]
    public int DiagnosticLevel { get; init; }

    [JsonPropertyName("DiagnosticLevel_Title")]
    public string? DiagnosticLevelTitle { get; init; }
}

/// <summary>
/// Diagnostik gönderme isteği
/// </summary>
public sealed record SendDiagnosticRequest
{
    [JsonPropertyName("stationId")]
    public Guid StationId { get; init; }

    [JsonPropertyName("details")]
    public required string Details { get; init; }

    [JsonPropertyName("startDate")]
    public DateTime StartDate { get; init; }
}

/// <summary>
/// Tip numarası ile diagnostik gönderme isteği
/// </summary>
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

/// <summary>
/// Enerji kesintisi bildirimi
/// </summary>
public sealed record SendPowerOffTimeRequest
{
    [JsonPropertyName("StationId")]
    public Guid StationId { get; init; }

    [JsonPropertyName("StartDate")]
    public DateTime StartDate { get; init; }

    [JsonPropertyName("EndDate")]
    public DateTime? EndDate { get; init; }
}
