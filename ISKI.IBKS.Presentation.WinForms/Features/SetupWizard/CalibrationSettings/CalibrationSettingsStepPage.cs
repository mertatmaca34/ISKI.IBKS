using System;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using ISKI.IBKS.Presentation.WinForms.Features.SetupWizard.Model;
using ISKI.IBKS.Presentation.WinForms.Features.SetupWizard.CalibrationSettings;
using ISKI.IBKS.Presentation.WinForms.Features.SetupWizard.WizardMain;
using ISKI.IBKS.Shared.Localization;
using System.Linq;

namespace ISKI.IBKS.Presentation.WinForms.Features.SetupWizard.CalibrationSettings;

public partial class CalibrationSettingsStepPage : UserControl, ICalibrationSettingsStepView, ISetupWizardStep
{
    private readonly CalibrationSettingsStepPresenter _presenter;

    public string Title => Strings.Wizard_CalibrationTitle;
    public string Description => Strings.Wizard_CalibrationDesc;
    public int StepNumber => 4;

    public double PhZeroRef { get => (double)NumericUpDownPhZeroRef.Value; set => NumericUpDownPhZeroRef.Value = (decimal)value; }
    public double PhSpanRef { get => (double)NumericUpDownPhSpanRef.Value; set => NumericUpDownPhSpanRef.Value = (decimal)value; }
    public int PhDuration { get => (int)NumericUpDownPhDuration.Value; set => NumericUpDownPhDuration.Value = (decimal)value; }
    public double CondZeroRef { get => (double)NumericUpDownIletkenlikZeroRef.Value; set => NumericUpDownIletkenlikZeroRef.Value = (decimal)value; }
    public double CondSpanRef { get => (double)NumericUpDownIletkenlikSpanRef.Value; set => NumericUpDownIletkenlikSpanRef.Value = (decimal)value; }
    public int CondDuration { get => (int)NumericUpDownIletkenlikDuration.Value; set => NumericUpDownIletkenlikDuration.Value = (decimal)value; }

    public CalibrationSettingsStepPage(SetupState state)
    {
        InitializeComponent();
        InitializeLocalization();
        _presenter = new CalibrationSettingsStepPresenter(this, state);
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
        LabelPh.Text = Strings.Cal_PhTitle;
        LabelPhZero.Text = Strings.Cal_ZeroRef;
        LabelPhSpan.Text = Strings.Cal_SpanRef;
        LabelPhDur.Text = Strings.Cal_Duration;

        LabelIletkenlik.Text = Strings.Cal_ConductivityTitle;
        LabelIletkenlikZero.Text = Strings.Cal_ZeroRef;
        LabelIletkenlikSpan.Text = Strings.Cal_SpanRef;
        LabelIletkenlikDur.Text = Strings.Cal_Duration;
    }
}
