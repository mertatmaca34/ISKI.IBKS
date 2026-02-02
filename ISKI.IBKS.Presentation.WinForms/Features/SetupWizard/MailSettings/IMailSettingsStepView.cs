using System;

namespace ISKI.IBKS.Presentation.WinForms.Features.SetupWizard.MailSettings;

public interface IMailSettingsStepView
{
    event EventHandler TestMailRequested;

    string SmtpHost { get; set; }
    decimal SmtpPort { get; set; }
    string SmtpUser { get; set; }
    string SmtpPassword { get; set; }
    bool UseSsl { get; set; }
    string ReceiverMailAddress { get; set; }

    void SetTestButtonState(bool enabled, string text);
    void ShowMessage(string message, string title, bool isError);
}
