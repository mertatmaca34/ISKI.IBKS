
using ISKI.IBKS.Domain.Common.Entities;

namespace ISKI.IBKS.Domain.Entities;

public sealed class AlarmUserSubscription : Entity<Guid>
{
    public Guid AlarmDefinitionId { get; private set; }
    public Guid AlarmUserId { get; private set; }
    public AlarmUser AlarmUser { get; private set; } = null!;
    public bool IsActive { get; private set; } = true;

    private AlarmUserSubscription() { }

    [System.Diagnostics.CodeAnalysis.SetsRequiredMembers]
    public AlarmUserSubscription(Guid alarmDefinitionId, Guid alarmUserId)
    {
        Id = Guid.NewGuid();
        AlarmDefinitionId = alarmDefinitionId;
        AlarmUserId = alarmUserId;
        IsActive = true;
    }

    public void Deactivate() => IsActive = false;
    public void Activate() => IsActive = true;
}

