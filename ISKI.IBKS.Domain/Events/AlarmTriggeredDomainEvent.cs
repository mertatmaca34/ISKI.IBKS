using ISKI.IBKS.Domain.Common.Entities;
using System;

namespace ISKI.IBKS.Domain.Events;

public record AlarmTriggeredDomainEvent(
    Guid AlarmId,
    string AlarmName,
    bool IsActive) : IDomainEvent;
