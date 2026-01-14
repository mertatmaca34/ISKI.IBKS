using System.Text.Json.Serialization;

namespace ISKI.IBKS.Infrastructure.RemoteApi.SAIS.Contracts.Sample;

/// <summary>
/// Numune alım başlatma isteği (tetikleme ile)
/// </summary>
public sealed record SampleRequestStartRequest
{
    [JsonPropertyName("StationId")]
    public Guid StationId { get; init; }

    [JsonPropertyName("SampleCode")]
    public required string SampleCode { get; init; }

    [JsonPropertyName("StartDate")]
    public DateTime StartDate { get; init; }
}

/// <summary>
/// Sınır aşımı nedeniyle numune alımı isteği
/// </summary>
public sealed record SampleLimitOverRequest
{
    [JsonPropertyName("StationId")]
    public Guid StationId { get; init; }

    [JsonPropertyName("Parameter")]
    public required string Parameter { get; init; }
}

/// <summary>
/// Numune tamamlandı bildirimi
/// </summary>
public sealed record SampleCompleteRequest
{
    [JsonPropertyName("StationId")]
    public Guid StationId { get; init; }

    [JsonPropertyName("SampleCode")]
    public required string SampleCode { get; init; }
}

/// <summary>
/// Numune hata bildirimi
/// </summary>
public sealed record SampleErrorRequest
{
    [JsonPropertyName("StationId")]
    public Guid StationId { get; init; }

    [JsonPropertyName("SampleCode")]
    public required string SampleCode { get; init; }
}
