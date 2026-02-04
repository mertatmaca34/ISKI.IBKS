using ISKI.IBKS.Domain.Common.Entities;
using System.Diagnostics.CodeAnalysis;

namespace ISKI.IBKS.Domain.Entities;

/// <summary>
/// Sensör verilerini temsil eder. SAIS API formatına uyumlu yapı.
/// Her kayıt 1 dakikalık periyodu temsil eder.
/// </summary>
public sealed class SensorData : AuditableEntity<Guid>
{
    public Guid StationId { get; private set; }
    public DateTime ReadTime { get; private set; }
    public int Period { get; private set; } = 1; // 1=1dk, 2=5dk, 4=30dk, 8=saatlik, 16=günlük
    public string? SoftwareVersion { get; private set; }

    // Analog Sensörler
    public double? TesisDebi { get; private set; }
    public int? TesisDebi_Status { get; private set; }

    public double? AkisHizi { get; private set; }
    public int? AkisHizi_Status { get; private set; }

    public double? Ph { get; private set; }
    public int? Ph_Status { get; private set; }

    public double? Iletkenlik { get; private set; }
    public int? Iletkenlik_Status { get; private set; }

    public double? CozunmusOksijen { get; private set; }
    public int? CozunmusOksijen_Status { get; private set; }

    public double? Koi { get; private set; }
    public int? Koi_Status { get; private set; }

    public double? Akm { get; private set; }
    public int? Akm_Status { get; private set; }

    public double? Sicaklik { get; private set; }
    public int? Sicaklik_Status { get; private set; }

    // Opsiyonel Sensörler
    public double? DesarjDebi { get; private set; }
    public int? DesarjDebi_Status { get; private set; }

    public double? HariciDebi { get; private set; }
    public int? HariciDebi_Status { get; private set; }

    public double? HariciDebi2 { get; private set; }
    public int? HariciDebi2_Status { get; private set; }

    // SAIS'e gönderildi mi?
    public bool IsSentToSais { get; private set; }
    public DateTime? SentToSaisAt { get; private set; }

    private SensorData() { }

    [SetsRequiredMembers]
    public SensorData(Guid stationId, DateTime readTime, int period = 1, string? softwareVersion = null)
    {
        Id = Guid.NewGuid();
        StationId = stationId;
        ReadTime = readTime;
        Period = period;
        SoftwareVersion = softwareVersion;
        IsSentToSais = false;
    }

    public void SetAnalogValues(
        double? tesisDebi, int? tesisDebiStatus,
        double? akisHizi, int? akisHiziStatus,
        double? ph, int? phStatus,
        double? iletkenlik, int? iletkenlikStatus,
        double? cozunmusOksijen, int? cozunmusOksijenStatus,
        double? koi, int? koiStatus,
        double? akm, int? akmStatus,
        double? sicaklik, int? sicaklikStatus)
    {
        TesisDebi = tesisDebi; TesisDebi_Status = tesisDebiStatus;
        AkisHizi = akisHizi; AkisHizi_Status = akisHiziStatus;
        Ph = ph; Ph_Status = phStatus;
        Iletkenlik = iletkenlik; Iletkenlik_Status = iletkenlikStatus;
        CozunmusOksijen = cozunmusOksijen; CozunmusOksijen_Status = cozunmusOksijenStatus;
        Koi = koi; Koi_Status = koiStatus;
        Akm = akm; Akm_Status = akmStatus;
        Sicaklik = sicaklik; Sicaklik_Status = sicaklikStatus;
    }

    public void SetOptionalValues(
        double? desarjDebi, int? desarjDebiStatus,
        double? hariciDebi, int? hariciDebiStatus,
        double? hariciDebi2, int? hariciDebi2Status)
    {
        DesarjDebi = desarjDebi; DesarjDebi_Status = desarjDebiStatus;
        HariciDebi = hariciDebi; HariciDebi_Status = hariciDebiStatus;
        HariciDebi2 = hariciDebi2; HariciDebi2_Status = hariciDebi2Status;
    }

    public void MarkAsSentToSais()
    {
        IsSentToSais = true;
        SentToSaisAt = DateTime.UtcNow;
    }
}
