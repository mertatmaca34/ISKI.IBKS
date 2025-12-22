using ISKI.IBKS.Infrastructure.RemoteApi.SAIS.Abstractions;
using ISKI.IBKS.Infrastructure.RemoteApi.SAIS.Contracts;
using ISKI.IBKS.Infrastructure.RemoteApi.SAIS.Contracts.Calibration;
using ISKI.IBKS.Infrastructure.RemoteApi.SAIS.Contracts.Channel;
using ISKI.IBKS.Infrastructure.RemoteApi.SAIS.Contracts.Login;
using ISKI.IBKS.Infrastructure.RemoteApi.SAIS.Contracts.SendData;
using ISKI.IBKS.Infrastructure.RemoteApi.SAIS.Contracts.Units;
using ISKI.IBKS.Infrastructure.RemoteApi.SAIS.Options;
using Microsoft.Extensions.Options;
using System;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace ISKI.IBKS.Infrastructure.RemoteApi.SAIS.Http;

public class SaisApiClient : SaisApiClientBase, ISaisApiClient
{
    public SaisApiClient(HttpClient httpClient, ISaisTicketProvider? saisTicketProvider, IOptions<SAISOptions> saisOptions)
        : base(httpClient, saisOptions, saisTicketProvider)
    {
    }

    public Task<SaisResultEnvelope<LoginResponse>> LoginAsync(LoginRequest request, CancellationToken ct = default)
        => PostAsync<LoginResponse>(relativeUri: "/Security/login", payload: request, includeTicket: false, cancellationToken: ct);

    public Task<SaisResultEnvelope<SendDataResponse>> SendDataAsync(SendDataRequest request, CancellationToken ct = default)
        => PostAsync<SendDataResponse>(relativeUri: "/SAIS/SendData", payload: request, includeTicket: true, cancellationToken: ct);

    public Task<SaisResultEnvelope<GetChannelInformationResponse>> GetChannelInformation(GetChannelInformationRequest request, CancellationToken ct = default)
        => PostAsync<GetChannelInformationResponse>(relativeUri: "/SAIS/GetChannelInformationByStationId", payload: request, includeTicket: true, cancellationToken: ct);

    public Task<SaisResultEnvelope<GetCalibrationResponse[]>> GetCalibration(GetCalibrationRequest request, CancellationToken ct = default)
        => PostAsync<GetCalibrationResponse[]>(relativeUri: "/SAIS/GetCalibration", payload: request, includeTicket: true, cancellationToken: ct);

    public Task<SaisResultEnvelope<GetUnitsResponse>> GetUnits(CancellationToken ct = default)
        => PostAsync<GetUnitsResponse>(relativeUri: "/SAIS/GetUnits", payload: null, includeTicket: true, cancellationToken: ct);

}
