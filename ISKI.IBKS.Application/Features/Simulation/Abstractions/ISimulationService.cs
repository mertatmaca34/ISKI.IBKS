using ISKI.IBKS.Application.Features.HealthSummary.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ISKI.IBKS.Application.Features.Simulation.Abstractions;

public interface ISimulationService
{
    Task<HealthSummaryDto> GetHealthSummaryAsync(Guid stationId, CancellationToken ct = default);

}
