using ISKI.IBKS.Presentation.WinForms.Features.SettingsPage.SettingsMain.Presenter;

using Microsoft.Extensions.DependencyInjection;
using ISKI.IBKS.Shared.Localization;

namespace ISKI.IBKS.Presentation.WinForms.Features.SettingsPage.SettingsMain.View;

public partial class SettingsPage : UserControl, ISettingsPageView
{
    private readonly IServiceProvider _serviceProvider;

    public event EventHandler ShowStationSettingsRequested;
    public event EventHandler ShowApiSettingsRequested;
    public event EventHandler ShowPlcSettingsRequested;
    public event EventHandler ShowCalibrationSettingsRequested;
    public event EventHandler ShowMailServerSettingsRequested;

    public SettingsPage(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
        InitializeComponent();
        InitializeLocalization();
        AttachEvents();

        ActivatorUtilities.CreateInstance<SettingsPagePresenter>(serviceProvider, this);

        ShowPlcSettingsRequested?.Invoke(this, EventArgs.Empty);
    }

    private void InitializeLocalization()
    {
        ButtonStationSettings.Text = Strings.Settings_Station;
        ButtonApiSettings.Text = Strings.Settings_Api;
        ButtonPlcSettings.Text = Strings.Settings_Plc;
        ButtonCalibrationSettings.Text = Strings.Settings_Calibration;
        ButtonMailServerSettings.Text = Strings.Settings_Mail;
    }

    private void AttachEvents()
    {
        ButtonStationSettings.Click += (s, e) => ShowStationSettingsRequested?.Invoke(this, EventArgs.Empty);
        ButtonApiSettings.Click += (s, e) => ShowApiSettingsRequested?.Invoke(this, EventArgs.Empty);
        ButtonPlcSettings.Click += (s, e) => ShowPlcSettingsRequested?.Invoke(this, EventArgs.Empty);
        ButtonCalibrationSettings.Click += (s, e) => ShowCalibrationSettingsRequested?.Invoke(this, EventArgs.Empty);
        ButtonMailServerSettings.Click += (s, e) => ShowMailServerSettingsRequested?.Invoke(this, EventArgs.Empty);
    }

    public void SetContent<T>() where T : UserControl
    {
        PanelContent.Controls.Clear();
        var control = _serviceProvider.GetService<T>() ?? ActivatorUtilities.CreateInstance<T>(_serviceProvider);
        control.Dock = DockStyle.Fill;
        PanelContent.Controls.Add(control);
    }
}

