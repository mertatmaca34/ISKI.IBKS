using System.Text.Json.Serialization;

namespace ISKI.IBKS.Application.Common.RemoteApi.SAIS.Data;

public sealed record ParameterResponse
{
    [JsonPropertyName("Parameter")]
    public string? Parameter { get; init; }

    [JsonPropertyName("ParameterText")]
    public string? ParameterText { get; init; }

    [JsonPropertyName("MonitorType")]
    public int MonitorType { get; init; }

    [JsonPropertyName("MonitorTypeText")]
    public string? MonitorTypeText { get; init; }
}

