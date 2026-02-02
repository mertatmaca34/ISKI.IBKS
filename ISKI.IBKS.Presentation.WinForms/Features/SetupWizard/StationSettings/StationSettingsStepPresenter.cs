using System;

using ISKI.IBKS.Presentation.WinForms.Features.SetupWizard.Model;

namespace ISKI.IBKS.Presentation.WinForms.Features.SetupWizard.StationSettings;

public sealed class StationSettingsStepPresenter
{
    private readonly IStationSettingsStepView _view;
    private readonly SetupState _state;

    public StationSettingsStepPresenter(IStationSettingsStepView view, SetupState state)
    {
        _view = view;
        _state = state;
        LoadData();
    }

    private void LoadData()
    {
        _view.StationId = _state.StationId.ToString();
        _view.StationName = _state.StationName;
        _view.LocalApiHost = _state.LocalApiHost;
    }

    public void SaveData()
    {
        if (Guid.TryParse(_view.StationId, out Guid id)) _state.StationId = id;
        _state.StationName = _view.StationName.Trim();
        _state.LocalApiHost = _view.LocalApiHost.Trim();
    }

    public (bool IsValid, string? ErrorMessage) Validate()
    {
        if (!Guid.TryParse(_view.StationId, out _)) return (false, "Geçersiz İstasyon ID.");
        if (string.IsNullOrWhiteSpace(_view.StationName)) return (false, "İstasyon adı boş olamaz.");
        if (string.IsNullOrWhiteSpace(_view.LocalApiHost)) return (false, "Yerel API adresi boş olamaz.");
        return (true, null);
    }
}
