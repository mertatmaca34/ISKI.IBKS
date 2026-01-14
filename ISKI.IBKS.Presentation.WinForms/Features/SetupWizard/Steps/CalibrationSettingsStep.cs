namespace ISKI.IBKS.Presentation.WinForms.Features.SetupWizard;

/// <summary>
/// Kalibrasyon Ayarları adımı
/// </summary>
public partial class CalibrationSettingsStep : UserControl, ISetupWizardStep
{
    private readonly SetupState _state;

    public string Title => "Kalibrasyon Ayarları";
    public string Description => "pH ve İletkenlik sensörleri için kalibrasyon referans değerlerini ayarlayın.";
    public int StepNumber => 4;

    public CalibrationSettingsStep(SetupState state)
    {
        _state = state;
        InitializeComponent();
    }

    public Task LoadAsync(CancellationToken ct = default)
    {
        _numPhZeroRef.Value = (decimal)_state.PhZeroRef;
        _numPhSpanRef.Value = (decimal)_state.PhSpanRef;
        _numPhDuration.Value = _state.PhCalibrationDuration;
        _numIletZeroRef.Value = (decimal)_state.IletkenlikZeroRef;
        _numIletSpanRef.Value = (decimal)_state.IletkenlikSpanRef;
        _numIletDuration.Value = _state.IletkenlikCalibrationDuration;
        return Task.CompletedTask;
    }

    public (bool IsValid, string? ErrorMessage) Validate()
    {
        if (_numPhZeroRef.Value == _numPhSpanRef.Value)
            return (false, "pH Zero ve Span değerleri farklı olmalıdır.");

        if (_numIletZeroRef.Value == _numIletSpanRef.Value)
            return (false, "İletkenlik Zero ve Span değerleri farklı olmalıdır.");

        return (true, null);
    }

    public Task<bool> SaveAsync(CancellationToken ct = default)
    {
        _state.PhZeroRef = (double)_numPhZeroRef.Value;
        _state.PhSpanRef = (double)_numPhSpanRef.Value;
        _state.PhCalibrationDuration = (int)_numPhDuration.Value;
        _state.IletkenlikZeroRef = (double)_numIletZeroRef.Value;
        _state.IletkenlikSpanRef = (double)_numIletSpanRef.Value;
        _state.IletkenlikCalibrationDuration = (int)_numIletDuration.Value;
        return Task.FromResult(true);
    }

    public Control GetControl() => this;
}
