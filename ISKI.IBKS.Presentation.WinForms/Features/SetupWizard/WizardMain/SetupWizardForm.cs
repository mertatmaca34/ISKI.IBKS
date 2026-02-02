using System;
using System.IO;
using System.Windows.Forms;
using System.Linq;
using ISKI.IBKS.Shared.Localization;
using Microsoft.Extensions.DependencyInjection;
using ISKI.IBKS.Presentation.WinForms.Features.SetupWizard.WizardMain;
using ISKI.IBKS.Infrastructure.Persistence.Contexts;

namespace ISKI.IBKS.Presentation.WinForms.Features.SetupWizard.WizardMain;

public partial class SetupWizardForm : Form, ISetupWizardView
{
    public static bool IsSetupRequired(IbksDbContext context)
    {
        try
        {
            // 1. Check if we can connect to the DB
            if (!context.Database.CanConnect()) return true;

            // 2. Check if database is seeded (has data)
            if (!context.AlarmDefinitions.Any()) return true;
            
            // DB exists and has data -> setup is NOT required
            return false;
        }
        catch { return true; }
    }

    public void SetNextButtonEnabled(bool enabled) => _btnNext.Enabled = enabled;
    public event EventHandler NextRequested;
    public event EventHandler BackRequested;

    public bool IsCompleted { get; private set; }

    public SetupWizardForm(IServiceProvider serviceProvider)
    {
        InitializeComponent();
        InitializeLocalization();
        SetupFormIcon();
        _btnNext.Click += (s, e) => NextRequested?.Invoke(this, EventArgs.Empty);
        _btnPrevious.Click += (s, e) => BackRequested?.Invoke(this, EventArgs.Empty);

        ActivatorUtilities.CreateInstance<SetupWizardPresenter>(serviceProvider, this);
    }

    private void InitializeLocalization()
    {
        this.Text = Strings.Wizard_AppTitle;
        _lblTitle.Text = Strings.Wizard_Title;
        _btnNext.Text = Strings.Common_Next;
        _btnPrevious.Text = Strings.Common_Back;
    }

    private void SetupFormIcon()
    {
        try
        {
            string iconPath = Path.Combine(AppContext.BaseDirectory, "Resources", "app.ico");
            if (File.Exists(iconPath)) this.Icon = new Icon(iconPath);
        }
        catch { }
    }

    public void SetStepTitle(string title) => _lblTitle.Text = title;
    public void SetStepDescription(string description) => _lblDescription.Text = description;
    public void SetStepIndicator(string text) => _lblStepIndicator.Text = text;
    
    public void ShowStepControl(Control control)
    {
        _contentPanel.Controls.Clear();
        control.Dock = DockStyle.Fill;
        _contentPanel.Controls.Add(control);
    }

    public void SetNextButtonText(string text) => _btnNext.Text = text;
    public void SetBackButtonVisible(bool visible) => _btnPrevious.Visible = visible;

    public void ShowError(string message) => MessageBox.Show(message, Strings.Common_Error, MessageBoxButtons.OK, MessageBoxIcon.Error);
    public void ShowInfo(string message) => MessageBox.Show(message, Strings.Common_Information, MessageBoxButtons.OK, MessageBoxIcon.Information);
    
    public void UpdateTheme(bool hasErrors, string summaryText)
    {
        if (InvokeRequired)
        {
            Invoke(new Action(() => UpdateTheme(hasErrors, summaryText)));
            return;
        }

        var themeColor = hasErrors ? Color.Crimson : Color.FromArgb(0, 120, 215);
        _headerPanel.BackColor = themeColor;
        _btnNext.BackColor = themeColor;
        _lblTitle.Text = summaryText;
    }

    public void CloseWizard()
    {
        this.IsCompleted = true;
        this.Close();
    }
}
