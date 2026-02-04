using ISKI.IBKS.Infrastructure.RemoteApi.SAIS.Abstractions;
using ISKI.IBKS.Infrastructure.RemoteApi.SAIS.Contracts;
using ISKI.IBKS.Infrastructure.RemoteApi.SAIS.Contracts.Login;
using ISKI.IBKS.Infrastructure.RemoteApi.SAIS.Options;
using ISKI.IBKS.Infrastructure.RemoteApi.SAIS.Utilities;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;

namespace ISKI.IBKS.Infrastructure.RemoteApi.SAIS.Providers;

public sealed class TicketProvider : ISaisTicketProvider, IDisposable
{
    // Resolve auth client per-call to avoid lifetime/capture issues
    private readonly IServiceProvider _serviceProvider;
    private readonly SAISOptions _options;
    private readonly ILogger<TicketProvider> _logger;

    private readonly SemaphoreSlim _sync = new(1, 1);

    private SaisTicket? _currentTicket;
    private DateTimeOffset _expiresAtUtc;
    private bool _disposed;

    public TicketProvider(
        IServiceProvider serviceProvider,
        IOptions<SAISOptions> options,
        ILogger<TicketProvider> logger)
    {
        _serviceProvider = serviceProvider ?? throw new ArgumentNullException(nameof(serviceProvider));
        _options = options?.Value ?? throw new ArgumentNullException(nameof(options));
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
    }

    public async Task<SaisTicket> GetTicketAsync(CancellationToken cancellationToken = default)
    {
        ThrowIfDisposed();

        if (IsTicketValid())
            return _currentTicket!;

        await _sync.WaitAsync(cancellationToken).ConfigureAwait(false);
        try
        {
            if (IsTicketValid())
                return _currentTicket!;

            _logger.LogInformation("Requesting new SAIS ticket...");

            using var scope = _serviceProvider.CreateScope();
            var authClient = scope.ServiceProvider.GetRequiredService<ISaisAuthClient>();

            // Hash password twice with MD5 as required by SAIS API
            var hashedPassword = PasswordHasher.HashPassword(_options.Password);

            var loginReq = new LoginRequest
            {
                UserName = _options.Username,
                Password = hashedPassword
            };

            var envelope = await authClient.LoginAsync(loginReq, cancellationToken).ConfigureAwait(false);

            if (envelope?.Objects is null)
            {
                _logger.LogError("SAIS login response did not contain objects.");
                throw new InvalidOperationException("Login response did not contain expected objects.");
            }

            if (!envelope.Objects.TicketId.HasValue)
            {
                _logger.LogError("SAIS login response did not contain a ticket id.");
                throw new InvalidOperationException("Login response did not contain a ticket id.");
            }

            var createdAt = DateTime.UtcNow;
            var ticket = new SaisTicket(
                envelope.Objects.TicketId, 
                envelope.Objects.DeviceId, 
                envelope.Objects.User is null ? null : createdAt.AddMinutes(30),
                createdAt);

            _currentTicket = ticket;
            _expiresAtUtc = DateTimeOffset.UtcNow.Add(_options.TicketTtl);

            _logger.LogInformation("SAIS ticket acquired. TicketId={TicketId}, CreatedAt={CreatedAt}, ExpiresAtUtc={ExpiresAtUtc}", 
                ticket.TicketId, createdAt, _expiresAtUtc);

            return ticket;
        }
        catch (Exception ex)
        {
            _currentTicket = null;
            _expiresAtUtc = default;
            _logger.LogError(ex, "Failed to obtain SAIS ticket.");
            throw;
        }
        finally
        {
            _sync.Release();
        }
    }

    public void InvalidateTicket()
    {
        ThrowIfDisposed();
        _currentTicket = null;
        _expiresAtUtc = default;
        _logger.LogWarning("SAIS ticket invalidated.");
    }

    private bool IsTicketValid()
    {
        if (_currentTicket is null)
        {
            _logger.LogDebug("Ticket validation failed: ticket is null");
            return false;
        }

        // Check if ticket is older than 30 minutes
        var ticketAge = DateTime.UtcNow - _currentTicket.CreatedAt;
        if (ticketAge.TotalMinutes >= 30)
        {
            _logger.LogWarning("Ticket validation failed: ticket is older than 30 minutes. Age={TicketAgeMinutes} minutes", 
                ticketAge.TotalMinutes);
            return false;
        }

        // Check provider's expiration time
        if (_expiresAtUtc <= DateTimeOffset.UtcNow.AddSeconds(10))
        {
            _logger.LogWarning("Ticket validation failed: provider expiration time reached");
            return false;
        }

        _logger.LogDebug("Ticket is valid. Age={TicketAgeMinutes} minutes", ticketAge.TotalMinutes);
        return true;
    }

    private void ThrowIfDisposed()
    {
        if (_disposed)
            throw new ObjectDisposedException(nameof(TicketProvider));
    }

    public void Dispose()
    {
        if (_disposed) return;
        _sync.Dispose();
        _disposed = true;
    }
}
