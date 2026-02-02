using System;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using ISKI.IBKS.Presentation.WinForms.Features.SetupWizard.Model;
using ISKI.IBKS.Presentation.WinForms.Features.SetupWizard.SaisApiSettings;
using ISKI.IBKS.Presentation.WinForms.Features.SetupWizard.WizardMain;
using ISKI.IBKS.Shared.Localization;
using System.Linq;

namespace ISKI.IBKS.Presentation.WinForms.Features.SetupWizard.SaisApiSettings;

public partial class SaisApiSettingsStep : UserControl, ISaisApiSettingsStepView, ISetupWizardStep
{
    private readonly SaisApiSettingsStepPresenter _presenter;

    public string Title => Strings.Wizard_SaisTitle;
    public string Description => Strings.Wizard_SaisDesc;
    public int StepNumber => 2;

    public string ApiUrl { get => _txtApiUrl.Text; set => _txtApiUrl.Text = value; }
    public string UserName { get => _txtUserName.Text; set => _txtUserName.Text = value; }
    public string Password { get => _txtPassword.Text; set => _txtPassword.Text = value; }

    public SaisApiSettingsStep(SetupState state)
    {
        InitializeComponent();
        InitializeLocalization();
        _presenter = new SaisApiSettingsStepPresenter(this, state);
    }

    public Task LoadAsync(CancellationToken ct = default) => Task.CompletedTask;
    public (bool IsValid, string? ErrorMessage) Validate() => _presenter.Validate();
    public Task<bool> SaveAsync(CancellationToken ct = default)
    {
        _presenter.SaveData();
        return Task.FromResult(true);
    }

    public Control GetControl() => this;

    private void InitializeLocalization()
    {
        lblUrl.Text = Strings.Sais_Url;
        lblUser.Text = Strings.Mail_Username;
        lblPass.Text = Strings.Mail_Password;
    }
}
