using ISKI.IBKS.Domain.Abstractions;
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
    private readonly IHomePageView _view;
    private readonly IOptions<PlcSettings> _options;

    public HomePagePresenter(IHomePageView view, IStationSnapshotCache stationSnapshotCache, IOptions<PlcSettings> options)
    {
        view.Load += OnLoad;

        _view = view;
        _stationSnapshotCache = stationSnapshotCache;
        _options = options;
    }

    private void OnLoad(object? sender, EventArgs e)
    {

    }

    public async Task RefreshAsync()
    {
        var station = _options.Value.Station;

        var snapshot = await _stationSnapshotCache.GetLast(station.IpAddress);


    }
}
