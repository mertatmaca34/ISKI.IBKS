using ISKI.IBKS.Infrastructure.RemoteApi.SAIS.Contracts;
using ISKI.IBKS.Infrastructure.RemoteApi.SAIS.Contracts.Calibration;
using ISKI.IBKS.Infrastructure.RemoteApi.SAIS.Contracts.Channel;
using ISKI.IBKS.Infrastructure.RemoteApi.SAIS.Contracts.Data;
using ISKI.IBKS.Infrastructure.RemoteApi.SAIS.Contracts.Diagnostics;
using ISKI.IBKS.Infrastructure.RemoteApi.SAIS.Contracts.Login;
using ISKI.IBKS.Infrastructure.RemoteApi.SAIS.Contracts.Sample;
using ISKI.IBKS.Infrastructure.RemoteApi.SAIS.Contracts.SendData;
using ISKI.IBKS.Infrastructure.RemoteApi.SAIS.Contracts.Station;
using ISKI.IBKS.Infrastructure.RemoteApi.SAIS.Contracts.Units;

namespace ISKI.IBKS.Infrastructure.RemoteApi.SAIS.Abstractions;

public interface ISaisApiClient
{
    // Güvenlik Servisleri
    Task<SaisResultEnvelope<LoginResponse>> LoginAsync(LoginRequest request, CancellationToken ct = default);

    // Zaman Servisleri
    Task<SaisResultEnvelope<DateTime>> GetServerDateTimeAsync(CancellationToken ct = default);

    // İstasyon Servisleri
    Task<SaisResultEnvelope<StationInfoResponse>> GetStationInformationAsync(Guid stationId, CancellationToken ct = default);
    Task<SaisResultEnvelope<object>> SendHostChangedAsync(SendHostChangedRequest request, CancellationToken ct = default);

    // Kanal Servisleri
    Task<SaisResultEnvelope<GetChannelInformationResponse>> GetChannelInformationAsync(Guid stationId, CancellationToken ct = default);

    // Genel Bilgiler Servisleri
    Task<SaisResultEnvelope<GetUnitsResponse>> GetUnitsAsync(CancellationToken ct = default);
    Task<SaisResultEnvelope<ParameterResponse[]>> GetParametersAsync(CancellationToken ct = default);
    Task<SaisResultEnvelope<DataStatusResponse[]>> GetDataStatusDescriptionAsync(CancellationToken ct = default);

    // Veri Servisleri
    Task<SaisResultEnvelope<SendDataResponse>> SendDataAsync(SendDataRequest request, CancellationToken ct = default);
    Task<SaisResultEnvelope<SendDataResponse>> GetLastDataAsync(Guid stationId, int period, CancellationToken ct = default);
    Task<SaisResultEnvelope<SendDataResponse[]>> GetDataByBetweenTwoDateAsync(GetDataRequest request, CancellationToken ct = default);
    Task<SaisResultEnvelope<MissingDatesResponse>> GetMissingDatesAsync(Guid stationId, CancellationToken ct = default);

    // Diagnostik Servisleri
    Task<SaisResultEnvelope<DiagnosticTypeResponse[]>> GetDiagnosticTypesAsync(CancellationToken ct = default);
    Task<SaisResultEnvelope<object>> SendDiagnosticAsync(SendDiagnosticRequest request, CancellationToken ct = default);
    Task<SaisResultEnvelope<object>> SendDiagnosticWithTypeNoAsync(SendDiagnosticWithTypeRequest request, CancellationToken ct = default);
    Task<SaisResultEnvelope<object>> SendPowerOffTimeAsync(SendPowerOffTimeRequest request, CancellationToken ct = default);

    // Kalibrasyon Servisleri
    Task<SaisResultEnvelope<object>> SendCalibrationAsync(SendCalibrationRequest request, CancellationToken ct = default);
    Task<SaisResultEnvelope<GetCalibrationResponse[]>> GetCalibrationAsync(GetCalibrationRequest request, CancellationToken ct = default);

    // Numune Servisleri
    Task<SaisResultEnvelope<object>> SampleRequestStartAsync(SampleRequestStartRequest request, CancellationToken ct = default);
    Task<SaisResultEnvelope<string>> SampleRequestLimitOverAsync(SampleLimitOverRequest request, CancellationToken ct = default);
    Task<SaisResultEnvelope<object>> SampleRequestCompleteAsync(SampleCompleteRequest request, CancellationToken ct = default);
    Task<SaisResultEnvelope<object>> SampleRequestErrorAsync(SampleErrorRequest request, CancellationToken ct = default);
}
