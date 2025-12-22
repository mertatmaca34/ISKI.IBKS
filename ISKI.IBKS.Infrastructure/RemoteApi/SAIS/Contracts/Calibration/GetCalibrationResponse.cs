using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace ISKI.IBKS.Infrastructure.RemoteApi.SAIS.Contracts.Calibration;

public sealed class GetCalibrationResponse
{
    [JsonPropertyName("StationId")]
    public Guid StationId { get; init; }

    [JsonPropertyName("DBColumnName")]
    public string DBColumnName { get; init; } = string.Empty;

    [JsonPropertyName("CalibrationDate")]
    public DateTime CalibrationDate { get; init; }

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

    // JSON'da küçük harfli alanlar var:
    [JsonPropertyName("id")]
    public Guid Id { get; init; }

    [JsonPropertyName("created")]
    public DateTime Created { get; init; }

    [JsonPropertyName("changed")]
    public DateTime? Changed { get; init; }

    [JsonPropertyName("changedby")]
    public string? ChangedBy { get; init; }

    [JsonPropertyName("createdby")]
    public string? CreatedBy { get; init; }
}
