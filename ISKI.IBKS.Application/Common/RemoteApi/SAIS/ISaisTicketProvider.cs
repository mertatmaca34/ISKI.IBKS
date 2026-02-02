namespace ISKI.IBKS.Application.Common.RemoteApi.SAIS;

public interface ISaisTicketProvider
{
    Task<SaisTicket> GetTicketAsync(CancellationToken ct = default);
    void InvalidateTicket();
}
