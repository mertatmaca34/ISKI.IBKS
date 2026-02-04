using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace ISKI.IBKS.Infrastructure.RemoteApi.SAIS.Contracts.SendData;

/// <summary>
/// SAIS API'ye veri göndermek için kullanılan request modeli.
/// Dokümantasyon: Sadece ölçülen parametreler gönderilmelidir.
/// Her parametre için değer varsa mutlaka status bilgisi de gönderilmelidir.
/// </summary>
public sealed record SendDataRequest
{
    [JsonPropertyName("Stationid")]
    public Guid StationId { get; init; }

    [JsonPropertyName("Readtime")]
    public DateTime ReadTime { get; init; }

    [JsonPropertyName("SoftwareVersion")]
    public string SoftwareVersion { get; init; } = string.Empty;

    [JsonPropertyName("Period")]
    public int Period { get; init; } = 1; // SAIS sadece 1 dakikalık periyot kabul eder

    // Analog Sensörler - Nullable çünkü sadece ölçülen parametreler gönderilmeli
    [JsonPropertyName("AkisHizi")]
    public double? AkisHizi { get; init; }

    [JsonPropertyName("AkisHizi_Status")]
    public int? AkisHizi_Status { get; init; }

    [JsonPropertyName("AKM")]
    public double? AKM { get; init; }

    [JsonPropertyName("AKM_Status")]
    public int? AKM_Status { get; init; }

    [JsonPropertyName("CozunmusOksijen")]
    public double? CozunmusOksijen { get; init; }

    [JsonPropertyName("CozunmusOksijen_Status")]
    public int? CozunmusOksijen_Status { get; init; }

    [JsonPropertyName("Debi")]
    public double? Debi { get; init; }

    [JsonPropertyName("Debi_Status")]
    public int? Debi_Status { get; init; }

    [JsonPropertyName("DesarjDebi")]
    public double? DesarjDebi { get; init; }

    [JsonPropertyName("DesarjDebi_Status")]
    public int? DesarjDebi_Status { get; init; }

    [JsonPropertyName("HariciDebi")]
    public double? HariciDebi { get; init; }

    [JsonPropertyName("HariciDebi_Status")]
    public int? HariciDebi_Status { get; init; }

    [JsonPropertyName("HariciDebi2")]
    public double? HariciDebi2 { get; init; }

    [JsonPropertyName("HariciDebi2_Status")]
    public int? HariciDebi2_Status { get; init; }

    [JsonPropertyName("KOi")]
    public double? KOi { get; init; }

    [JsonPropertyName("KOi_Status")]
    public int? KOi_Status { get; init; }

    [JsonPropertyName("pH")]
    public double? pH { get; init; }

    [JsonPropertyName("pH_Status")]
    public int? pH_Status { get; init; }

    [JsonPropertyName("Sicaklik")]
    public double? Sicaklik { get; init; }

    [JsonPropertyName("Sicaklik_Status")]
    public int? Sicaklik_Status { get; init; }

    [JsonPropertyName("Iletkenlik")]
    public double? Iletkenlik { get; init; }

    [JsonPropertyName("Iletkenlik_Status")]
    public int? Iletkenlik_Status { get; init; }
}
