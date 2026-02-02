using System;

namespace ISKI.IBKS.Presentation.WinForms.Features.SetupWizard.SaisApiSettings;

public interface ISaisApiSettingsStepView
{
    string ApiUrl { get; set; }
    string UserName { get; set; }
    string Password { get; set; }
}
