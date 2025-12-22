using ISKI.IBKS.Application.Features.HealthSummary.Dtos;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace ISKI.IBKS.Application.Features.HealthSummary.Services;

public interface IHealthSummaryService
{
    Task<HealthSummaryDto> GetHealthSummaryAsync(Guid stationId, CancellationToken ct = default);
}
