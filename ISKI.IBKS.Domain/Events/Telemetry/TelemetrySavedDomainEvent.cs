using ISKI.IBKS.Domain.Common.Entities;

namespace ISKI.IBKS.Domain.Events.Telemetry;

public record TelemetrySavedDomainEvent(
    Guid StationId,
    DateTime ReadTime,
    double Ph,
    double Iletkenlik,
    double CozunmusOksijen,
    double Akm,
    double Koi) : IDomainEvent;

