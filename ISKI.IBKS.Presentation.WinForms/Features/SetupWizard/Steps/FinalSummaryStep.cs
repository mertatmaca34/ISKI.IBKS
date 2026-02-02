using System;
using System.Drawing;
using System.Windows.Forms;
using ISKI.IBKS.Presentation.WinForms.Features.SetupWizard.Model;
using ISKI.IBKS.Presentation.WinForms.Features.SetupWizard.WizardMain;
using ISKI.IBKS.Shared.Localization;
using Microsoft.Extensions.DependencyInjection;

namespace ISKI.IBKS.Presentation.WinForms.Features.SetupWizard.Steps;

public partial class FinalSummaryStep : UserControl, ISetupWizardStep, IFinalSummaryStepView
{
    private readonly IServiceProvider _serviceProvider;
    private FinalSummaryStepPresenter _presenter;

    public string Title => Strings.Wizard_FinalSummaryTitle;
    public string Description => Strings.Wizard_FinalSummaryDesc;
    public int StepNumber => 6; // Or however many steps there are

    public FinalSummaryStep(IServiceProvider serviceProvider)
    {
        InitializeComponent();
        _serviceProvider = serviceProvider;
        _presenter = ActivatorUtilities.CreateInstance<FinalSummaryStepPresenter>(_serviceProvider, this);
    }

    private void InitializeComponent()
    {
        this.SuspendLayout();
        
        var layout = new TableLayoutPanel
        {
            Dock = DockStyle.Fill,
            ColumnCount = 2,
            RowCount = 5,
            Padding = new Padding(20)
        };
        layout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 70F));
        layout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 30F));

        // Add proper row styles
        for (int i = 0; i < 5; i++)
        {
            layout.RowStyles.Add(new RowStyle(SizeType.AutoSize));
        }

        _lblDb = CreateStatusLabel(Strings.Wizard_CheckDb);
        _lblPlc = CreateStatusLabel(Strings.Wizard_CheckPlc);
        _lblSais = CreateStatusLabel(Strings.Wizard_CheckSais);
        _lblMail = CreateStatusLabel(Strings.Wizard_CheckMail);

        _iconDb = CreateStatusIcon();
        _iconPlc = CreateStatusIcon();
        _iconSais = CreateStatusIcon();
        _iconMail = CreateStatusIcon();

        layout.Controls.Add(_lblDb, 0, 0);
        layout.Controls.Add(_iconDb, 1, 0);
        layout.Controls.Add(_lblPlc, 0, 1);
        layout.Controls.Add(_iconPlc, 1, 1);
        layout.Controls.Add(_lblSais, 0, 2);
        layout.Controls.Add(_iconSais, 1, 2);
        layout.Controls.Add(_lblMail, 0, 3);
        layout.Controls.Add(_iconMail, 1, 3);

        this.Controls.Add(layout);
        this.Name = "FinalSummaryStep";
        this.Size = new Size(600, 400);
        this.ResumeLayout(false);
    }

    private Label _lblDb, _lblPlc, _lblSais, _lblMail;
    private Label _iconDb, _iconPlc, _iconSais, _iconMail;

    private Label CreateStatusLabel(string text) => new Label { Text = text, AutoSize = true, Font = new Font("Segoe UI", 11F), Margin = new Padding(0, 10, 0, 10) };
    private Label CreateStatusIcon() => new Label { Text = "...", AutoSize = true, Font = new Font("Segoe UI", 11F, FontStyle.Bold), Margin = new Padding(0, 10, 0, 10), TextAlign = ContentAlignment.MiddleCenter };

    public Control GetControl() => this;

    public async Task LoadAsync(CancellationToken ct = default) => await _presenter.RunTestsAsync();

    public async Task<bool> SaveAsync(CancellationToken ct = default) => await Task.FromResult(true);

    public (bool IsValid, string? ErrorMessage) Validate() => (true, null);

    public void SetDbStatus(string message, bool isSuccess) => UpdateStatus(_iconDb, message, isSuccess);
    public void SetPlcStatus(string message, bool isSuccess) => UpdateStatus(_iconPlc, message, isSuccess);
    public void SetSaisStatus(string message, bool isSuccess) => UpdateStatus(_iconSais, message, isSuccess);
    public void SetMailStatus(string message, bool isSuccess) => UpdateStatus(_iconMail, message, isSuccess);

    private void UpdateStatus(Label iconLabel, string message, bool isSuccess)
    {
        if (InvokeRequired)
        {
            Invoke(new Action(() => UpdateStatus(iconLabel, message, isSuccess)));
            return;
        }
        iconLabel.Text = isSuccess ? "✔ Başarılı" : "❌ Başarısız";
        iconLabel.ForeColor = isSuccess ? Color.Green : Color.Red;
    }

    public void SetTestingInProgress(bool inProgress) { }
    public event EventHandler<int> TestsCompleted;

    public void OnTestsCompleted(int errorCount)
    {
        if (InvokeRequired)
        {
            Invoke(new Action(() => OnTestsCompleted(errorCount)));
            return;
        }
        
        TestsCompleted?.Invoke(this, errorCount);
    }
}
