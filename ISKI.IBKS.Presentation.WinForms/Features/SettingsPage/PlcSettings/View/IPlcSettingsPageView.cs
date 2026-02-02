using System;
using System.Collections.Generic;

namespace ISKI.IBKS.Presentation.WinForms.Features.SettingsPage.PlcSettings.View;

public interface IPlcSettingsPageView
{
    event EventHandler Load;
    event EventHandler SaveRequested;

    string PlcIp { get; set; }
    List<string> SelectedSensors { get; set; }

    void ShowInfo(string message);
    void ShowError(string message);
}
