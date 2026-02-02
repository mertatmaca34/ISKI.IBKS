using System.Text.Json.Serialization;

namespace ISKI.IBKS.Application.Common.RemoteApi.SAIS.Data;

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

