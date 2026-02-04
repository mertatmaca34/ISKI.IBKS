using System.Text.Json.Serialization;

namespace ISKI.IBKS.Infrastructure.RemoteApi.SAIS.Contracts.Station;

/// <summary>
/// İstasyon bilgileri sorgu yanıtı
/// </summary>
public sealed record StationInfoResponse
{
    [JsonPropertyName("StationId")]
    public Guid StationId { get; init; }

    [JsonPropertyName("Code")]
    public string? Code { get; init; }

    [JsonPropertyName("Name")]
    public string? Name { get; init; }

    [JsonPropertyName("DataPeriodMinute")]
    public int DataPeriodMinute { get; init; }

    [JsonPropertyName("LastDataDate")]
    public DateTime? LastDataDate { get; init; }

    [JsonPropertyName("ConnectionDomainAddress")]
    public string? ConnectionDomainAddress { get; init; }

    [JsonPropertyName("ConnectionPort")]
    public string? ConnectionPort { get; init; }

    [JsonPropertyName("ConnectionUser")]
    public string? ConnectionUser { get; init; }

    [JsonPropertyName("ConnectionPassword")]
    public string? ConnectionPassword { get; init; }

    [JsonPropertyName("Company")]
    public string? Company { get; init; }

    [JsonPropertyName("BirtDate")]
    public DateTime? BirtDate { get; init; }

    [JsonPropertyName("SetupDate")]
    public DateTime? SetupDate { get; init; }

    [JsonPropertyName("Adress")]
    public string? Adress { get; init; }

    [JsonPropertyName("Software")]
    public string? Software { get; init; }
}

/// <summary>
/// Host ve bağlantı bilgileri güncelleme isteği
/// </summary>
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
