using ISKI.IBKS.Presentation.WinForms.Features.SettingsPage.CalibrationSettings.Presenter;
using ISKI.IBKS.Shared.Localization;
using Microsoft.Extensions.DependencyInjection;

namespace ISKI.IBKS.Presentation.WinForms.Features.SettingsPage.CalibrationSettings.View;

public partial class CalibrationSettingsPage : UserControl, ICalibrationSettingsPageView
{
    public event EventHandler SaveRequested;

    public string AkmZeroRef { get => CalibrationSettingsBarAkm.ZeroRef; set => CalibrationSettingsBarAkm.ZeroRef = value; }
    public string AkmZeroTime { get => CalibrationSettingsBarAkm.ZeroTime; set => CalibrationSettingsBarAkm.ZeroTime = value; }
    public string KoiZeroRef { get => CalibrationSettingsBarKoi.ZeroRef; set => CalibrationSettingsBarKoi.ZeroRef = value; }
    public string KoiZeroTime { get => CalibrationSettingsBarKoi.ZeroTime; set => CalibrationSettingsBarKoi.ZeroTime = value; }
    public string PhZeroRef { get => CalibrationSettingsBarPh.ZeroRef; set => CalibrationSettingsBarPh.ZeroRef = value; }
    public string PhZeroTime { get => CalibrationSettingsBarPh.ZeroTime; set => CalibrationSettingsBarPh.ZeroTime = value; }
    public string PhSpanRef { get => CalibrationSettingsBarPh.SpanRef; set => CalibrationSettingsBarPh.SpanRef = value; }
    public string PhSpanTime { get => CalibrationSettingsBarPh.SpanTime; set => CalibrationSettingsBarPh.SpanTime = value; }
    public string IletkenlikZeroRef { get => CalibrationSettingsBarIletkenlik.ZeroRef; set => CalibrationSettingsBarIletkenlik.ZeroRef = value; }
    public string IletkenlikZeroTime { get => CalibrationSettingsBarIletkenlik.ZeroTime; set => CalibrationSettingsBarIletkenlik.ZeroTime = value; }
    public string IletkenlikSpanRef { get => CalibrationSettingsBarIletkenlik.SpanRef; set => CalibrationSettingsBarIletkenlik.SpanRef = value; }
    public string IletkenlikSpanTime { get => CalibrationSettingsBarIletkenlik.SpanTime; set => CalibrationSettingsBarIletkenlik.SpanTime = value; }

    public CalibrationSettingsPage(IServiceProvider serviceProvider)
    {
        InitializeComponent();
        InitializeLocalization();
        ButtonSave.Click += (s, e) => SaveRequested?.Invoke(this, EventArgs.Empty);

        ActivatorUtilities.CreateInstance<CalibrationSettingsPresenter>(serviceProvider, this);
    }

    private void InitializeLocalization()
    {
        titleBarControl1.TitleBarText = Strings.Settings_Calibration;
        CalibrationSettingsBarAkm.Parameter = "AKM";
        CalibrationSettingsBarKoi.Parameter = "KOİ";
        CalibrationSettingsBarPh.Parameter = "pH";
        CalibrationSettingsBarIletkenlik.Parameter = Strings.Sensor_Iletkenlik;
        ButtonSave.Text = Strings.Common_Save;
    }

    public void ShowInfo(string message) => MessageBox.Show(message, Strings.Common_Information, MessageBoxButtons.OK, MessageBoxIcon.Information);
    public void ShowError(string message) => MessageBox.Show(message, Strings.Common_Error, MessageBoxButtons.OK, MessageBoxIcon.Error);
}
