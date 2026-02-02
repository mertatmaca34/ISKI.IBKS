using ISKI.IBKS.Application.Common.RemoteApi.SAIS;
using ISKI.IBKS.Infrastructure.RemoteApi.SAIS.Options;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace ISKI.IBKS.Infrastructure.RemoteApi.SAIS.Http;

public sealed class SaisAuthClient : SaisApiClientBase, ISaisAuthClient
{
    public SaisAuthClient(
        HttpClient httpClient,
        IOptions<SAISOptions> saisOptions,
        ISaisTicketProvider saisTicketProvider,
        ILogger<SaisAuthClient> logger)
        : base(httpClient, saisOptions, saisTicketProvider, logger)
    {
    }

    public Task<SaisResultEnvelope<LoginResponse>> LoginAsync(LoginRequest request, CancellationToken ct = default)
        => PostAsync<LoginResponse>(relativeUri: _saisOptions.LoginUrl ?? "/Security/login", payload: request, includeTicket: false, cancellationToken: ct);
}
