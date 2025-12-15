using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace ISKI.IBKS.Infrastructure.RemoteApi.SAIS.Contracts.SendData;

public sealed record SendDataRequest
{
    [JsonPropertyName("Stationid")]
    public Guid StationId { get; init; }

    [JsonPropertyName("Readtime")]
    public DateTime ReadTime { get; init; }

    [JsonPropertyName("SoftwareVersion")]
    public required string SoftwareVersion { get; init; } 

    [JsonPropertyName("AkisHizi")]
    public double AkisHizi { get; init; }

    [JsonPropertyName("AKM")]
    public double AKM { get; init; }

    [JsonPropertyName("CozunmusOksijen")]
    public double CozunmusOksijen { get; init; }

    [JsonPropertyName("Debi")]
    public double Debi { get; init; }

    [JsonPropertyName("DesarjDebi")]
    public double DesarjDebi { get; init; }

    [JsonPropertyName("HariciDebi")]
    public double HariciDebi { get; init; }

    [JsonPropertyName("HariciDebi2")]
    public double HariciDebi2 { get; init; }

    [JsonPropertyName("KOi")]
    public double KOi { get; init; }

    [JsonPropertyName("pH")]
    public double Ph { get; init; }

    [JsonPropertyName("Sicaklik")]
    public double Sicaklik { get; init; }

    [JsonPropertyName("Iletkenlik")]
    public double Iletkenlik { get; init; }

    [JsonPropertyName("Period")]
    public int Period { get; init; }

    [JsonPropertyName("AkisHizi_Status")]
    public int AkisHiziStatus { get; init; }

    [JsonPropertyName("AKM_Status")]
    public int AKMStatus { get; init; }

    [JsonPropertyName("CozunmusOksijen_Status")]
    public int CozunmusOksijenStatus { get; init; }

    [JsonPropertyName("Debi_Status")]
    public int DebiStatus { get; init; }

    [JsonPropertyName("DesarjDebi_Status")]
    public int DesarjDebiStatus { get; init; }

    [JsonPropertyName("HariciDebi_Status")]
    public int HariciDebiStatus { get; init; }

    [JsonPropertyName("HariciDebi2_Status")]
    public int HariciDebi2Status { get; init; }

    [JsonPropertyName("KOi_Status")]
    public int KOiStatus { get; init; }

    [JsonPropertyName("pH_Status")]
    public int PhStatus { get; init; }

    [JsonPropertyName("Sicaklik_Status")]
    public int SicaklikStatus { get; init; }

    [JsonPropertyName("Iletkenlik_Status")]
    public required string IletkenlikStatus { get; init; }
}
