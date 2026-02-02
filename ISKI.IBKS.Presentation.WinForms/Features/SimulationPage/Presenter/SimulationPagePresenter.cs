using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using System.Threading.Tasks;
using ISKI.IBKS.Application.Common.IoT.Snapshots;
using ISKI.IBKS.Application.Common.IoT.Plc;
using ISKI.IBKS.Presentation.WinForms.Features.SimulationPage.View;

namespace ISKI.IBKS.Presentation.WinForms.Features.SimulationPage.Presenter;

public sealed class SimulationPagePresenter
{
    private readonly IStationSnapshotCache _stationSnapshotCache;
    private readonly PlcSettings _plcSettings;
    private readonly ISimulationPageView _view;
    private readonly ILogger<SimulationPagePresenter> _logger;
    private readonly System.Windows.Forms.Timer _timer;
    private bool _blinkState;
    private bool _isRefreshing;

    public SimulationPagePresenter(
        IStationSnapshotCache stationSnapshotCache,
        IOptions<PlcSettings> plcSettings,
        ISimulationPageView view,
        ILogger<SimulationPagePresenter> logger)
    {
        _stationSnapshotCache = stationSnapshotCache;
        _plcSettings = plcSettings.Value;
        _view = view;
        _logger = logger;

        _view.Load += (s, e) => _timer.Start();
        _view.Disposed += (s, e) => Stop();

        _timer = new System.Windows.Forms.Timer { Interval = 1000 };
        _timer.Tick += async (s, e) => await RefreshAsync();
    }

    private async Task RefreshAsync()
    {
        if (_isRefreshing) return;
        _isRefreshing = true;

        try
        {
            var snapshot = await _stationSnapshotCache.Get(_plcSettings.Station.StationId);
            if (snapshot != null)
            {
                _blinkState = !_blinkState;
                _view.UpdateDisplay(snapshot, _blinkState);
            }
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Simulation refresh error");
        }
        finally
        {
            _isRefreshing = false;
        }
    }

    public void Stop()
    {
        _timer.Stop();
        _timer.Dispose();
    }
}

