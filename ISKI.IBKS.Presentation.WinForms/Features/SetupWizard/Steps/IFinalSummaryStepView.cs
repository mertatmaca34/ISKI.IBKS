using System.Windows.Forms;
using ISKI.IBKS.Presentation.WinForms.Features.SetupWizard.WizardMain;

namespace ISKI.IBKS.Presentation.WinForms.Features.SetupWizard.Steps;

public interface IFinalSummaryStepView
{
    void SetDbStatus(string message, bool isSuccess);
    void SetPlcStatus(string message, bool isSuccess);
    void SetSaisStatus(string message, bool isSuccess);
    void SetMailStatus(string message, bool isSuccess);
    
    void SetTestingInProgress(bool inProgress);
    event EventHandler<int> TestsCompleted;
    void OnTestsCompleted(int errorCount);
}
