using System.Text.Json.Serialization;

namespace ISKI.IBKS.WebAPI.Contracts;

/// <summary>
/// Standard response envelope for all SAIS API responses.
/// Matches the format expected by SAIS: { result, message, objects }
/// </summary>
public class SaisApiResponse<T>
{
    [JsonPropertyName("result")]
    public bool Result { get; set; }

    [JsonPropertyName("message")]
    public string? Message { get; set; }

    [JsonPropertyName("objects")]
    public T? Objects { get; set; }

    public static SaisApiResponse<T> Success(T? data, string? message = null)
    {
        return new SaisApiResponse<T>
        {
            Result = true,
            Message = message,
            Objects = data
        };
    }

    public static SaisApiResponse<T> Failure(string message)
    {
        return new SaisApiResponse<T>
        {
            Result = false,
            Message = message,
            Objects = default
        };
    }
}
