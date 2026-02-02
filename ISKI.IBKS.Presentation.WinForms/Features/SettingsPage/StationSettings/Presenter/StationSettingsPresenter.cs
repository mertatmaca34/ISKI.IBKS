using ISKI.IBKS.Application.Common.Configuration;
using System;
using System.Threading.Tasks;
using ISKI.IBKS.Presentation.WinForms.Features.SettingsPage.StationSettings.View;

namespace ISKI.IBKS.Presentation.WinForms.Features.SettingsPage.StationSettings.Presenter;

public sealed class StationSettingsPresenter
{
    private readonly IStationConfiguration _stationConfig;
    private readonly IStationSettingsPageView _view;

    public StationSettingsPresenter(IStationConfiguration stationConfig, IStationSettingsPageView view)
    {
        _stationConfig = stationConfig;
        _view = view;

        _view.Load += (s, e) => LoadSettings();
        _view.SaveRequested += async (s, e) => await SaveSettingsAsync();

        LoadSettings();
    }

    private void LoadSettings()
    {
        try
        {
            _view.StationName = _stationConfig.StationName;
            _view.StationId = _stationConfig.StationId.ToString();
        }
        catch (Exception ex)
        {
            _view.ShowError($"Ayarlar yüklenirken hata: {ex.Message}");
        }
    }

    private async Task SaveSettingsAsync()
    {
        if (!Guid.TryParse(_view.StationId, out Guid stationId))
        {
            _view.ShowError("Geçersiz İstasyon ID (GUID formatı gerekli).");
            return;
        }

        try
        {
            await _stationConfig.SaveStationIdAndNameAsync(stationId, _view.StationName.Trim());
            _view.ShowInfo("İstasyon ayarları kaydedildi. Değişikliklerin tamamı için uygulamayı yeniden başlatmanız gerekebilir.");
        }
        catch (Exception ex)
        {
            _view.ShowError($"Kaydetme hatası: {ex.Message}");
        }
    }
}
