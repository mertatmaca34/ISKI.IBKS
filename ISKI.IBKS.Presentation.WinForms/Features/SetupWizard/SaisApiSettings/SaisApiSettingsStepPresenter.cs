using System;

using ISKI.IBKS.Presentation.WinForms.Features.SetupWizard.Model;

namespace ISKI.IBKS.Presentation.WinForms.Features.SetupWizard.SaisApiSettings;

public sealed class SaisApiSettingsStepPresenter
{
    private readonly ISaisApiSettingsStepView _view;
    private readonly SetupState _state;

    public SaisApiSettingsStepPresenter(ISaisApiSettingsStepView view, SetupState state)
    {
        _view = view;
        _state = state;
        LoadData();
    }

    private void LoadData()
    {
        _view.ApiUrl = _state.SaisApiUrl;
        _view.UserName = _state.SaisUserName;
        _view.Password = _state.SaisPassword;
    }

    public void SaveData()
    {
        _state.SaisApiUrl = _view.ApiUrl.Trim();
        _state.SaisUserName = _view.UserName.Trim();
        _state.SaisPassword = _view.Password;
    }

    public (bool IsValid, string? ErrorMessage) Validate()
    {
        if (string.IsNullOrWhiteSpace(_view.ApiUrl)) return (false, "API URL boş olamaz.");
        if (!Uri.TryCreate(_view.ApiUrl, UriKind.Absolute, out _)) return (false, "Geçersiz API URL.");
        if (string.IsNullOrWhiteSpace(_view.UserName)) return (false, "Kullanıcı adı boş olamaz.");
        if (string.IsNullOrWhiteSpace(_view.Password)) return (false, "Şifre boş olamaz.");
        return (true, null);
    }
}
