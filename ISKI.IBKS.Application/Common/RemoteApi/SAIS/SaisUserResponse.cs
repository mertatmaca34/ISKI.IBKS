using System.Text.Json.Serialization;

namespace ISKI.IBKS.Application.Common.RemoteApi.SAIS;

public sealed record SaisUserResponse
{
    [JsonPropertyName("Id")]
    public int? Id { get; init; }

    [JsonPropertyName("UserName")]
    public string? UserName { get; init; }

    [JsonPropertyName("FullName")]
    public string? FullName { get; init; }
}

