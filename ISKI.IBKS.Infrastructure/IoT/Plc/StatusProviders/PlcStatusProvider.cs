using ISKI.IBKS.Application.Features.HealthSummary.Abstractions;
using ISKI.IBKS.Application.Features.Plc.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ISKI.IBKS.Infrastructure.IoT.Plc.StatusProviders;

public sealed class PlcStatusProvider(IPlcClient plcClient) : IPlcStatusProvider
{
    public async Task<bool> IsConnectedAsync(Guid stationId, CancellationToken ct = default)
    {
        if (plcClient != null && plcClient.IsConnected)
        {
            return await Task.FromResult(true);
        }
        return await Task.FromResult(false);
    }
}
