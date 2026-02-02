using System;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using ISKI.IBKS.Application.Common.RemoteApi.SAIS;
using ISKI.IBKS.Infrastructure.RemoteApi.SAIS.Options;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace ISKI.IBKS.Infrastructure.RemoteApi.SAIS.Http;

public class SaisApiClient : SaisApiClientBase, ISaisApiClient
{
    public SaisApiClient(
        HttpClient httpClient,
        ISaisTicketProvider? saisTicketProvider,
        IOptions<SAISOptions> saisOptions,
        ILogger<SaisApiClient> logger)
        : base(httpClient, saisOptions, saisTicketProvider, logger)
    {
    }

    #region Guvenlik Servisleri
    public Task<SaisResultEnvelope<LoginResponse>> LoginAsync(LoginRequest request, CancellationToken ct = default)
        => PostAsync<LoginResponse>(relativeUri: "/Security/login", payload: request, includeTicket: false, cancellationToken: ct);
    #endregion

    #region Zaman Servisleri
    public Task<SaisResultEnvelope<DateTime>> GetServerDateTimeAsync(CancellationToken ct = default)
        => PostAsync<DateTime>(relativeUri: "/SAIS/GetServerDateTime", payload: null, includeTicket: true, cancellationToken: ct);
    #endregion

    #region Istasyon Servisleri
    public Task<SaisResultEnvelope<StationInfoResponse>> GetStationInformationAsync(Guid stationId, CancellationToken ct = default)
        => PostAsync<StationInfoResponse>(relativeUri: $"/SAIS/GetStationInformation?stationId={stationId}", payload: null, includeTicket: true, cancellationToken: ct);

    public Task<SaisResultEnvelope<object>> SendHostChangedAsync(SendHostChangedRequest request, CancellationToken ct = default)
        => PostAsync<object>(relativeUri: "/SAIS/SendHostChanged", payload: request, includeTicket: true, cancellationToken: ct);
    #endregion

    #region Kanal Servisleri
    public Task<SaisResultEnvelope<GetChannelInformationResponse>> GetChannelInformationAsync(Guid stationId, CancellationToken ct = default)
        => PostAsync<GetChannelInformationResponse>(relativeUri: $"/SAIS/GetChannelInformationByStationId?stationId={stationId}", payload: null, includeTicket: true, cancellationToken: ct);
    #endregion

    #region Genel Bilgiler Servisleri
    public Task<SaisResultEnvelope<GetUnitsResponse>> GetUnitsAsync(CancellationToken ct = default)
        => PostAsync<GetUnitsResponse>(relativeUri: "/SAIS/GetUnits", payload: null, includeTicket: true, cancellationToken: ct);

    public Task<SaisResultEnvelope<ParameterResponse[]>> GetParametersAsync(CancellationToken ct = default)
        => PostAsync<ParameterResponse[]>(relativeUri: "/SAIS/GetParameters", payload: null, includeTicket: true, cancellationToken: ct);

    public Task<SaisResultEnvelope<DataStatusResponse[]>> GetDataStatusDescriptionAsync(CancellationToken ct = default)
        => PostAsync<DataStatusResponse[]>(relativeUri: "/SAIS/GetDataStatusDescription", payload: null, includeTicket: true, cancellationToken: ct);
    #endregion

    #region Veri Servisleri
    public Task<SaisResultEnvelope<SendDataResponse>> SendDataAsync(SendDataRequest request, CancellationToken ct = default)
        => PostAsync<SendDataResponse>(relativeUri: "/SAIS/SendData", payload: request, includeTicket: true, cancellationToken: ct);

    public Task<SaisResultEnvelope<SendDataResponse>> GetLastDataAsync(Guid stationId, int period, CancellationToken ct = default)
        => PostAsync<SendDataResponse>(relativeUri: $"/SAIS/GetLastData?stationId={stationId}&period={period}", payload: null, includeTicket: true, cancellationToken: ct);

    public Task<SaisResultEnvelope<SendDataResponse[]>> GetDataByBetweenTwoDateAsync(GetDataRequest request, CancellationToken ct = default)
        => PostAsync<SendDataResponse[]>(
            relativeUri: $"/SAIS/GetDataByBetweenTwoDate?stationId={request.StationId}&period={request.Period}&startDate={request.StartDate:yyyy-MM-dd HH:mm:ss}&endDate={request.EndDate:yyyy-MM-dd HH:mm:ss}",
            payload: null,
            includeTicket: true,
            cancellationToken: ct);

