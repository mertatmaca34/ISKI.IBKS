using ISKI.IBKS.Application.Features.AnalogSensors.Services;
using ISKI.IBKS.Domain.Abstractions;
using ISKI.IBKS.Domain.Entities;
using ISKI.IBKS.Infrastructure.IoT.Plc.Configuration;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ISKI.IBKS.Presentation.WinForms.Features.HomePage;

public class HomePagePresenter
{
    private readonly IStationSnapshotCache _stationSnapshotCache;
    private readonly IAnalogSensorService _analogSensorService;
    private readonly IHomePageView _view;
    private readonly IOptions<PlcSettings> _options;
    private readonly System.Windows.Forms.Timer _refreshTimer = new();

    public HomePagePresenter(IHomePageView view, IStationSnapshotCache stationSnapshotCache, IAnalogSensorService analogSensorService,
        IOptions<PlcSettings> options)
    {
        view.Load += OnLoad;

        _view = view;
        _analogSensorService = analogSensorService;
        _stationSnapshotCache = stationSnapshotCache;
        _options = options;
    }
    public async Task LoadAnalogChannelsAsync(Guid stationId, CancellationToken ct)
    {
        var readings = await _analogSensorService.GetChannelsAsync(stationId, ct);

        _view.RenderAnalogChannels(readings);
    }

    private async void OnLoad(object? sender, EventArgs e)
    {
        var stationId = _options.Value.Station.StationId;

        _refreshTimer.Interval = 5000; // 1 sn (istersen config’ten al)

        _refreshTimer.Tick += async (_, __) => await LoadAnalogChannelsAsync(stationId,CancellationToken.None);
        _refreshTimer.Start();

        await LoadAnalogChannelsAsync(stationId, CancellationToken.None);
    }
}
