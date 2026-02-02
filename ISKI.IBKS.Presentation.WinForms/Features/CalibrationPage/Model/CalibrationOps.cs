using ISKI.IBKS.Application.Common.IoT.Plc;
using ISKI.IBKS.Application.Common.IoT;
using ISKI.IBKS.Application.Common.RemoteApi.SAIS;
using ISKI.IBKS.Application.Common.Configuration;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ISKI.IBKS.Presentation.WinForms.Features.CalibrationPage.Model;

public class CalibrationOps
{
    private readonly IPlcClient _plcClient;
    private readonly IDataCollectionService _dataCollectionService;
    private readonly ISaisApiClient _saisApiClient;
    private readonly ILogger _logger;
    private readonly IStationConfiguration _stationConfig;

    public bool isCalibrationInProgress;

    public CalibrationOps(
        IPlcClient plcClient,
        IDataCollectionService dataCollectionService,
        ISaisApiClient saisApiClient,
        IStationConfiguration stationConfig,
        ILogger logger)
    {
        _plcClient = plcClient;
        _dataCollectionService = dataCollectionService;
        _saisApiClient = saisApiClient;
        _stationConfig = stationConfig;
        _logger = logger;
    }

    public async Task StartCalibration(string calibrationName, string calibrationType, int calibrationTime)
    {
        if (isCalibrationInProgress)
        {
            return;
        }

        isCalibrationInProgress = true;

        if (calibrationType == "Zero")
            await StartZeroCalibration(calibrationName, calibrationTime);
        else
            await StartSpanCalibration(calibrationName, calibrationTime);
    }

    private async Task StartZeroCalibration(string calibrationName, int calibrationTime)
    {
        isCalibrationInProgress = false;
        await Task.CompletedTask;
    }

    private async Task StartSpanCalibration(string calibrationName, int calibrationTime)
    {
        isCalibrationInProgress = false;
        await Task.CompletedTask;
    }
}
