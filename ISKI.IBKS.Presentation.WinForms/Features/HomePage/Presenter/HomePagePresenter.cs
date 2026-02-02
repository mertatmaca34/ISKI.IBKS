using ISKI.IBKS.Application.Common.IoT.Snapshots;
using ISKI.IBKS.Application.Common.RemoteApi.SAIS;
using ISKI.IBKS.Infrastructure;
using ISKI.IBKS.Application.Common.IoT.Plc;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using ISKI.IBKS.Application.Common.IoT;
using System;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Collections.Generic;
using ISKI.IBKS.Presentation.WinForms.Features.HomePage.View;

namespace ISKI.IBKS.Presentation.WinForms.Features.HomePage.Presenter;

public sealed class HomePagePresenter
{
    private readonly IAnalogSensorService _analogSensorService;
    private readonly IDigitalSensorService _digitalSensorService;
    private readonly IStationStatusService _stationStatusService;
    private readonly IDataCollectionService _dataCollectionService;
    private readonly IHomePageView _view;
    private readonly ILogger<HomePagePresenter> _logger;
    private Guid _stationId;
    private readonly CancellationTokenSource _cts = new();
    private int _isRefreshing = 0;
    private System.Windows.Forms.Timer _refreshTimer;

    public HomePagePresenter(
        IAnalogSensorService analogSensorService,
        IDigitalSensorService digitalSensorService,
        IStationStatusService stationStatusService,
        IDataCollectionService dataCollectionService,
        IHomePageView view,
        ILogger<HomePagePresenter> logger)
    {
        _analogSensorService = analogSensorService;
        _digitalSensorService = digitalSensorService;
        _stationStatusService = stationStatusService;
        _dataCollectionService = dataCollectionService;
        _view = view;
        _logger = logger;

        _view.Load += async (s, e) => await StartAsync();
        _view.Disposed += (s, e) => Stop();
    }

    public async Task StartAsync()
    {
        try
        {
            _stationId = await _dataCollectionService.GetStationIdAsync();
            await RefreshDataAsync();
            
            _refreshTimer = new System.Windows.Forms.Timer { Interval = 10000 }; // 10 seconds
            _refreshTimer.Tick += async (s, e) => await RefreshDataAsync();
            _refreshTimer.Start();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Failed to start HomePagePresenter");
        }
    }

    public void Stop()
    {
        _refreshTimer?.Stop();
        _refreshTimer?.Dispose();
        _cts.Cancel();
    }

    private async Task RefreshDataAsync()
    {
        if (Interlocked.CompareExchange(ref _isRefreshing, 1, 0) == 1) return;

        try
        {
            var analogReadings = await _analogSensorService.GetReadingsAsync(_stationId, _cts.Token);
            _view.UpdateAnalogReadings(analogReadings);

            var digitalReadings = await _digitalSensorService.GetReadingsAsync(_stationId, _cts.Token);
            _view.UpdateDigitalReadings(digitalReadings);

            var status = await _stationStatusService.GetStatusAsync(_stationId, _cts.Token);
            _view.UpdateStationStatus(status);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error refreshing home page data");
            _view.ShowError("Veriler güncellenirken hata oluştu.");
        }
        finally
        {
            Interlocked.Exchange(ref _isRefreshing, 0);
        }
    }
}
