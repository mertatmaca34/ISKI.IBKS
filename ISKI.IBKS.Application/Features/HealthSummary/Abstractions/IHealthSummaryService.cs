using ISKI.IBKS.Application.Features.HealthSummary.Dtos;

namespace ISKI.IBKS.Application.Features.HealthSummary.Abstractions;

public interface IHealthSummaryService
{
    Task<HealthSummaryDto> GetHealthSummaryAsync(Guid stationId, CancellationToken ct = default);
}