    public Task<SaisResultEnvelope<MissingDatesResponse>> GetMissingDatesAsync(Guid stationId, CancellationToken ct = default)
        => PostAsync<MissingDatesResponse>(relativeUri: $"/SAIS/GetMissingDates?stationId={stationId}", payload: null, includeTicket: true, cancellationToken: ct);
    #endregion

    #region Diagnostik Servisleri
    public Task<SaisResultEnvelope<DiagnosticTypeResponse[]>> GetDiagnosticTypesAsync(CancellationToken ct = default)
        => PostAsync<DiagnosticTypeResponse[]>(relativeUri: "/SAIS/GetDiagnosticTypes", payload: null, includeTicket: true, cancellationToken: ct);

    public Task<SaisResultEnvelope<object>> SendDiagnosticAsync(SendDiagnosticRequest request, CancellationToken ct = default)
        => PostAsync<object>(relativeUri: "/SAIS/SendDiagnostic", payload: request, includeTicket: true, cancellationToken: ct);

    public Task<SaisResultEnvelope<object>> SendDiagnosticWithTypeNoAsync(SendDiagnosticWithTypeRequest request, CancellationToken ct = default)
        => PostAsync<object>(relativeUri: "/SAIS/SendDiagnosticWithTypeNo", payload: request, includeTicket: true, cancellationToken: ct);

    public Task<SaisResultEnvelope<object>> SendPowerOffTimeAsync(SendPowerOffTimeRequest request, CancellationToken ct = default)
        => PostAsync<object>(relativeUri: "/SAIS/SendPowerOffTime", payload: request, includeTicket: true, cancellationToken: ct);
    #endregion

    #region Kalibrasyon Servisleri
    public Task<SaisResultEnvelope<object>> SendCalibrationAsync(SendCalibrationRequest request, CancellationToken ct = default)
        => PostAsync<object>(relativeUri: "/SAIS/SendCalibration", payload: request, includeTicket: true, cancellationToken: ct);

    public Task<SaisResultEnvelope<GetCalibrationResponse[]>> GetCalibrationAsync(GetCalibrationRequest request, CancellationToken ct = default)
        => PostAsync<GetCalibrationResponse[]>(
            relativeUri: $"/SAIS/GetCalibration?stationId={request.StationId}&startDate={request.StartDate:yyyy-MM-dd}&endDate={request.EndDate:yyyy-MM-dd}",
            payload: null,
            includeTicket: true,
            cancellationToken: ct);
    #endregion

    #region Numune Servisleri
    public Task<SaisResultEnvelope<object>> SampleRequestStartAsync(SampleRequestStartRequest request, CancellationToken ct = default)
        => PostAsync<object>(relativeUri: "/SAIS/SampleRequestStart", payload: request, includeTicket: true, cancellationToken: ct);

    public Task<SaisResultEnvelope<string>> SampleRequestLimitOverAsync(SampleLimitOverRequest request, CancellationToken ct = default)
        => PostAsync<string>(relativeUri: "/SAIS/SampleRequestLimitOver", payload: request, includeTicket: true, cancellationToken: ct);

    public Task<SaisResultEnvelope<object>> SampleRequestCompleteAsync(SampleCompleteRequest request, CancellationToken ct = default)
        => PostAsync<object>(relativeUri: "/SAIS/SampleRequestComplete", payload: request, includeTicket: true, cancellationToken: ct);

    public Task<SaisResultEnvelope<object>> SampleRequestErrorAsync(SampleErrorRequest request, CancellationToken ct = default)
        => PostAsync<object>(relativeUri: "/SAIS/SampleRequestError", payload: request, includeTicket: true, cancellationToken: ct);
    #endregion

    public async Task<SaisResultEnvelope<bool>> SendInstantDataAsync(object data, CancellationToken ct = default) { return new SaisResultEnvelope<bool> { Result = true, Objects = true }; }
    public async Task<SaisResultEnvelope<bool>> SendHistoryDataAsync(object data, CancellationToken ct = default) { return new SaisResultEnvelope<bool> { Result = true, Objects = true }; }
}
