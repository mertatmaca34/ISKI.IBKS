namespace ISKI.IBKS.Presentation.WinForms.Features.SetupWizard;

/// <summary>
/// SAIS API Ayarları adımı
/// </summary>
public partial class SaisApiSettingsStep : UserControl, ISetupWizardStep
{
    private readonly SetupState _state;

    public string Title => "SAIS API Ayarları";
    public string Description => "SAIS API bağlantı bilgilerini yapılandırın.";
    public int StepNumber => 2;

    public SaisApiSettingsStep(SetupState state)
    {
        _state = state;
        InitializeComponent();
    }

    public Task LoadAsync(CancellationToken ct = default)
    {
        _txtApiUrl.Text = _state.SaisApiUrl;
        _txtUserName.Text = _state.SaisUserName;
        _txtPassword.Text = _state.SaisPassword;
        return Task.CompletedTask;
    }

    public (bool IsValid, string? ErrorMessage) Validate()
    {
        if (string.IsNullOrWhiteSpace(_txtApiUrl.Text))
            return (false, "SAIS API adresi boş olamaz.");

        if (!Uri.TryCreate(_txtApiUrl.Text, UriKind.Absolute, out _))
            return (false, "Geçerli bir URL adresi giriniz.");

        if (string.IsNullOrWhiteSpace(_txtUserName.Text))
            return (false, "Kullanıcı adı boş olamaz.");

        if (string.IsNullOrWhiteSpace(_txtPassword.Text))
            return (false, "Şifre boş olamaz.");

        return (true, null);
    }

    public Task<bool> SaveAsync(CancellationToken ct = default)
    {
        _state.SaisApiUrl = _txtApiUrl.Text.Trim();
        _state.SaisUserName = _txtUserName.Text.Trim();
        _state.SaisPassword = _txtPassword.Text;
        return Task.FromResult(true);
    }

    public Control GetControl() => this;
}
