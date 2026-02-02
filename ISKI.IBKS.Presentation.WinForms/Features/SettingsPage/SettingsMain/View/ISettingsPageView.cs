using System;
using System.Windows.Forms;

namespace ISKI.IBKS.Presentation.WinForms.Features.SettingsPage.SettingsMain.View;

public interface ISettingsPageView
{
    event EventHandler ShowStationSettingsRequested;
    event EventHandler ShowApiSettingsRequested;
    event EventHandler ShowPlcSettingsRequested;
    event EventHandler ShowCalibrationSettingsRequested;
    event EventHandler ShowMailServerSettingsRequested;

    void SetContent<T>() where T : UserControl;
}
