using System.Text.Json.Serialization;

namespace ISKI.IBKS.Application.Common.RemoteApi.SAIS.Data;

public sealed record MissingDatesResponse
{
    [JsonPropertyName("StartDate")]
    public DateTime StartDate { get; init; }

    [JsonPropertyName("EndDate")]
    public DateTime EndDate { get; init; }

    [JsonPropertyName("MissingDates")]
    public List<DateTime>? MissingDates { get; init; }
}

