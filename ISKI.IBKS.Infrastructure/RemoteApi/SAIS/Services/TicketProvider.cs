using ISKI.IBKS.Infrastructure.RemoteApi.SAIS.Abstractions;
using ISKI.IBKS.Infrastructure.RemoteApi.SAIS.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ISKI.IBKS.Infrastructure.RemoteApi.SAIS.Services;

public class TicketProvider : ISaisTicketProvider
{
    private SaisTicket? _currentTicket;

    public Task<SaisTicket> GetTicketAsync(CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public void InvalidateTicket()
    {
        _currentTicket = null;
    }
}
