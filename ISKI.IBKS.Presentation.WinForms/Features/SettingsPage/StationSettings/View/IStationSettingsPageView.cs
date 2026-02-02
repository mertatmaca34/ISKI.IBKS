using System;

namespace ISKI.IBKS.Presentation.WinForms.Features.SettingsPage.StationSettings.View;

public interface IStationSettingsPageView
{
    event EventHandler Load;
    event EventHandler SaveRequested;

    string StationName { get; set; }
    string StationId { get; set; }

    void ShowInfo(string message);
    void ShowError(string message);
}
