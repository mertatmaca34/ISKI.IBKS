using System;
using System.Windows.Forms;

namespace ISKI.IBKS.Presentation.WinForms.Features.SetupWizard.WizardMain;

public interface ISetupWizardView
{
    event EventHandler NextRequested;
    event EventHandler BackRequested;

    void SetStepTitle(string title);
    void SetStepDescription(string description);
    void SetStepIndicator(string text);
    void ShowStepControl(Control control);
    void SetNextButtonText(string text);
    void SetNextButtonEnabled(bool enabled);
    void SetBackButtonVisible(bool visible);

    void ShowError(string message);
    void ShowInfo(string message);
    void UpdateTheme(bool hasErrors, string summaryText);
    void CloseWizard();
}
