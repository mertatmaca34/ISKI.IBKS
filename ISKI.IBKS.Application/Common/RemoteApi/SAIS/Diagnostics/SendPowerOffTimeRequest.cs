using System.Text.Json.Serialization;

namespace ISKI.IBKS.Application.Common.RemoteApi.SAIS;

public sealed record SendPowerOffTimeRequest
{
    [JsonPropertyName("StationId")]
    public Guid StationId { get; init; }

    [JsonPropertyName("StartDate")]
    public DateTime StartDate { get; init; }

    [JsonPropertyName("EndDate")]
    public DateTime? EndDate { get; init; }
}

