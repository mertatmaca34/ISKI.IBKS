using System.Text.Json.Serialization;

namespace ISKI.IBKS.Infrastructure.RemoteApi.SAIS.Contracts.Data;

/// <summary>
/// Veri sorgulama isteği (iki tarih arası)
/// </summary>
public sealed record GetDataRequest
{
    public Guid StationId { get; init; }
    public int Period { get; init; }
    public DateTime StartDate { get; init; }
    public DateTime EndDate { get; init; }
}

/// <summary>
/// Veri durum kodu
/// </summary>
public sealed record DataStatusResponse
{
    [JsonPropertyName("StatusCode")]
    public int StatusCode { get; init; }

    [JsonPropertyName("StatusName")]
    public string? StatusName { get; init; }

    [JsonPropertyName("StatusDescription")]
    public string? StatusDescription { get; init; }

    [JsonPropertyName("IsValid")]
    public bool IsValid { get; init; }
}

/// <summary>
/// Parametre bilgisi
/// </summary>
public sealed record ParameterResponse
{
    [JsonPropertyName("Parameter")]
    public string? Parameter { get; init; }

    [JsonPropertyName("ParameterText")]
    public string? ParameterText { get; init; }

    [JsonPropertyName("MonitorType")]
    public int MonitorType { get; init; }

    [JsonPropertyName("MonitorTypeText")]
    public string? MonitorTypeText { get; init; }
}

/// <summary>
/// Eksik veri yanıtı
/// </summary>
public sealed record MissingDatesResponse
{
    [JsonPropertyName("StartDate")]
    public DateTime StartDate { get; init; }

    [JsonPropertyName("EndDate")]
    public DateTime EndDate { get; init; }

    [JsonPropertyName("MissingDates")]
    public List<DateTime>? MissingDates { get; init; }
}
