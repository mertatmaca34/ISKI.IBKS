using ISKI.IBKS.Application.Features.HealthSummary.Abstractions;
using ISKI.IBKS.Infrastructure.RemoteApi.SAIS.Abstractions;
using ISKI.IBKS.Infrastructure.RemoteApi.SAIS.Contracts.Calibration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ISKI.IBKS.Infrastructure.RemoteApi.SAIS.Providers
{
    public class SaisStatusProvider(ISaisTicketProvider ticketProvider, ISaisApiClient saisApiClient) : ISaisStatusProvider
    {
        public async Task<DateTime?> GetLastIletkenlikCalibrationDateAsync(Guid stationId, CancellationToken ct = default)
        {
            GetCalibrationRequest request = new GetCalibrationRequest
            {
                StationId = stationId,
                StartDate = DateTime.Now.AddMonths(-1),
                EndDate = DateTime.Now
            };

            var getLastCalibrationDates = await saisApiClient.GetCalibrationAsync(request, ct);


            return getLastCalibrationDates?.Objects?
                .Where(c => string.Equals(c.DBColumnName, "Iletkenlik", StringComparison.OrdinalIgnoreCase))
                .OrderByDescending(c => c.CalibrationDate)
                .FirstOrDefault()?.CalibrationDate ?? DateTime.MinValue;
        }

        public async Task<DateTime?> GetLastPhCalibrationDateAsync(Guid stationId, CancellationToken ct = default)
        {
            GetCalibrationRequest request = new()
            {
                StationId = stationId,
                StartDate = DateTime.Now.AddMonths(-1),
                EndDate = DateTime.Now
            };
            try
            {
                var getLastCalibrationDates = await saisApiClient.GetCalibrationAsync(request, ct);


                return getLastCalibrationDates?.Objects?
                    .Where(c => string.Equals(c.DBColumnName, "Ph", StringComparison.OrdinalIgnoreCase))
                    .OrderByDescending(c => c.CalibrationDate)
                    .FirstOrDefault()?.CalibrationDate ?? DateTime.MinValue;
            }
            catch (Exception)
            {
                return DateTime.MinValue;
            }
        }

        public async Task<bool> IsHealthyAsync(CancellationToken ct = default)
        {
            try
            {
                var ticket = await ticketProvider.GetTicketAsync(ct);

                if (ticket is not null && ticket.TicketId.HasValue && ticket.ExpiresAt > DateTime.UtcNow)
                {
                    return true;
                }
                return false;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}
