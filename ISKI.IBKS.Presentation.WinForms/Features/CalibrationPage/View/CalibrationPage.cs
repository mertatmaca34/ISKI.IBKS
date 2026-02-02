using System;
using System.Collections.Generic;
using ISKI.IBKS.Shared.Localization;
using Microsoft.Extensions.DependencyInjection;
using ISKI.IBKS.Presentation.WinForms.Features.CalibrationPage.Presenter;

namespace ISKI.IBKS.Presentation.WinForms.Features.CalibrationPage.View;

public partial class CalibrationPage : UserControl, ICalibrationPageView
{
    private readonly List<Control> _controls = new();
    private Guid _stationId = Guid.Empty;

    public event EventHandler<CalibrationEventArgs> CalibrationRequested;

    public CalibrationPage(IServiceProvider serviceProvider)
    {
        InitializeComponent();
        InitializeLocalization();
        ActivatorUtilities.CreateInstance<CalibrationPagePresenter>(serviceProvider, this);
    }

    private void InitializeLocalization()
    {
        // Titles and General UI
        titleBarControl1.TitleBarText = Strings.Nav_Calibration;
        titleBarControl3.TitleBarText = Strings.Cal_SimulationTitle;
        TitleBarControlActiveCalibration.TitleBarText = Strings.Nav_Calibration; // or specific title
        TitleBarControlTimeRemain.TitleBarText = string.Format(Strings.Cal_TimeRemaining, "-");

        // KOI
        if (Controls.Find("label21", true).FirstOrDefault() is Label lblKoiDesc) lblKoiDesc.Text = Strings.Sensor_Koi;
        if (Controls.Find("label22", true).FirstOrDefault() is Label lblKoiName) lblKoiName.Text = "KOİ";
        ButtonKoiZero.Text = Strings.Cal_ZeroButton;

        // Iletkenlik
        if (Controls.Find("label17", true).FirstOrDefault() is Label lblIletDesc) lblIletDesc.Text = Strings.Sensor_Iletkenlik;
        if (Controls.Find("label18", true).FirstOrDefault() is Label lblIletName) lblIletName.Text = Strings.Sensor_Iletkenlik;
        ButtonIletkenlikZero.Text = Strings.Cal_ZeroButton;
        ButtonIletkenlikSpan.Text = Strings.Cal_SpanButton;

        // pH
        if (Controls.Find("label13", true).FirstOrDefault() is Label lblPhDesc) lblPhDesc.Text = "Potansiyel Hidrojen";
        if (Controls.Find("label14", true).FirstOrDefault() is Label lblPhName) lblPhName.Text = Strings.Sensor_Ph;
        ButtonPhZero.Text = Strings.Cal_ZeroButton;
        ButtonPhSpan.Text = Strings.Cal_SpanButton;

        // AKM
        if (Controls.Find("label1", true).FirstOrDefault() is Label lblAkmDesc) lblAkmDesc.Text = Strings.Sensor_Akm;
        if (Controls.Find("label2", true).FirstOrDefault() is Label lblAkmName) lblAkmName.Text = "AKM";
        ButtonAkmZero.Text = Strings.Cal_ZeroButton;

        // Chart
        if (ChartCalibration.Titles.Count > 0) ChartCalibration.Titles[0].Text = Strings.Cal_ChartTitle;
        if (ChartCalibration.Series.Count > 0) ChartCalibration.Series[0].LegendText = Strings.Cal_Value;
        if (ChartCalibration.Series.Count > 1) ChartCalibration.Series[1].LegendText = Strings.Cal_Reference;

        // Last Calibration Labels
        if (Controls.Find("label23", true).FirstOrDefault() is Label lblKoiLast) lblKoiLast.Text = "Son Kalibrasyon Tarihi";
        if (Controls.Find("label19", true).FirstOrDefault() is Label lblIletLast) lblIletLast.Text = "Son Kalibrasyon Tarihi";
        if (Controls.Find("label15", true).FirstOrDefault() is Label lblPhLast) lblPhLast.Text = "Son Kalibrasyon Tarihi";
        if (Controls.Find("label3", true).FirstOrDefault() is Label lblAkmLast) lblAkmLast.Text = "Son Kalibrasyon Tarihi";
    }

    public void SetStationId(Guid stationId)
    {
        _stationId = stationId;
    }

    public void UpdateCalibrationStatus(string channel, string step, int remainingSeconds)
    {
        // UI Update logic (Passive)
    }

    public void ShowError(string message)
    {
        if (InvokeRequired)
        {
            BeginInvoke(new Action(() => ShowError(message)));
            return;
        }
        MessageBox.Show(message, Strings.Common_Error, MessageBoxButtons.OK, MessageBoxIcon.Error);
    }

    public List<Control> GetCalibrationControls()
    {
        if (_controls.Count == 0)
        {
            _controls.Add(CalibrationStatusBarZero);
            _controls.Add(CalibrationStatusBarSpan);
            _controls.Add(ChartCalibration);
            _controls.Add(TitleBarControlActiveCalibration);
            _controls.Add(TitleBarControlTimeRemain);
        }
        return _controls;
    }

    private void ButtonAkmZero_Click(object sender, EventArgs e) => 
        CalibrationRequested?.Invoke(this, new CalibrationEventArgs { Channel = "AKM", Step = "Zero", DurationSeconds = 60 });

    private void ButtonKoiZero_Click(object sender, EventArgs e) => 
        CalibrationRequested?.Invoke(this, new CalibrationEventArgs { Channel = "KOi", Step = "Zero", DurationSeconds = 60 });

    private void ButtonPhZero_Click(object sender, EventArgs e) => 
        CalibrationRequested?.Invoke(this, new CalibrationEventArgs { Channel = "pH", Step = "Zero", DurationSeconds = 60 });

    private void ButtonPhSpan_Click(object sender, EventArgs e) => 
        CalibrationRequested?.Invoke(this, new CalibrationEventArgs { Channel = "pH", Step = "Span", DurationSeconds = 60 });

    private void ButtonIletkenlikZero_Click(object sender, EventArgs e) => 
        CalibrationRequested?.Invoke(this, new CalibrationEventArgs { Channel = "Iletkenlik", Step = "Zero", DurationSeconds = 60 });

    private void ButtonIletkenlikSpan_Click(object sender, EventArgs e) => 
        CalibrationRequested?.Invoke(this, new CalibrationEventArgs { Channel = "Iletkenlik", Step = "Span", DurationSeconds = 60 });
}
