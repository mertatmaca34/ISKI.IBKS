using System;

namespace ISKI.IBKS.Presentation.WinForms.Features.SettingsPage.ApiSettings.View;

public interface IApiSettingsPageView
{
    event EventHandler Load;
    event EventHandler SaveRequested;

    string BaseUrl { get; set; }
    string Username { get; set; }
    string Password { get; set; }

    void ShowInfo(string message);
    void ShowError(string message);
}
