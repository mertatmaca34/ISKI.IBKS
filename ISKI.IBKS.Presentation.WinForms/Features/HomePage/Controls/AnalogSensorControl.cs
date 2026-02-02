using ISKI.IBKS.Domain.Enums;
using ISKI.IBKS.Presentation.WinForms.Common.Ui;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace ISKI.IBKS.Presentation.WinForms.Features.HomePage.Controls;

public partial class AnalogSensorControl : UserControl
{
    private AnalogSignalStatus _sensorStatus;
    public string AnalogSensorUnit { get; set; } = string.Empty;

    public string SensorName
    {
        get => LabelSensorName.Text;
        set => LabelSensorName.Text = $"{value} {AnalogSensorUnit}";
    }

    public string SensorInstantValue
    {
        get => LabelSensorInstantValue.Text;
        set => LabelSensorInstantValue.Text = $"{value} {AnalogSensorUnit}";
    }

    public string SensorHourlyAvgValue
    {
        get => LabelSensorHourlyAvgValue.Text;
        set => LabelSensorHourlyAvgValue.Text = $"{value} {AnalogSensorUnit}";
    }

    public AnalogSignalStatus SensorStatus
    {
        get => _sensorStatus;
        set
        {
            _sensorStatus = value;
            PanelStatusIndicator.BackColor = StatusPalette.Get(value);
        }
    }

    public AnalogSensorControl()
    {
        InitializeComponent();
    }

    public void UpdateValues(string instantValue, string hourlyAvgValue, string unit, AnalogSignalStatus status)
    {
        AnalogSensorUnit = unit;
        SensorInstantValue = instantValue;
        SensorHourlyAvgValue = hourlyAvgValue;
        SensorStatus = status;
    }
}
