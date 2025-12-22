using ISKI.IBKS.Infrastructure.RemoteApi.SAIS.Abstractions;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace ISKI.IBKS.Infrastructure.RemoteApi.SAIS.Services
{
    public sealed class SaisTicketKeepAliveService : BackgroundService
    {
        private readonly ISaisTicketProvider _ticketProvider;
        private readonly ILogger<SaisTicketKeepAliveService> _logger;
        private readonly TimeSpan _refreshInterval = TimeSpan.FromSeconds(30);

        public SaisTicketKeepAliveService(ISaisTicketProvider ticketProvider, ILogger<SaisTicketKeepAliveService> logger)
        {
            _ticketProvider = ticketProvider ?? throw new ArgumentNullException(nameof(ticketProvider));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            _logger.LogInformation("SAIS ticket keep-alive service started.");

            while (!stoppingToken.IsCancellationRequested)
            {
                try
                {
                    // Ensure ticket is available and valid. TicketProvider will refresh only when needed.
                    var ticket = await _ticketProvider.GetTicketAsync(stoppingToken).ConfigureAwait(false);

                    if (ticket is null || ticket.TicketId is null)
                    {
                        _logger.LogWarning("SAIS ticket absent or invalid during keep-alive.");
                    }
                    else
                    {
                        _logger.LogDebug("SAIS ticket keep-alive OK. TicketId={TicketId}", ticket.TicketId);
                    }
                }
                catch (OperationCanceledException)
                {
                    // shutting down
                    break;
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "Error while keeping SAIS ticket alive.");
                }

                try
                {
                    await Task.Delay(_refreshInterval, stoppingToken).ConfigureAwait(false);
                }
                catch (OperationCanceledException)
                {
                    break;
                }
            }

            _logger.LogInformation("SAIS ticket keep-alive service stopped.");
        }
    }
}
