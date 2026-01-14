using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ISKI.IBKS.Application.Features.HealthSummary.Abstractions;

public interface IPlcStatusProvider
{
    Task<bool> IsConnectedAsync(Guid stationId, CancellationToken ct = default);
}
