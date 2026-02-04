using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ISKI.IBKS.Application.Features.HealthSummary.Abstractions;

public interface ISaisStatusProvider
{
    Task<bool> IsHealthyAsync(CancellationToken ct = default);
    Task<DateTime?> GetLastIletkenlikCalibrationDateAsync(Guid stationId, CancellationToken ct = default);
    Task<DateTime?> GetLastPhCalibrationDateAsync(Guid stationId, CancellationToken ct = default);
}

