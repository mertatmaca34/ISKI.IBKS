using ISKI.IBKS.Application.Features.HealthSummary.Dtos;
using ISKI.IBKS.Application.Features.HealthSummary.Services;
using ISKI.IBKS.Application.Features.StationSnapshots.Abstractions;
using ISKI.IBKS.Infrastructure.RemoteApi.SAIS.Abstractions;
using ISKI.IBKS.Infrastructure.RemoteApi.SAIS.Contracts.Calibration;
using Microsoft.Extensions.Logging;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace ISKI.IBKS.Infrastructure.Application.Features.HealthSummary;

public class HealthSummaryService : IHealthSummaryService
{
    private readonly IStationSnapshotCache _snapshotCache;
    private readonly ISaisTicketProvider _ticketProvider;
    private readonly ISaisApiClient _saisApiClient;
    private readonly ILogger<HealthSummaryService> _logger;

    public HealthSummaryService(
        IStationSnapshotCache snapshotCache,
        ISaisTicketProvider ticketProvider,
        ISaisApiClient saisApiClient,
        ILogger<HealthSummaryService> logger)
    {
        _snapshotCache = snapshotCache ?? throw new ArgumentNullException(nameof(snapshotCache));
        _ticketProvider = ticketProvider ?? throw new ArgumentNullException(nameof(ticketProvider));
        _saisApiClient = saisApiClient ?? throw new ArgumentNullException(nameof(saisApiClient));
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
    }

    public async Task<HealthSummaryDto> GetHealthSummaryAsync(Guid stationId, CancellationToken ct = default)
    {
        ct.ThrowIfCancellationRequested();

        // PLC health: check if snapshot exists and assume connected if present
        var plcConnected = _snapshotCache.TryGet(stationId, out var snap) && snap is not null;

        // API health: try to get ticket
        var apiHealthy = false;
        try
        {
            var ticket = await _ticketProvider.GetTicketAsync(ct).ConfigureAwait(false);
            apiHealthy = ticket is not null && ticket.TicketId.HasValue;
        }
        catch (Exception ex)
        {
            _logger.LogWarning(ex, "Failed to obtain SAIS ticket while evaluating API health.");
            apiHealthy = false;
        }

        // Calibration dates: call SAIS GetCalibration and map by DBColumnName
        DateTime? lastPh = null;
        DateTime? lastIletkenlik = null;

        try
        {
            var req = new GetCalibrationRequest
            {
                StationId = stationId,
                StartDate = DateTime.UtcNow.AddYears(-1),
                EndDate = DateTime.UtcNow
            };

            var envelope = await _saisApiClient.GetCalibration(req, ct).ConfigureAwait(false);

            var items = envelope?.Objects;
            if (items is not null && items.Length > 0)
            {
                // find latest calibration date for Ph
                var phEntries = items.Where(i => string.Equals(i.DBColumnName, "Ph", StringComparison.OrdinalIgnoreCase));
                if (phEntries.Any())
                {
                    var latest = phEntries.Max(i => i.CalibrationDate);
                    lastPh = latest;
                }

                // find latest calibration date for Iletkenlik (some APIs may use different casing or names)
                var iletEntries = items.Where(i => string.Equals(i.DBColumnName, "Iletkenlik", StringComparison.OrdinalIgnoreCase));
                if (iletEntries.Any())
                {
                    var latest = iletEntries.Max(i => i.CalibrationDate);
                    lastIletkenlik = latest;
                }

                // If API uses alternate DB column names, we could also attempt matching by known keys (e.g. from ui-mapping).
            }
        }
        catch (Exception ex)
        {
            _logger.LogWarning(ex, "Failed to read calibration data.");
        }

        return new HealthSummaryDto
        {
            PlcConnected = plcConnected,
            ApiHealthy = apiHealthy,
            LastPhCalibration = lastPh,
            LastIletkenlikCalibration = lastIletkenlik
        };
    }
}
