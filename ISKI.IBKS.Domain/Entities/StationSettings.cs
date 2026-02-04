using System.Diagnostics.CodeAnalysis;

namespace ISKI.IBKS.Domain.Entities;

/// <summary>
/// İstasyon Ayarları - Her tesis için yapılandırma bilgileri
/// </summary>
public sealed class StationSettings
{
    public Guid Id { get; private set; }
    
    /// <summary>SAIS İstasyon ID (Bakanlık tarafından verilen)</summary>
    public required Guid StationId { get; set; }
    
    /// <summary>İstasyon Kodu</summary>
    public string? Code { get; private set; }
    
    /// <summary>İstasyon Adı</summary>
    public required string Name { get; set; }
    
    /// <summary>Veri Periyodu (dakika cinsinden, genellikle 1)</summary>
    public int DataPeriodMinute { get; private set; } = 1;
    
    /// <summary>En son merkeze gönderilen veri tarihi</summary>
    public DateTime? LastDataDate { get; private set; }
    
    /// <summary>Kabin yazılımı erişim host/IP adresi</summary>
    public string? ConnectionDomainAddress { get; private set; }
    
    /// <summary>Kabin yazılımı erişim port numarası</summary>
    public string? ConnectionPort { get; private set; }
    
    /// <summary>SAIS basic auth kullanıcı adı</summary>
    public string? ConnectionUser { get; private set; }
    
    /// <summary>SAIS basic auth şifresi</summary>
    public string? ConnectionPassword { get; private set; }
    
    /// <summary>Firma/Tesis adı</summary>
    public string? Company { get; private set; }
    
    /// <summary>Doğum tarihi (tesis kurulum tarihi)</summary>
    public DateTime? BirthDate { get; private set; }
    
    /// <summary>Kurulum tarihi</summary>
    public DateTime? SetupDate { get; private set; }
    
    /// <summary>Adres bilgisi</summary>
    public string? Address { get; private set; }
    
    /// <summary>Kullanılan yazılım bilgisi</summary>
    public string? Software { get; private set; }
    
    // PLC Bağlantı Ayarları
    public string PlcIpAddress { get; private set; } = "10.33.3.253";
    public int PlcRack { get; private set; } = 0;
    public int PlcSlot { get; private set; } = 1;
    
    // Kalibrasyon Ayarları
    public int PhZeroDuration { get; private set; } = 60;
    public double PhZeroReference { get; private set; } = 7.0;
    public int PhSpanDuration { get; private set; } = 60;
    public double PhSpanReference { get; private set; } = 4.0;
    
    public int ConductivityZeroDuration { get; private set; } = 60;
    public double ConductivityZeroReference { get; private set; } = 0;
    public int ConductivitySpanDuration { get; private set; } = 60;
    public double ConductivitySpanReference { get; private set; } = 1413;
    
    public int AkmZeroDuration { get; private set; } = 60;
    public double AkmZeroReference { get; private set; } = 0;
    
    public int KoiZeroDuration { get; private set; } = 60;
    public double KoiZeroReference { get; private set; } = 0;
    
    /// <summary>Seçili sensörler listesi (JSON olarak saklanır)</summary>
    public string? SelectedSensorsJson { get; private set; }
    
    public DateTime CreatedAt { get; private set; } = DateTime.UtcNow;
    public DateTime? UpdatedAt { get; private set; }
    public bool IsDeleted { get; private set; }

    private StationSettings() { }

    [SetsRequiredMembers]
    public StationSettings(Guid stationId, string name)
    {
        Id = Guid.NewGuid();
        StationId = stationId;
        Name = name;
    }

    public void UpdateFromSais(string? code, int dataPeriodMinute, DateTime? lastDataDate,
        string? connectionDomainAddress, string? connectionPort, string? connectionUser,
        string? connectionPassword, string? company, DateTime? birthDate, DateTime? setupDate,
        string? address, string? software)
    {
        Code = code;
        DataPeriodMinute = dataPeriodMinute;
        LastDataDate = lastDataDate;
        ConnectionDomainAddress = connectionDomainAddress;
        ConnectionPort = connectionPort;
        ConnectionUser = connectionUser;
        ConnectionPassword = connectionPassword;
        Company = company;
        BirthDate = birthDate;
        SetupDate = setupDate;
        Address = address;
        Software = software;
        UpdatedAt = DateTime.UtcNow;
    }

    public void UpdatePlcSettings(string ipAddress, int rack, int slot)
    {
        PlcIpAddress = ipAddress;
        PlcRack = rack;
        PlcSlot = slot;
        UpdatedAt = DateTime.UtcNow;
    }

    public void UpdateCalibrationSettings(
        int phZeroDuration, double phZeroRef, int phSpanDuration, double phSpanRef,
        int condZeroDuration, double condZeroRef, int condSpanDuration, double condSpanRef,
        int akmZeroDuration, double akmZeroRef, int koiZeroDuration, double koiZeroRef)
    {
        PhZeroDuration = phZeroDuration;
        PhZeroReference = phZeroRef;
        PhSpanDuration = phSpanDuration;
        PhSpanReference = phSpanRef;
        ConductivityZeroDuration = condZeroDuration;
        ConductivityZeroReference = condZeroRef;
        ConductivitySpanDuration = condSpanDuration;
        ConductivitySpanReference = condSpanRef;
        AkmZeroDuration = akmZeroDuration;
        AkmZeroReference = akmZeroRef;
        KoiZeroDuration = koiZeroDuration;
        KoiZeroReference = koiZeroRef;
        UpdatedAt = DateTime.UtcNow;
    }

    public void UpdateLastDataDate(DateTime lastDataDate)
    {
        LastDataDate = lastDataDate;
        UpdatedAt = DateTime.UtcNow;
    }

    /// <summary>Seçili sensörlerin listesini döndürür</summary>
    public List<string> GetSelectedSensors()
    {
        if (string.IsNullOrWhiteSpace(SelectedSensorsJson))
            return new List<string>();
        return System.Text.Json.JsonSerializer.Deserialize<List<string>>(SelectedSensorsJson) ?? new List<string>();
    }

    /// <summary>Seçili sensörleri günceller</summary>
    public void UpdateSelectedSensors(IEnumerable<string> sensors)
    {
        SelectedSensorsJson = System.Text.Json.JsonSerializer.Serialize(sensors.ToList());
        UpdatedAt = DateTime.UtcNow;
    }
}
