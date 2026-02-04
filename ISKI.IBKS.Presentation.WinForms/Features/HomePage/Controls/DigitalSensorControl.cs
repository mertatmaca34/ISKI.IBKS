using ISKI.IBKS.Application.Features.DigitalSensors.Enums;
using ISKI.IBKS.Presentation.WinForms.Common.Ui;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ISKI.IBKS.Presentation.WinForms.Features.HomePage.Controls;

public partial class DigitalSensorControl : UserControl
{
    private DigitalSignalStatus _sensorStatus;

    public Color StatusIndicator 
    {
        get => PanelStatusIndicator.BackColor; 
        set => PanelStatusIndicator.BackColor = value;
    }

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
