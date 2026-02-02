using ISKI.IBKS.Domain.Common.Entities;
using ISKI.IBKS.Domain.Enums;
using System.Diagnostics.CodeAnalysis;

namespace ISKI.IBKS.Domain.Entities;

public sealed class AlarmEvent : AuditableEntity<Guid>
{
    public Guid StationId { get; private set; }
    public Guid AlarmDefinitionId { get; private set; }
    public DateTime OccurredAt { get; private set; }
    public DateTime? ResolvedAt { get; private set; }
    public double? TriggerValue { get; private set; }
    public string SensorName { get; private set; } = string.Empty;
    public string Message { get; private set; } = string.Empty;
    public bool NotificationSent { get; private set; }
    public DateTime? NotificationSentAt { get; private set; }
    public AlarmPriority Priority { get; private set; }

    public AlarmDefinition? AlarmDefinition { get; private set; }

    private AlarmEvent() { }

    [SetsRequiredMembers]
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
        CreatedAt = DateTime.UtcNow;
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
}
