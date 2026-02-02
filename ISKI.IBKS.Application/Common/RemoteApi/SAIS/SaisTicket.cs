namespace ISKI.IBKS.Application.Common.RemoteApi.SAIS;

public sealed record SaisTicket(Guid? TicketId, Guid? DeviceId, DateTime? ExpiresAt, DateTime CreatedAt);

