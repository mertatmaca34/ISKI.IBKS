using ISKI.IBKS.Infrastructure.RemoteApi.SAIS.Abstractions;
using ISKI.IBKS.Infrastructure.RemoteApi.SAIS.Contracts;
using ISKI.IBKS.Infrastructure.RemoteApi.SAIS.Contracts.Login;
using ISKI.IBKS.Infrastructure.RemoteApi.SAIS.Options;
using Microsoft.Extensions.Options;
using System;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace ISKI.IBKS.Infrastructure.RemoteApi.SAIS.Http;

public sealed class SaisAuthClient : SaisApiClientBase, ISaisAuthClient
{
    public SaisAuthClient(HttpClient httpClient, IOptions<SAISOptions> saisOptions, ISaisTicketProvider saisTicketProvider)
        : base(httpClient, saisOptions, saisTicketProvider)
    {
    }

    public Task<SaisResultEnvelope<LoginResponse>> LoginAsync(LoginRequest request, CancellationToken ct = default)
        => PostAsync<LoginResponse>(relativeUri: _saisOptions.LoginUrl ?? "/Security/login", payload: request, includeTicket: false, cancellationToken: ct);
}
