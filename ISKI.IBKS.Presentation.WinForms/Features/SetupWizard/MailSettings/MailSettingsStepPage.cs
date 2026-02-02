using System;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using ISKI.IBKS.Presentation.WinForms.Features.SetupWizard.Model;
using ISKI.IBKS.Presentation.WinForms.Features.SetupWizard.MailSettings;
using ISKI.IBKS.Presentation.WinForms.Features.SetupWizard.WizardMain;
using ISKI.IBKS.Shared.Localization;

namespace ISKI.IBKS.Presentation.WinForms.Features.SetupWizard.MailSettings;

public partial class MailSettingsStepPage : UserControl, IMailSettingsStepView, ISetupWizardStep
{
    private readonly MailSettingsStepPresenter _presenter;

    public event EventHandler TestMailRequested;

    public string Title => Strings.Wizard_MailTitle;
    public string Description => Strings.Wizard_MailDesc;
    public int StepNumber => 5;

    public string SmtpHost { get => TextBoxSmtpHost.Text; set => TextBoxSmtpHost.Text = value; }
    public decimal SmtpPort { get => NumericUpDownSmtpPort.Value; set => NumericUpDownSmtpPort.Value = value; }
    public string SmtpUser { get => TextBoxUser.Text; set => TextBoxUser.Text = value; }
    public string SmtpPassword { get => TextBoxSmtpPassword.Text; set => TextBoxSmtpPassword.Text = value; }
    public bool UseSsl { get => CheckBoxSsl.Checked; set => CheckBoxSsl.Checked = value; }
    public string ReceiverMailAddress { get => TextBoxReceiverMailAddress.Text; set => TextBoxReceiverMailAddress.Text = value; }

    public MailSettingsStepPage(SetupState state)
    {
        InitializeComponent();
        InitializeLocalization();
        _presenter = new MailSettingsStepPresenter(this, state);
        ButtonSendTestMail.Click += (s, e) => TestMailRequested?.Invoke(this, EventArgs.Empty);
    }

    public Task LoadAsync(CancellationToken ct = default) => Task.CompletedTask;
    public (bool IsValid, string? ErrorMessage) Validate() => _presenter.Validate();
    public Task<bool> SaveAsync(CancellationToken ct = default)
    {
        _presenter.SaveData();
        return Task.FromResult(true);
    }

    public Control GetControl() => this;

    public void SetTestButtonState(bool enabled, string text)
    {
        ButtonSendTestMail.Enabled = enabled;
        ButtonSendTestMail.Text = text;
    }

    public void ShowMessage(string message, string title, bool isError)
    {
        MessageBox.Show(message, title, MessageBoxButtons.OK, isError ? MessageBoxIcon.Error : MessageBoxIcon.Information);
    }

    private void InitializeLocalization()
    {
        LabelHost.Text = Strings.Mail_SmtpHost;
        LabelPort.Text = Strings.Mail_Port;
        LabelUser.Text = Strings.Mail_Username;
        LabelPassword.Text = Strings.Mail_Password;
        
        // Controls
        CheckBoxSsl.Text = Strings.Mail_UseSsl;
        LabelReceiver.Text = Strings.Mail_Receiver;
        ButtonSendTestMail.Text = Strings.Mail_TestButton;
    }
}
