using ISKI.IBKS.Application.Features.HealthSummary.Abstractions;
using ISKI.IBKS.Application.Features.HealthSummary.Dtos;

namespace ISKI.IBKS.Application.Features.HealthSummary.Services;

public class HealthSummaryService(IPlcStatusProvider plcStatusProvider, ISaisStatusProvider saisStatusProvider) : IHealthSummaryService 
{
    public async Task<HealthSummaryDto> GetHealthSummaryAsync(Guid stationId, CancellationToken ct = default)
    {
        bool plcStatus = await plcStatusProvider.IsConnectedAsync(stationId, ct);
        bool saisStatus = await saisStatusProvider.IsHealthyAsync(ct);

        DateTime? lastPhCalibration = saisStatus == true ? await saisStatusProvider.GetLastPhCalibrationDateAsync(stationId, ct) : DateTime.MinValue;
        DateTime? lastIletkenlikCalibration = saisStatus == true ? await saisStatusProvider.GetLastIletkenlikCalibrationDateAsync(stationId, ct) : DateTime.MinValue;

        return new HealthSummaryDto
        {
            PlcHealthy = plcStatus,
            ApiHealthy = saisStatus,
            LastPhCalibration = lastPhCalibration,
            LastIletkenlikCalibration = lastIletkenlikCalibration
        };
    }
}