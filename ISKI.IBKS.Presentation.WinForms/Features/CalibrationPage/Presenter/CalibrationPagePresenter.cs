using ISKI.IBKS.Application.Common.IoT;
using ISKI.IBKS.Application.Common.IoT.Plc;
using ISKI.IBKS.Application.Common.RemoteApi.SAIS;
using ISKI.IBKS.Application.Common.Configuration;
using ISKI.IBKS.Presentation.WinForms.Features.CalibrationPage.Model;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;

using ISKI.IBKS.Presentation.WinForms.Features.CalibrationPage.View;
using ISKI.IBKS.Shared.Localization;

namespace ISKI.IBKS.Presentation.WinForms.Features.CalibrationPage.Presenter;

public sealed class CalibrationPagePresenter
{
    private readonly IDataCollectionService _dataCollectionService;
    private readonly IPlcClient _plcClient;
    private readonly ISaisApiClient _saisApiClient;
    private readonly IStationConfiguration _stationConfig;
    private readonly ILogger<CalibrationPagePresenter> _logger;
    private readonly ICalibrationPageView _view;
    private readonly CalibrationOps _calibrationOps;
    private Guid _stationId;

    public CalibrationPagePresenter(
        IDataCollectionService dataCollectionService,
        IPlcClient plcClient,
        ISaisApiClient saisApiClient,
        IStationConfiguration stationConfig,
        ICalibrationPageView view,
        ILogger<CalibrationPagePresenter> logger,
        ILogger<ISKI.IBKS.Presentation.WinForms.Features.CalibrationPage.View.CalibrationPage> viewLogger)
    {
        _dataCollectionService = dataCollectionService;
        _plcClient = plcClient;
        _saisApiClient = saisApiClient;
        _stationConfig = stationConfig;
        _view = view;
        _logger = logger;

        _calibrationOps = new CalibrationOps(_plcClient, _dataCollectionService, _saisApiClient, _stationConfig, viewLogger);

        _view.Load += async (s, e) => await InitializeAsync();
        _view.CalibrationRequested += async (s, e) => await HandleCalibrationRequestAsync(e);
    }

    private async Task InitializeAsync()
    {
        try
        {
            _stationId = await _dataCollectionService.GetStationIdAsync();
            _view.SetStationId(_stationId);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Failed to initialize CalibrationPagePresenter");
            _view.ShowError(Strings.Error_StationInfo);
        }
    }

    private async Task HandleCalibrationRequestAsync(CalibrationEventArgs e)
    {
        try
        {
            await _calibrationOps.StartCalibration(e.Channel, e.Step, e.DurationSeconds);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Calibration error for {Channel} {Step}", e.Channel, e.Step);
            _view.ShowError(string.Format(Strings.Error_CalibrationFailed, e.Channel, e.Step));
        }
    }
}
