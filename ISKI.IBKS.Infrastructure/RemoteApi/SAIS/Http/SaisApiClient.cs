using ISKI.IBKS.Infrastructure.RemoteApi.SAIS.Abstractions;
using ISKI.IBKS.Infrastructure.RemoteApi.SAIS.Models;
using ISKI.IBKS.Infrastructure.RemoteApi.SAIS.Models.Channel;
using ISKI.IBKS.Infrastructure.RemoteApi.SAIS.Models.Login;
using ISKI.IBKS.Infrastructure.RemoteApi.SAIS.Models.SendData;
using ISKI.IBKS.Infrastructure.RemoteApi.SAIS.Models.Units;
using ISKI.IBKS.Infrastructure.RemoteApi.SAIS.Options;
using Microsoft.Extensions.Options;

namespace ISKI.IBKS.Infrastructure.RemoteApi.SAIS.Http;

public class SaisApiClient : SaisApiClientBase, ISaisApiClient
{
    public SaisApiClient(HttpClient httpClient, ISaisTicketProvider? saisTicketProvider, IOptions<SAISOptions> saisOptions) : base(httpClient, saisTicketProvider, saisOptions)
    {
    }

    public Task<SaisResultEnvelope<LoginResponse>> LoginAsync(LoginRequest request, CancellationToken ct = default)
        => PostAsync<LoginResponse>("/Security/login", request, includeTicket: false, ct);

    public Task<SaisResultEnvelope<SendDataResponse>> SendDataAsync(SendDataRequest request, CancellationToken ct = default)
        => PostAsync<SendDataResponse>("/SendData", request, includeTicket: true, ct);

    public Task<SaisResultEnvelope<GetChannelInformationResponse>> GetChannelInformation(GetChannelInformationRequest request, CancellationToken ct = default)
        => PostAsync<GetChannelInformationResponse>("/GetChannelInformationByStationId", request, includeTicket: true, ct);

    public Task<SaisResultEnvelope<GetUnitsResponse>> GetUnits(CancellationToken ct = default)
        => PostAsync<GetUnitsResponse>("/GetChannelInformationByStationId", null, includeTicket: true, ct);

}
