using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ISKI.IBKS.Infrastructure.RemoteApi.SAIS.Contracts.Channel;

public sealed record GetChannelInformationRequest
{
    public Guid StationId { get; init; }
}
