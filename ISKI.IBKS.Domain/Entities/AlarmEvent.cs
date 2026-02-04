using ISKI.IBKS.Domain.Common.Entities;

namespace ISKI.IBKS.Domain.Entities;

/// <summary>
/// Oluşan alarm olaylarını temsil eder.
/// Alarm geçmişi için kullanılır.
/// </summary>
public sealed class AlarmEvent : Entity<Guid>
{
    public Guid StationId { get; private set; }
    public Guid AlarmDefinitionId { get; private set; }
    
    /// <summary>
    /// Alarm oluşma zamanı
    /// </summary>
    public DateTime OccurredAt { get; private set; }
    
    /// <summary>
    /// Alarm kapanma zamanı (null ise hala aktif)
    /// </summary>
    public DateTime? ResolvedAt { get; private set; }
    
    /// <summary>
    /// Alarm tetikleyen değer
    /// </summary>
    public double? TriggerValue { get; private set; }
    
    /// <summary>
    /// İlgili sensör adı
    /// </summary>
    public string SensorName { get; private set; } = string.Empty;
    
    /// <summary>
    /// Alarm mesajı
    /// </summary>
    public string Message { get; private set; } = string.Empty;
    
    /// <summary>
    /// Bildirim gönderildi mi?
    /// </summary>
    public bool NotificationSent { get; private set; }
    
    /// <summary>
    /// Bildirim gönderim zamanı
    /// </summary>
    public DateTime? NotificationSentAt { get; private set; }
    
    /// <summary>
    /// Alarm önceliği
    /// </summary>
    public AlarmPriority Priority { get; private set; }

    private AlarmEvent() { }

    public AlarmEvent(
        Guid stationId,
        Guid alarmDefinitionId,
        string sensorName,
        string message,
        double? triggerValue,
        AlarmPriority priority = AlarmPriority.Medium)
    {
        Id = Guid.NewGuid();
        StationId = stationId;
        AlarmDefinitionId = alarmDefinitionId;
        SensorName = sensorName;
        Message = message;
        TriggerValue = triggerValue;
        Priority = priority;
        OccurredAt = DateTime.UtcNow;
        NotificationSent = false;
    }

    public void Resolve()
    {
        ResolvedAt = DateTime.UtcNow;
    }

    public void MarkNotificationSent()
    {
        NotificationSent = true;
        NotificationSentAt = DateTime.UtcNow;
    }

    public bool IsActive => !ResolvedAt.HasValue;

    public TimeSpan? Duration => ResolvedAt.HasValue 
        ? ResolvedAt.Value - OccurredAt 
        : DateTime.UtcNow - OccurredAt;
}
