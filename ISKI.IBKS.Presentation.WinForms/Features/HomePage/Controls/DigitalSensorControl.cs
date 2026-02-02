using ISKI.IBKS.Domain.Enums;
using ISKI.IBKS.Presentation.WinForms.Common.Ui;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace ISKI.IBKS.Presentation.WinForms.Features.HomePage.Controls;

public partial class DigitalSensorControl : UserControl
{
    private DigitalSignalStatus _sensorStatus;

    public DigitalSignalStatus SensorStatus
    {
        get => _sensorStatus;
        set
        {
            _sensorStatus = value;
            PanelStatusIndicator.BackColor = StatusPalette.Get(value);
        }
    }

    public string SensorName
    {
        get => LabelSensorName.Text;
        set => LabelSensorName.Text = value;
    }

    public DigitalSensorControl()
    {
        InitializeComponent();
    }
}
