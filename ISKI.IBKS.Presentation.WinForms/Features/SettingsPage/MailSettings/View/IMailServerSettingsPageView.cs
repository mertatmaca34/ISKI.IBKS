using System;

namespace ISKI.IBKS.Presentation.WinForms.Features.SettingsPage.MailSettings.View;

public interface IMailServerSettingsPageView
{
    event EventHandler Load;
    event EventHandler SaveRequested;
    event EventHandler UseCredentialsChanged;

    string Host { get; set; }
    int Port { get; set; }
    string Username { get; set; }
    string Password { get; set; }
    bool UseSsl { get; set; }
    string FromName { get; set; }
    bool CredentialsEnabled { get; set; }

    void ShowInfo(string message);
    void ShowError(string message);
}
