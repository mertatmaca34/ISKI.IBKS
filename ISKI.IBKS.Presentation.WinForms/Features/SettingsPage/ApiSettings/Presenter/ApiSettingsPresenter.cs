using System;
using System.IO;
using System.Text.Json;
using System.Text.Json.Nodes;

using ISKI.IBKS.Presentation.WinForms.Features.SettingsPage.ApiSettings.View;
using ISKI.IBKS.Application.Common.Configuration; // Assuming IStationConfiguration is here

namespace ISKI.IBKS.Presentation.WinForms.Features.SettingsPage.ApiSettings.Presenter;

public sealed class ApiSettingsPresenter
{
    private readonly IApiSettingsPageView _view;
    private readonly IStationConfiguration _stationConfig;

    public ApiSettingsPresenter(IApiSettingsPageView view, IStationConfiguration stationConfig)
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
            var settings = _stationConfig.Sais;
            _view.BaseUrl = settings.BaseUrl;
            _view.Username = settings.UserName;
            _view.Password = settings.Password;
        }
        catch (Exception ex)
        {
            _view.ShowError($"Ayarlar yüklenirken hata: {ex.Message}");
        }
    }

    private async Task SaveSettingsAsync()
    {
        try
        {
            await _stationConfig.SaveSaisSettingsAsync(new SaisSettings
            {
                BaseUrl = _view.BaseUrl.Trim(),
                UserName = _view.Username.Trim(),
                Password = _view.Password.Trim()
            });

            _view.ShowInfo("API ayarları kaydedildi.");
        }
        catch (Exception ex)
        {
            _view.ShowError($"Kayıt hatası: {ex.Message}");
        }
    }
}
