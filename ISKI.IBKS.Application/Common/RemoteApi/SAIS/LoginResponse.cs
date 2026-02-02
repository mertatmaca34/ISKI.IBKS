using System.Text.Json.Serialization;

namespace ISKI.IBKS.Application.Common.RemoteApi.SAIS;

public sealed class LoginResponse
{
    [JsonPropertyName("TicketId")]
    public Guid? TicketId { get; init; }

    [JsonPropertyName("DeviceId")]
    public Guid? DeviceId { get; init; }

    [JsonPropertyName("User")]
    public SaisUserResponse? User { get; init; }
}

