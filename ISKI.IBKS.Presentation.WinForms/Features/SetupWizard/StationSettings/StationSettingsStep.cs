using System;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using ISKI.IBKS.Presentation.WinForms.Features.SetupWizard.Model;
using ISKI.IBKS.Presentation.WinForms.Features.SetupWizard.StationSettings;
using ISKI.IBKS.Presentation.WinForms.Features.SetupWizard.WizardMain;
using ISKI.IBKS.Shared.Localization;
using System.Linq;

namespace ISKI.IBKS.Presentation.WinForms.Features.SetupWizard.StationSettings;

public partial class StationSettingsStep : UserControl, IStationSettingsStepView, ISetupWizardStep
{
    private readonly StationSettingsStepPresenter _presenter;

    public string Title => Strings.Wizard_StationTitle;
    public string Description => Strings.Wizard_StationDesc;
    public int StepNumber => 3;

    public string StationId { get => _txtStationId.Text; set => _txtStationId.Text = value; }
    public string StationName { get => _txtStationName.Text; set => _txtStationName.Text = value; }
    public string LocalApiHost { get => _txtLocalApiHost.Text; set => _txtLocalApiHost.Text = value; }

    public StationSettingsStep(SetupState state)
    {
        InitializeComponent();
        InitializeLocalization();
        _presenter = new StationSettingsStepPresenter(this, state);
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
        lblId.Text = Strings.Station_Id;
        lblName.Text = Strings.Station_Name;
        lblHost.Text = Strings.Station_ApiHost;
        lblPort.Text = Strings.Station_ApiPort;
        lblUser.Text = Strings.Station_ApiUser;
        lblPass.Text = Strings.Station_ApiPass;
    }
}
