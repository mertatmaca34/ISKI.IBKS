using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ISKI.IBKS.Infrastructure.RemoteApi.SAIS.Contracts;

public sealed record SaisTicket(Guid? TicketId, Guid? DeviceId, DateTime? ExpiresAt, DateTime CreatedAt);
