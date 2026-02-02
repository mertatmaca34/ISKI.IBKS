namespace ISKI.IBKS.Application.Common.RemoteApi.SAIS;

public interface ISaisStatusProvider
{
    Task<DateTime?> GetLastIletkenlikCalibrationDateAsync(Guid stationId, CancellationToken ct = default);
    Task<DateTime?> GetLastPhCalibrationDateAsync(Guid stationId, CancellationToken ct = default);
    Task<bool> IsHealthyAsync(CancellationToken ct = default);
}

