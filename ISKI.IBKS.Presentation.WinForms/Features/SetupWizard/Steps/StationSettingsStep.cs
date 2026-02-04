namespace ISKI.IBKS.Presentation.WinForms.Features.SetupWizard;

/// <summary>
/// İstasyon Ayarları adımı
/// </summary>
public partial class StationSettingsStep : UserControl, ISetupWizardStep
{
    private readonly SetupState _state;

    public string Title => "İstasyon Ayarları";
    public string Description => "İstasyon ve local API bilgilerini yapılandırın.";
    public int StepNumber => 3;

    public StationSettingsStep(SetupState state)
    {
        _state = state;
        InitializeComponent();
    }

    public Task LoadAsync(CancellationToken ct = default)
    {
        _txtStationId.Text = _state.StationId == Guid.Empty ? "" : _state.StationId.ToString();
        _txtStationName.Text = _state.StationName;
        _txtLocalApiHost.Text = _state.LocalApiHost;
        _txtLocalApiPort.Text = _state.LocalApiPort;
        _txtLocalApiUser.Text = _state.LocalApiUserName;
        _txtLocalApiPassword.Text = _state.LocalApiPassword;
        return Task.CompletedTask;
    }

    public (bool IsValid, string? ErrorMessage) Validate()
    {
        if (string.IsNullOrWhiteSpace(_txtStationId.Text))
            return (false, "İstasyon ID boş olamaz.");

        if (!Guid.TryParse(_txtStationId.Text, out _))
            return (false, "Geçerli bir GUID formatı giriniz.");

        if (string.IsNullOrWhiteSpace(_txtStationName.Text))
            return (false, "İstasyon adı boş olamaz.");

        if (string.IsNullOrWhiteSpace(_txtLocalApiHost.Text))
            return (false, "Local API adresi boş olamaz.");

        if (!int.TryParse(_txtLocalApiPort.Text, out var port) || port < 1 || port > 65535)
            return (false, "Geçerli bir port numarası giriniz (1-65535).");

        return (true, null);
    }

    public Task<bool> SaveAsync(CancellationToken ct = default)
    {
        _state.StationId = Guid.Parse(_txtStationId.Text.Trim());
        _state.StationName = _txtStationName.Text.Trim();
        _state.LocalApiHost = _txtLocalApiHost.Text.Trim();
        _state.LocalApiPort = _txtLocalApiPort.Text.Trim();
        _state.LocalApiUserName = _txtLocalApiUser.Text.Trim();
        _state.LocalApiPassword = _txtLocalApiPassword.Text;
        return Task.FromResult(true);
    }

    public Control GetControl() => this;
}
