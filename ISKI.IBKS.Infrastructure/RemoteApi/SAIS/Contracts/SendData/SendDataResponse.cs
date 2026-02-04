using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace ISKI.IBKS.Infrastructure.RemoteApi.SAIS.Contracts.SendData;

public sealed record SendDataResponse
{
    [JsonPropertyName("Period")]
    public int? Period { get; init; }

    [JsonPropertyName("ReadTime")]
    public DateTimeOffset? ReadTime { get; init; }

    [JsonPropertyName("AKM")]
    public double? Akm { get; init; }

    [JsonPropertyName("AKM_Status")]
    public int? AkmStatus { get; init; }

    [JsonPropertyName("Debi")]
    public double? Debi { get; init; }

    [JsonPropertyName("Debi_Status")]
    public int? DebiStatus { get; init; }

    [JsonPropertyName("KOi")]
    public double? Koi { get; init; }

    [JsonPropertyName("KOi_Status")]
    public int? KoiStatus { get; init; }

    [JsonPropertyName("pH")]
    public double? Ph { get; init; }

    [JsonPropertyName("pH_Status")]
    public int? PhStatus { get; init; }

    [JsonPropertyName("AkisHizi")]
    public double? FlowVelocity { get; init; }

    [JsonPropertyName("AkisHizi_Status")]
    public int? FlowVelocityStatus { get; init; }

    [JsonPropertyName("CozunmusOksijen")]
    public double? DissolvedOxygen { get; init; }

    [JsonPropertyName("CozunmusOksijen_Status")]
    public int? DissolvedOxygenStatus { get; init; }

    [JsonPropertyName("Iletkenlik")]
    public double? Conductivity { get; init; }

    [JsonPropertyName("Iletkenlik_Status")]
    public int? ConductivityStatus { get; init; }

    [JsonPropertyName("Sicaklik")]
    public double? Temperature { get; init; }

    [JsonPropertyName("Sicaklik_Status")]
    public int? TemperatureStatus { get; init; }

    [JsonPropertyName("AKM_N")]
    public double? AkmN { get; init; }

    [JsonPropertyName("AKM_N_Status")]
    public int? AkmNStatus { get; init; }

    [JsonPropertyName("Debi_N")]
    public double? DebiN { get; init; }

    [JsonPropertyName("Debi_N_Status")]
    public int? DebiNStatus { get; init; }

    [JsonPropertyName("KOi_N")]
    public double? KoiN { get; init; }

    [JsonPropertyName("KOi_N_Status")]
    public int? KoiNStatus { get; init; }

    [JsonPropertyName("pH_N")]
    public double? PhN { get; init; }

    [JsonPropertyName("pH_N_Status")]
    public int? PhNStatus { get; init; }

    [JsonPropertyName("AkisHizi_N")]
    public double? FlowVelocityN { get; init; }

    [JsonPropertyName("AkisHizi_N_Status")]
    public int? FlowVelocityNStatus { get; init; }

    [JsonPropertyName("CozunmusOksijen_N")]
    public double? DissolvedOxygenN { get; init; }

    [JsonPropertyName("CozunmusOksijen_N_Status")]
    public int? DissolvedOxygenNStatus { get; init; }

    [JsonPropertyName("Iletkenlik_N")]
    public double? ConductivityN { get; init; }

    [JsonPropertyName("Iletkenlik_N_Status")]
    public int? ConductivityNStatus { get; init; }

    [JsonPropertyName("Sicaklik_N")]
    public double? TemperatureN { get; init; }

    [JsonPropertyName("Sicaklik_N_Status")]
    public int? TemperatureNStatus { get; init; }
}
