using ISKI.IBKS.Domain.Common.Entities;

namespace ISKI.IBKS.Domain.Events.Calibration;

public record CalibrationSavedDomainEvent(
    Guid CalibrationId,
    Guid StationId,
    string DbColumnName) : IDomainEvent;

