using ISKI.IBKS.Domain.Abstractions;
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

    public HomePagePresenter(IHomePageView view, IStationSnapshotCache stationSnapshotCache)
    {
        view.Load += OnLoad;

        _view = view;
        _stationSnapshotCache = stationSnapshotCache;
    }

    private void OnLoad(object? sender, EventArgs e)
    {

    }
}
