using System;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;

using ISKI.IBKS.Presentation.WinForms.Features.SetupWizard.Model;

namespace ISKI.IBKS.Presentation.WinForms.Features.SetupWizard.MailSettings;

public sealed class MailSettingsStepPresenter
{
    private readonly IMailSettingsStepView _view;
    private readonly SetupState _state;

    public MailSettingsStepPresenter(IMailSettingsStepView view, SetupState state)
    {
        _view = view;
        _state = state;
        _view.TestMailRequested += async (s, e) => await TestMailAsync();
        LoadData();
    }

    private void LoadData()
    {
        _view.SmtpHost = _state.SmtpHost;
        _view.SmtpPort = _state.SmtpPort;
        _view.SmtpUser = _state.SmtpUser;
        _view.SmtpPassword = _state.SmtpPass;
        _view.UseSsl = _state.UseSsl;
        _view.ReceiverMailAddress = _state.SmtpUser; // Default to sender
    }

    public void SaveData()
    {
        _state.SmtpHost = _view.SmtpHost.Trim();
        _state.SmtpPort = (int)_view.SmtpPort;
        _state.SmtpUser = _view.SmtpUser.Trim();
        _state.SmtpPass = _view.SmtpPassword;
        _state.UseSsl = _view.UseSsl;
    }

    public (bool IsValid, string? ErrorMessage) Validate()
    {
        if (string.IsNullOrWhiteSpace(_view.SmtpHost)) return (false, "SMTP sunucusu boş olamaz.");
        if (string.IsNullOrWhiteSpace(_view.SmtpUser)) return (false, "Kullanıcı adı boş olamaz.");
        if (string.IsNullOrWhiteSpace(_view.SmtpPassword)) return (false, "Şifre boş olamaz.");
        return (true, null);
    }

    private async Task TestMailAsync()
    {
        if (string.IsNullOrWhiteSpace(_view.ReceiverMailAddress))
        {
            _view.ShowMessage("Lütfen test mailinin gönderileceği alıcı adresini giriniz.", "Uyarı", true);
            return;
        }

        _view.SetTestButtonState(false, "Test ediliyor...");
        try
        {
            var smtpClient = new SmtpClient(_view.SmtpHost.Trim())
            {
                Port = (int)_view.SmtpPort,
                Credentials = new NetworkCredential(_view.SmtpUser.Trim(), _view.SmtpPassword),
                EnableSsl = _view.UseSsl,
            };

            var message = new MailMessage
            {
                From = new MailAddress(_view.SmtpUser.Trim()),
                Subject = "IBKS Test Mail",
                Body = "Bu bir test e-postasıdır.",
            };
            message.To.Add(_view.ReceiverMailAddress.Trim());

            await smtpClient.SendMailAsync(message);
            _view.ShowMessage("Test e-postası başarıyla gönderildi.", "Başarılı", false);
        }
        catch (Exception ex)
        {
            _view.ShowMessage($"Test e-postası gönderilemedi: {ex.Message}", "Hata", true);
        }
        finally
        {
            _view.SetTestButtonState(true, "Test Et");
        }
    }
}
