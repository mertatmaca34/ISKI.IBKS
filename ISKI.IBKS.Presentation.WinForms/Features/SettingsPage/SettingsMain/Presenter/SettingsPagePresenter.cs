using System;
using System.Windows.Forms;
using ISKI.IBKS.Presentation.WinForms.Features.SettingsPage.SettingsMain.View;
using ISKI.IBKS.Presentation.WinForms.Features.SettingsPage.ApiSettings.View;
using ISKI.IBKS.Presentation.WinForms.Features.SettingsPage.PlcSettings.View;
using ISKI.IBKS.Presentation.WinForms.Features.SettingsPage.MailSettings.View;
using ISKI.IBKS.Presentation.WinForms.Features.SettingsPage.StationSettings.View;
using ISKI.IBKS.Presentation.WinForms.Features.SettingsPage.CalibrationSettings.View;

namespace ISKI.IBKS.Presentation.WinForms.Features.SettingsPage.SettingsMain.Presenter;

public sealed class SettingsPagePresenter
{
    private readonly ISettingsPageView _view;

    public SettingsPagePresenter(ISettingsPageView view)
    {
        _view = view;

        _view.ShowStationSettingsRequested += (s, e) => _view.SetContent<StationSettingsPage>();
        _view.ShowApiSettingsRequested += (s, e) => _view.SetContent<ApiSettingsPage>();
        _view.ShowPlcSettingsRequested += (s, e) => _view.SetContent<PlcSettingsPage>();
        _view.ShowCalibrationSettingsRequested += (s, e) => _view.SetContent<CalibrationSettingsPage>();
        _view.ShowMailServerSettingsRequested += (s, e) => _view.SetContent<MailServerSettingsPage>();
    }
}
