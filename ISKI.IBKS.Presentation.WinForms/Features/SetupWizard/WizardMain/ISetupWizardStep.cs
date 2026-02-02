using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ISKI.IBKS.Presentation.WinForms.Features.SetupWizard.WizardMain;

public interface ISetupWizardStep
{
    string Title { get; }

    string Description { get; }

    int StepNumber { get; }

    Task LoadAsync(CancellationToken ct = default);

    (bool IsValid, string? ErrorMessage) Validate();

    Task<bool> SaveAsync(CancellationToken ct = default);

    Control GetControl();
}

