using ISKI.IBKS.Infrastructure.RemoteApi.SAIS.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ISKI.IBKS.Infrastructure.RemoteApi.SAIS.Abstractions;

public interface ISaisTicketProvider
{
    Task<SaisTicket> GetTicketAsync(CancellationToken cancellationToken);
    void InvalidateTicket();
}
