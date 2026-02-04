namespace ISKI.IBKS.Presentation.WinForms.Features.SetupWizard;

/// <summary>
/// Mail Sunucu Ayarları adımı
/// </summary>
public partial class MailSettingsStep : UserControl, ISetupWizardStep
{
    private readonly SetupState _state;

    public string Title => "Mail Sunucu Ayarları";
    public string Description => "SMTP mail sunucu bilgilerini yapılandırın.";
    public int StepNumber => 5;

    public MailSettingsStep(SetupState state)
    {
        _state = state;
        InitializeComponent();
        SetupEvents();
    }

    private void SetupEvents()
    {
        _btnTestMail.Click += async (s, e) => await TestMailAsync();
    }

    public Task LoadAsync(CancellationToken ct = default)
    {
        _txtSmtpHost.Text = _state.SmtpHost;
        _numSmtpPort.Value = _state.SmtpPort;
        _txtSmtpUser.Text = _state.SmtpUserName;
        _txtSmtpPassword.Text = _state.SmtpPassword;
        _chkSsl.Checked = _state.SmtpUseSsl;
        return Task.CompletedTask;
    }

    public (bool IsValid, string? ErrorMessage) Validate()
    {
        if (string.IsNullOrWhiteSpace(_txtSmtpHost.Text))
            return (false, "SMTP sunucu adresi boş olamaz.");

        if (string.IsNullOrWhiteSpace(_txtSmtpUser.Text))
            return (false, "SMTP kullanıcı adı boş olamaz.");

        return (true, null);
    }

    public Task<bool> SaveAsync(CancellationToken ct = default)
    {
        _state.SmtpHost = _txtSmtpHost.Text.Trim();
        _state.SmtpPort = (int)_numSmtpPort.Value;
        _state.SmtpUserName = _txtSmtpUser.Text.Trim();
        _state.SmtpPassword = _txtSmtpPassword.Text;
        _state.SmtpUseSsl = _chkSsl.Checked;
        return Task.FromResult(true);
    }

    public Control GetControl() => this;

    private async Task TestMailAsync()
    {
        _btnTestMail.Enabled = false;
        _btnTestMail.Text = "Gönderiliyor...";

        try
        {
            using var smtpClient = new System.Net.Mail.SmtpClient(_txtSmtpHost.Text, (int)_numSmtpPort.Value)
            {
                EnableSsl = _chkSsl.Checked,
                Credentials = new System.Net.NetworkCredential(_txtSmtpUser.Text, _txtSmtpPassword.Text),
                Timeout = 10000
            };

            var testAddress = _txtSmtpUser.Text;
            var message = new System.Net.Mail.MailMessage(testAddress, testAddress, 
                "ISKI IBKS - Test Maili", 
                $"Bu bir test mailidir.\nTarih: {DateTime.Now:dd.MM.yyyy HH:mm}");

            await smtpClient.SendMailAsync(message);

            MessageBox.Show("Test maili başarıyla gönderildi!", "Başarılı", 
                MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
        catch (Exception ex)
        {
            MessageBox.Show($"Mail gönderimi başarısız.\n{ex.Message}", "Hata", 
                MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
        finally
        {
            _btnTestMail.Enabled = true;
            _btnTestMail.Text = "Test Maili Gönder";
        }
    }
}
