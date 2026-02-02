using System.Text.Json.Serialization;

namespace ISKI.IBKS.Application.Common.RemoteApi.SAIS;

public sealed record SaisResultEnvelope<T>
{
    [JsonPropertyName("result")]
    public bool Result { get; init; }

    [JsonPropertyName("message")]
    public string? Message { get; init; }

    [JsonPropertyName("objects")]
    public T? Objects { get; init; }

    public void EnsureSuccess()
    {
        if (!Result)
        {
            throw new Exception($"SAIS API error: {Message}");
        }
    }
}
