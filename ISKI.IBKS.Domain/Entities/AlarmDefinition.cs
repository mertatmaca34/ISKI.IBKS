using ISKI.IBKS.Domain.Common.Entities;

namespace ISKI.IBKS.Domain.Entities;

/// <summary>
/// Alarm tanımlarını temsil eder.
/// Eşik değerleri aşıldığında bildirim tetiklenir.
/// </summary>
public sealed class AlarmDefinition : AuditableEntity<Guid>
{
    public string Name { get; private set; } = string.Empty;
    public string Description { get; private set; } = string.Empty;
    
    /// <summary>
    /// İlişkili sensör adı (örn: Ph, Koi, Akm)
    /// </summary>
    public string SensorName { get; private set; } = string.Empty;
    
    /// <summary>
    /// Alarm türü: Threshold (eşik), Digital (dijital sensör), System (sistem)
    /// </summary>
    public AlarmType Type { get; private set; }
    
    /// <summary>
    /// Alt eşik değeri (null ise kontrol edilmez)
    /// </summary>
    public double? MinThreshold { get; private set; }
    
    /// <summary>
    /// Üst eşik değeri (null ise kontrol edilmez)
    /// </summary>
    public double? MaxThreshold { get; private set; }
    
    /// <summary>
    /// Dijital sensörler için beklenen değer (true/false)
    /// </summary>
    public bool? ExpectedDigitalValue { get; private set; }
    
    /// <summary>
    /// Alarm aktif mi?
    /// </summary>
    public bool IsActive { get; private set; } = true;
    
    /// <summary>
    /// Aynı alarm için tekrar mail göndermek için beklenecek süre (dakika).
    /// Seçenekler: 1, 10, 30, 60 (1 saat), 180 (3 saat), 1440 (1 gün)
    /// </summary>
    public int CooldownMinutes { get; private set; } = 30;

    private AlarmDefinition() { }

    public static AlarmDefinition CreateThresholdAlarm(
        string name,
        string sensorName,
        double? minThreshold,
        double? maxThreshold,
        string description = "",
        int cooldownMinutes = 30)
    {
        return new AlarmDefinition
        {
            Id = Guid.NewGuid(),
            Name = name,
            SensorName = sensorName,
            Type = AlarmType.Threshold,
            MinThreshold = minThreshold,
            MaxThreshold = maxThreshold,
            Description = description,
            CooldownMinutes = cooldownMinutes,
            IsActive = true
        };
    }

    public static AlarmDefinition CreateDigitalAlarm(
        string name,
        string sensorName,
        bool expectedValue,
        string description = "",
        int cooldownMinutes = 30)
    {
        return new AlarmDefinition
        {
            Id = Guid.NewGuid(),
            Name = name,
            SensorName = sensorName,
            Type = AlarmType.Digital,
            ExpectedDigitalValue = expectedValue,
            Description = description,
            CooldownMinutes = cooldownMinutes,
            IsActive = true
        };
    }

    public void Deactivate() => IsActive = false;
    public void Activate() => IsActive = true;

    public void UpdateThresholds(double? minThreshold, double? maxThreshold)
    {
        MinThreshold = minThreshold;
        MaxThreshold = maxThreshold;
    }

    public void Update(string name, string description, string sensorName, AlarmType type, 
        double? minThreshold, double? maxThreshold, bool? expectedDigitalValue, 
        int cooldownMinutes, bool isActive)
    {
        Name = name;
        Description = description;
        SensorName = sensorName;
        Type = type;
        MinThreshold = minThreshold;
        MaxThreshold = maxThreshold;
        ExpectedDigitalValue = expectedDigitalValue;
        CooldownMinutes = cooldownMinutes;
        IsActive = isActive;
    }
}

public enum AlarmType
{
    Threshold = 0,  // Eşik değer aşımı
    Digital = 1,    // Dijital sensör değişimi
    System = 2      // Sistem alarmı (bağlantı, enerji vb.)
}

// AlarmPriority enum is kept for backward compatibility with AlarmEvent
public enum AlarmPriority
{
    Low = 0,
    Medium = 1,
    High = 2,
    Critical = 3
}
