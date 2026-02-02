using ISKI.IBKS.Application.Common.Configuration;
using ISKI.IBKS.Presentation.WinForms.Features.SettingsPage.CalibrationSettings.View;
using System.Threading.Tasks;

namespace ISKI.IBKS.Presentation.WinForms.Features.SettingsPage.CalibrationSettings.Presenter;

public sealed class CalibrationSettingsPresenter
{
    private readonly ICalibrationSettingsPageView _view;
    private readonly IStationConfiguration _stationConfig;

    public CalibrationSettingsPresenter(ICalibrationSettingsPageView view, IStationConfiguration stationConfig)
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
            var settings = _stationConfig.Calibration;
            _view.PhZeroRef = settings.PhZeroReference.ToString();
            _view.PhZeroTime = settings.PhZeroDuration.ToString();
            _view.PhSpanRef = settings.PhSpanReference.ToString();
            _view.PhSpanTime = settings.PhSpanDuration.ToString();

            _view.IletkenlikZeroRef = settings.ConductivityZeroReference.ToString();
            _view.IletkenlikZeroTime = settings.ConductivityZeroDuration.ToString();
            _view.IletkenlikSpanRef = settings.ConductivitySpanReference.ToString();
            _view.IletkenlikSpanTime = settings.ConductivitySpanDuration.ToString();

            _view.AkmZeroRef = settings.AkmZeroReference.ToString();
            _view.AkmZeroTime = settings.AkmZeroDuration.ToString();

            _view.KoiZeroRef = settings.KoiZeroReference.ToString();
            _view.KoiZeroTime = settings.KoiZeroDuration.ToString();
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
            await _stationConfig.SaveCalibrationSettingsAsync(new ISKI.IBKS.Application.Common.Configuration.CalibrationSettings
            {
                PhZeroReference = double.TryParse(_view.PhZeroRef, out var pzr) ? pzr : 7.0,
                PhZeroDuration = int.TryParse(_view.PhZeroTime, out var pzd) ? pzd : 60,
                PhSpanReference = double.TryParse(_view.PhSpanRef, out var psr) ? psr : 4.0,
                PhSpanDuration = int.TryParse(_view.PhSpanTime, out var psd) ? psd : 60,

                ConductivityZeroReference = double.TryParse(_view.IletkenlikZeroRef, out var czr) ? czr : 0,
                ConductivityZeroDuration = int.TryParse(_view.IletkenlikZeroTime, out var czd) ? czd : 60,
                ConductivitySpanReference = double.TryParse(_view.IletkenlikSpanRef, out var csr) ? csr : 1413,
                ConductivitySpanDuration = int.TryParse(_view.IletkenlikSpanTime, out var csd) ? csd : 60,

                AkmZeroReference = double.TryParse(_view.AkmZeroRef, out var azr) ? azr : 0,
                AkmZeroDuration = int.TryParse(_view.AkmZeroTime, out var azd) ? azd : 60,

                KoiZeroReference = double.TryParse(_view.KoiZeroRef, out var kzr) ? kzr : 0,
                KoiZeroDuration = int.TryParse(_view.KoiZeroTime, out var kzd) ? kzd : 60
            });

            _view.ShowInfo("Kalibrasyon ayarları kaydedildi.");
        }
        catch (Exception ex)
        {
            _view.ShowError($"Kayıt hatası: {ex.Message}");
        }
    }
}
