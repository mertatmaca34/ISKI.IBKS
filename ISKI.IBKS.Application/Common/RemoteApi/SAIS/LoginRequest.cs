using System.Text.Json.Serialization;

namespace ISKI.IBKS.Application.Common.RemoteApi.SAIS;

public sealed record LoginRequest
{
    [JsonPropertyName("username")]
    public string UserName { get; init; } = string.Empty;
    [JsonPropertyName("password")]
    public string Password { get; init; } = string.Empty;
}

