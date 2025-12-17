using ISKI.IBKS.Application.Features.AnalogSensors.Services;
using ISKI.IBKS.Application.Features.StationStatus.Services;
using ISKI.IBKS.Infrastructure.IoT.Plc.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ISKI.IBKS.Presentation.WinForms.Features.HomePage;

public sealed class HomePagePresenter
{
    //Services
    private readonly IAnalogSensorService _analogSensorService;
    private readonly IStationStatusService _stationStatusService;
    private readonly IHomePageView _view;
    private readonly IOptions<PlcSettings> _options;
    private readonly ILogger<HomePagePresenter> _logger;
    private readonly CancellationTokenSource _cts = new();
    //Events
    private readonly System.Windows.Forms.Timer _refreshTimer = new();
    
    //Fields
    private Guid _stationId;
    private int _isRefreshing;

    public HomePagePresenter(
        IHomePageView view,
        IAnalogSensorService analogSensorService,
        IStationStatusService stationStatusService,
        IOptions<PlcSettings> options,
        ILogger<HomePagePresenter> logger)
    {
        _view = view;
        _analogSensorService = analogSensorService;
        _stationStatusService = stationStatusService;
        _options = options;
        _logger = logger;

        view.Load += OnLoad;
        view.Disposed += OnDisposed;
    }

    private void OnLoad(object? sender, EventArgs e)
    {
        _stationId = _options.Value.Station.StationId;

        _refreshTimer.Tick += RefreshTimerTick;
        _refreshTimer.Interval = 1000;
        _refreshTimer.Start();

        _ = TickHandlerAsync();
    }

    private async void RefreshTimerTick(object? sender, EventArgs e)
    {
        await TickHandlerAsync();
    }

    private async Task TickHandlerAsync()
    {
        if (Interlocked.Exchange(ref _isRefreshing, 1) == 1)
            return;

        try
        {
            var readings = await _analogSensorService.GetChannelsAsync(_stationId, _cts.Token);
            _view.RenderAnalogChannels(readings);

            var stationStatus = await _stationStatusService.GetStationStatusAsync(_stationId, _cts.Token);
            _view.RenderStationStatusBar(stationStatus);
        }
        catch(Exception ex)
        {
            _logger.LogError(ex, "Analog kanallar getirilirken hata oluştu.");
        }
        finally
        {
            Interlocked.Exchange(ref _isRefreshing, 0);
        }
    }

    private void RenderStationStatusBar()
    {

    }

    private void OnDisposed(object? sender, EventArgs e)
    {
        _cts.Cancel();
        _cts.Dispose();

        _refreshTimer.Stop();
        _refreshTimer.Tick -= RefreshTimerTick;
        _refreshTimer.Dispose();

        _view.Load -= OnLoad;
        _view.Disposed -= OnDisposed;
    }   
}
