using ISKI.IBKS.Infrastructure.RemoteApi.SAIS.Contracts;
using System.Threading;
using System.Threading.Tasks;

namespace ISKI.IBKS.Infrastructure.RemoteApi.SAIS.Abstractions;

public interface ISaisTicketProvider
{
    Task<SaisTicket> GetTicketAsync(CancellationToken cancellationToken = default);
    void InvalidateTicket();
}
