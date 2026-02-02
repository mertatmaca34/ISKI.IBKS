using ISKI.IBKS.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using ISKI.IBKS.Presentation.WinForms.Features.SettingsPage.PlcSettings.View;
using ISKI.IBKS.Application.Common.Configuration;
using System.Text.Json;
using ISKI.IBKS.Shared.Localization;

namespace ISKI.IBKS.Presentation.WinForms.Features.SettingsPage.PlcSettings.Presenter;

public sealed class PlcSettingsPresenter
{
    private readonly IServiceScopeFactory _scopeFactory;
    private readonly IPlcSettingsPageView _view;
    private readonly IStationConfiguration _stationConfig;

    public PlcSettingsPresenter(IServiceScopeFactory scopeFactory, IPlcSettingsPageView view, IStationConfiguration stationConfig)
    {
        _scopeFactory = scopeFactory;
        _view = view;
        _stationConfig = stationConfig;

        _view.Load += async (s, e) => await LoadSettingsAsync();
        _view.SaveRequested += async (s, e) => await SaveSettingsAsync();

        _ = LoadSettingsAsync();
    }

    private async Task LoadSettingsAsync()
    {
        try
        {
            var settings = _stationConfig.Plc;
            _view.PlcIp = settings.Station.IpAddress;
            _view.SelectedSensors = _stationConfig.SelectedSensors.ToList();
        }
        catch (Exception ex)
        {
            _view.ShowError(string.Format(Strings.Error_SettingsLoad, ex.Message));
        }
    }

    private async Task SaveSettingsAsync()
    {
        try
        {
            await _stationConfig.SavePlcSettingsAsync(_view.PlcIp.Trim(), _stationConfig.PlcRack, _stationConfig.PlcSlot, _view.SelectedSensors);
            _view.ShowInfo(Strings.Info_PlcSettingsSaved);
        }
        catch (Exception ex)
        {
            _view.ShowError(string.Format(Strings.Error_Save, ex.Message));
        }
    }
}
