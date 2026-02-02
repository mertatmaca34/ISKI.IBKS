using System;

using ISKI.IBKS.Presentation.WinForms.Features.SetupWizard.Model;

namespace ISKI.IBKS.Presentation.WinForms.Features.SetupWizard.CalibrationSettings;

public sealed class CalibrationSettingsStepPresenter
{
    private readonly ICalibrationSettingsStepView _view;
    private readonly SetupState _state;

    public CalibrationSettingsStepPresenter(ICalibrationSettingsStepView view, SetupState state)
    {
        _view = view;
        _state = state;
        LoadData();
    }

    private void LoadData()
    {
        _view.PhZeroRef = _state.PhZeroRef;
        _view.PhSpanRef = _state.PhSpanRef;
        _view.PhDuration = _state.PhCalibrationDuration;
        _view.CondZeroRef = _state.CondZeroRef;
        _view.CondSpanRef = _state.CondSpanRef;
        _view.CondDuration = _state.CondCalibrationDuration;
    }

    public void SaveData()
    {
        _state.PhZeroRef = _view.PhZeroRef;
        _state.PhSpanRef = _view.PhSpanRef;
        _state.PhCalibrationDuration = _view.PhDuration;
        _state.CondZeroRef = _view.CondZeroRef;
        _state.CondSpanRef = _view.CondSpanRef;
        _state.CondCalibrationDuration = _view.CondDuration;
    }

    public (bool IsValid, string? ErrorMessage) Validate()
    {
        if (_view.PhZeroRef == _view.PhSpanRef) return (false, "pH sıfır ve eğim değerleri aynı olamaz.");
        if (_view.CondZeroRef == _view.CondSpanRef) return (false, "İletkenlik sıfır ve eğim değerleri aynı olamaz.");
        return (true, null);
    }
}
