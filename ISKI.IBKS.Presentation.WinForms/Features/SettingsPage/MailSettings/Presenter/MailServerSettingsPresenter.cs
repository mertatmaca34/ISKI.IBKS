using System;
using System.IO;
using System.Text.Json;
using ISKI.IBKS.Application.Common.Configuration;
using ISKI.IBKS.Presentation.WinForms.Features.SettingsPage.MailSettings.View;

namespace ISKI.IBKS.Presentation.WinForms.Features.SettingsPage.MailSettings.Presenter;

public sealed class MailServerSettingsPresenter
{
    private readonly IMailServerSettingsPageView _view;
    private readonly IStationConfiguration _stationConfig;

    public MailServerSettingsPresenter(IMailServerSettingsPageView view, IStationConfiguration stationConfig)
    {
        _view = view;
        _stationConfig = stationConfig;

        _view.Load += (s, e) => LoadSettings();
        _view.SaveRequested += async (s, e) => await SaveSettingsAsync();

        LoadSettings();
    }

    private void LoadSettings()
    {
        try
        {
            var settings = _stationConfig.Mail;
            _view.Host = settings.SmtpHost;
            _view.Port = settings.SmtpPort;
            _view.Username = settings.Username;
            _view.Password = settings.Password;
            _view.UseSsl = settings.UseSsl;
            _view.FromName = settings.FromName;
            
            _view.CredentialsEnabled = !string.IsNullOrEmpty(settings.Username);
        }
        catch (Exception ex)
        {
            _view.ShowError($"Ayarlar yüklenirken hata: {ex.Message}");
        }
    }

    private async Task SaveSettingsAsync() // Renamed and made async
    {
        try
        {
            await _stationConfig.SaveMailSettingsAsync( // Using IStationConfiguration
                _view.Host.Trim(),
                _view.Port,
                _view.Username.Trim(),
                _view.Password,
                _view.UseSsl);

            _view.ShowInfo("Mail sunucu ayarları ve gönderen bilgisi kaydedildi.");
        }
        catch (Exception ex)
        {
            _view.ShowError($"Kayıt hatası: {ex.Message}");
        }
    }
}
