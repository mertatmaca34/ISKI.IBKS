using ISKI.IBKS.Domain.Common.Entities;
using ISKI.IBKS.Domain.Events.Sampling;
using ISKI.IBKS.Domain.Enums;

namespace ISKI.IBKS.Domain.Entities;

public sealed class SampleRequest : AuditableEntity<Guid>
{
    public Guid StationId { get; private set; }

    public string SampleCode { get; private set; } = string.Empty;

    public string? TriggerParameter { get; private set; }

    public SampleStatus Status { get; private set; }

    public DateTime StartedAt { get; private set; }

    public DateTime? CompletedAt { get; private set; }

    public SampleTriggerType TriggerType { get; private set; }

    public string? ErrorMessage { get; private set; }

    public static SampleRequest CreateRemote(Guid stationId)
    {
        return new SampleRequest
        {
            Id = Guid.NewGuid(),
            StationId = stationId,
            TriggerType = SampleTriggerType.SaisRemote,
            Status = SampleStatus.Pending,
            StartedAt = DateTime.UtcNow
        };
    }

    public static SampleRequest Create(Guid stationId, string reason, SamplePriority priority)
    {
        return new SampleRequest
        {
            Id = Guid.NewGuid(),
            StationId = stationId,
            TriggerParameter = reason,
            TriggerType = SampleTriggerType.Manual,
            Status = SampleStatus.Pending,
            StartedAt = DateTime.UtcNow
        };
    }

    private SampleRequest() { }

    public static SampleRequest CreateFromSaisTrigger(Guid stationId, string sampleCode)
    {
        var request = new SampleRequest
        {
            Id = Guid.NewGuid(),
            StationId = stationId,
            SampleCode = sampleCode,
            TriggerType = SampleTriggerType.SaisRemote,
            Status = SampleStatus.Started,
            StartedAt = DateTime.UtcNow
        };

        return request;
    }

    public static SampleRequest CreateFromLimitOver(Guid stationId, string triggerParameter)
    {
        var request = new SampleRequest
        {
            Id = Guid.NewGuid(),
            StationId = stationId,
            TriggerParameter = triggerParameter,
            TriggerType = SampleTriggerType.LimitOver,
            Status = SampleStatus.Pending,
            StartedAt = DateTime.UtcNow
        };

        request.RaiseDomainEvent(new SampleTriggeredDomainEvent(
            request.Id,
            request.StationId,
            request.TriggerParameter,
            request.TriggerType));

        return request;
    }

    public void SetSampleCode(string sampleCode)
    {
        if (Status != SampleStatus.Pending)
            return;

        SampleCode = sampleCode;
        Status = SampleStatus.Started;
    }

    public void Complete(int bottleNumber)
    {
        if (Status != SampleStatus.Started)
            return;

        Status = SampleStatus.Completed;
        CompletedAt = DateTime.UtcNow;

        RaiseDomainEvent(new SampleCompletedDomainEvent(Id, StationId, SampleCode));
    }

    public void MarkAsCompleted()
    {
        Complete(0);
    }

    public void MarkAsFailed(string errorMessage)
    {
        Status = SampleStatus.Failed;
        CompletedAt = DateTime.UtcNow;
        ErrorMessage = errorMessage;
    }
}

