using System.Text.Json.Serialization;

namespace ISKI.IBKS.Infrastructure.RemoteApi.SAIS.Contracts.Calibration;

/// <summary>
/// Kalibrasyon gönderme isteği
/// </summary>
public sealed record SendCalibrationRequest
{
    [JsonPropertyName("CalibrationDate")]
    public DateTime CalibrationDate { get; init; }

    [JsonPropertyName("StationId")]
    public Guid StationId { get; init; }

    [JsonPropertyName("DBColumnName")]
    public required string DBColumnName { get; init; }

    [JsonPropertyName("ZeroRef")]
    public double ZeroRef { get; init; }

    [JsonPropertyName("ZeroMeas")]
    public double ZeroMeas { get; init; }

    [JsonPropertyName("ZeroDiff")]
    public double ZeroDiff { get; init; }

    [JsonPropertyName("ZeroSTD")]
    public double ZeroSTD { get; init; }

    [JsonPropertyName("SpanRef")]
    public double SpanRef { get; init; }

    [JsonPropertyName("SpanMeas")]
    public double SpanMeas { get; init; }

    [JsonPropertyName("SpanDiff")]
    public double SpanDiff { get; init; }

    [JsonPropertyName("SpanSTD")]
    public double SpanSTD { get; init; }

    [JsonPropertyName("ResultFactor")]
    public double ResultFactor { get; init; }

    [JsonPropertyName("ResultZero")]
    public bool ResultZero { get; init; }

    [JsonPropertyName("ResultSpan")]
    public bool ResultSpan { get; init; }

    [JsonPropertyName("Result")]
    public bool Result { get; init; }
}
