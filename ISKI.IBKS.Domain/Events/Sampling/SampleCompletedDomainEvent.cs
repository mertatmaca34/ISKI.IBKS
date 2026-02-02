using ISKI.IBKS.Domain.Common.Entities;

namespace ISKI.IBKS.Domain.Events.Sampling;

public record SampleCompletedDomainEvent(
    Guid SampleRequestId,
    Guid StationId,
    string SampleCode) : IDomainEvent;

