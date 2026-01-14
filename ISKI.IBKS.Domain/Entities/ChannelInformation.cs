using System.Diagnostics.CodeAnalysis;

namespace ISKI.IBKS.Domain.Entities;

/// <summary>
/// SAIS Kanal Bilgileri - Her sensör için kanal yapılandırması
/// </summary>
public sealed class ChannelInformation
{
    public Guid Id { get; private set; }
    public Guid StationId { get; private set; }
    
    /// <summary>Marka adı</summary>
    public string? Brand { get; private set; }
    
    /// <summary>Model bilgisi</summary>
    public string? BrandModel { get; private set; }
    
    /// <summary>Kanal tam ismi (örn: "INV2020101812423644 | AKM Ölçer")</summary>
    public required string FullName { get; set; }
    
    /// <summary>Parametre adı (veritabanı kolon adı: pH, AKM, KOi, Debi vb.)</summary>
    public required string Parameter { get; set; }
    
    /// <summary>Parametre gösterim metni</summary>
    public string? ParameterText { get; set; }
    
    /// <summary>Birim ID (SAIS'ten alınan)</summary>
    public Guid? UnitId { get; private set; }
    
    /// <summary>Birim metni (örn: "mg/L", "m³/saat")</summary>
    public string? UnitText { get; private set; }
    
    /// <summary>Kanal aktif mi?</summary>
    public bool IsActive { get; private set; } = true;
    
    /// <summary>Minimum ölçüm değeri</summary>
    public double ChannelMinValue { get; private set; }
    
    /// <summary>Maksimum ölçüm değeri</summary>
    public double ChannelMaxValue { get; private set; }
    
    /// <summary>Kanal numarası</summary>
    public short ChannelNumber { get; private set; }
    
    /// <summary>Kalibrasyon formülü A değeri (y = A + Bx toplam)</summary>
    public double CalibrationFormulaA { get; private set; }
    
    /// <summary>Kalibrasyon formülü B değeri (y = A + Bx çarpan)</summary>
    public double CalibrationFormulaB { get; private set; } = 1.0;
    
    /// <summary>Analizör seri numarası</summary>
    public string? SerialNumber { get; private set; }
    
    public DateTime CreatedAt { get; private set; } = DateTime.UtcNow;
    public DateTime? UpdatedAt { get; private set; }
    public bool IsDeleted { get; private set; }
    public DateTime? DeletedAt { get; private set; }

    private ChannelInformation() { }

    [SetsRequiredMembers]
    public ChannelInformation(Guid stationId, string fullName, string parameter, short channelNumber)
    {
        Id = Guid.NewGuid();
        StationId = stationId;
        FullName = fullName;
        Parameter = parameter;
        ChannelNumber = channelNumber;
    }

    public void UpdateFromSais(string? brand, string? brandModel, string? unitText, Guid? unitId,
        bool isActive, double minValue, double maxValue, double formulaA, double formulaB, string? serialNumber)
    {
        Brand = brand;
        BrandModel = brandModel;
        UnitText = unitText;
        UnitId = unitId;
        IsActive = isActive;
        ChannelMinValue = minValue;
        ChannelMaxValue = maxValue;
        CalibrationFormulaA = formulaA;
        CalibrationFormulaB = formulaB;
        SerialNumber = serialNumber;
        UpdatedAt = DateTime.UtcNow;
    }

    public void Delete()
    {
        IsDeleted = true;
        DeletedAt = DateTime.UtcNow;
    }
}
