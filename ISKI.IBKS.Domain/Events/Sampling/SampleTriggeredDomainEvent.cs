using ISKI.IBKS.Domain.Common.Entities;
using ISKI.IBKS.Domain.Enums;

namespace ISKI.IBKS.Domain.Events.Sampling;

public record SampleTriggeredDomainEvent(
    Guid SampleRequestId,
    Guid StationId,
    string? TriggerParameter,
    SampleTriggerType TriggerType) : IDomainEvent;

