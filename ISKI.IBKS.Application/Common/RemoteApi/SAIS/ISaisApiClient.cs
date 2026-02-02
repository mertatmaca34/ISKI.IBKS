using ISKI.IBKS.Application.Common.RemoteApi.SAIS;

namespace ISKI.IBKS.Application.Common.RemoteApi.SAIS;

public interface ISaisApiClient
{
    Task<SaisResultEnvelope<LoginResponse>> LoginAsync(LoginRequest request, CancellationToken ct = default);

    Task<SaisResultEnvelope<DateTime>> GetServerDateTimeAsync(CancellationToken ct = default);

    Task<SaisResultEnvelope<StationInfoResponse>> GetStationInformationAsync(Guid stationId, CancellationToken ct = default);

    Task<SaisResultEnvelope<GetChannelInformationResponse>> GetChannelInformationAsync(Guid stationId, CancellationToken ct = default);

    Task<SaisResultEnvelope<SendDataResponse>> SendDataAsync(SendDataRequest request, CancellationToken ct = default);

    Task<SaisResultEnvelope<object>> SendCalibrationAsync(SendCalibrationRequest request, CancellationToken ct = default);
    Task<SaisResultEnvelope<GetCalibrationResponse[]>> GetCalibrationAsync(GetCalibrationRequest request, CancellationToken ct = default);

    Task<SaisResultEnvelope<object>> SampleRequestStartAsync(SampleRequestStartRequest request, CancellationToken ct = default);
    Task<SaisResultEnvelope<string>> SampleRequestLimitOverAsync(SampleLimitOverRequest request, CancellationToken ct = default);
    Task<SaisResultEnvelope<object>> SampleRequestCompleteAsync(SampleCompleteRequest request, CancellationToken ct = default);
    Task<SaisResultEnvelope<object>> SampleRequestErrorAsync(SampleErrorRequest request, CancellationToken ct = default);

    Task<SaisResultEnvelope<bool>> SendInstantDataAsync(object data, CancellationToken ct = default);
    Task<SaisResultEnvelope<bool>> SendHistoryDataAsync(object data, CancellationToken ct = default);
}

