using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ISKI.IBKS.Application.Common.RemoteApi.SAIS;

public sealed record AToken(Guid? TicketId, Guid? DeviceId);

