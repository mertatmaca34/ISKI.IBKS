using System;
using System.Collections.Generic;
using System.Net.NetworkInformation;
using System.Threading.Tasks;

using ISKI.IBKS.Presentation.WinForms.Features.SetupWizard.Model;

namespace ISKI.IBKS.Presentation.WinForms.Features.SetupWizard.PlcSettings;

public sealed class PlcSettingsStepPresenter
{
    private readonly IPlcSettingsStepView _view;
    private readonly SetupState _state;

    private static readonly string[] AvailableSensors = new[]
    {
        "TesisDebi", "OlcumCihaziAkisHizi", "Ph", "Iletkenlik", "CozunmusOksijen",
        "Koi", "Akm", "KabinSicakligi", "DesarjDebi", "HariciDebi", "HariciDebi2"
    };

    public PlcSettingsStepPresenter(IPlcSettingsStepView view, SetupState state)
    {
        _view = view;
        _state = state;

        _view.AvailableSensors = new List<string>(AvailableSensors);
        _view.TestConnectionRequested += async (s, e) => await TestConnectionAsync();

        LoadData();
    }

    private void LoadData()
    {
        _view.IpAddress = _state.PlcIpAddress;
        _view.Rack = _state.PlcRack;
        _view.Slot = _state.PlcSlot;
        _view.SelectedSensors = _state.PlcSelectedSensors;
    }

    public void SaveData()
    {
        _state.PlcIpAddress = _view.IpAddress.Trim();
        _state.PlcRack = (int)_view.Rack;
        _state.PlcSlot = (int)_view.Slot;
        _state.PlcSelectedSensors = _view.SelectedSensors;
    }

    public (bool IsValid, string? ErrorMessage) Validate()
    {
        if (string.IsNullOrWhiteSpace(_view.IpAddress)) return (false, "IP adresi boş olamaz.");
        if (!System.Net.IPAddress.TryParse(_view.IpAddress, out _)) return (false, "Geçersiz IP adresi.");
        if (_view.SelectedSensors.Count == 0) return (false, "En az bir sensör seçilmelidir.");
        return (true, null);
    }

    private async Task TestConnectionAsync()
    {
        _view.SetTestButtonState(false, "Test ediliyor...");
        try
        {
            var ping = new Ping();
            var reply = await ping.SendPingAsync(_view.IpAddress.Trim(), 2000);
            if (reply.Status == IPStatus.Success)
                _view.ShowMessage($"Bağlantı başarılı.\nSüre: {reply.RoundtripTime}ms", "Başarılı", false);
            else
                _view.ShowMessage($"Bağlantı başarısız.\nDurum: {reply.Status}", "Hata", true);
        }
        catch (Exception ex)
        {
            _view.ShowMessage($"Bağlantı hatası: {ex.Message}", "Hata", true);
        }
        finally
        {
            _view.SetTestButtonState(true, "Test Et");
        }
    }
}
