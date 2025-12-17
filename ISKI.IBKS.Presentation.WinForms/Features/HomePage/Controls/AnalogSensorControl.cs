using ISKI.IBKS.Application.Features.AnalogSensors.Enums;
using ISKI.IBKS.Infrastructure.Features.AnalogSensors;
using ISKI.IBKS.Presentation.WinForms.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ISKI.IBKS.Presentation.WinForms.Features.HomePage.Controls
{
    public partial class AnalogSensorControl : UserControl
    {
        private AnalogSignalStatus _sensorStatus;

        public string AnalogSensorUnit { get; set; }

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

        public AnalogSensorControl(string sensorName, string sensorInstantValue, string sensorHourlyAvgValue, string analogSensorUnit, AnalogSignalStatus analogSignalStatus)
        {
            InitializeComponent();

            AnalogSensorUnit = analogSensorUnit;
            SensorName = sensorName;
            SensorInstantValue = sensorInstantValue;
            SensorHourlyAvgValue = sensorHourlyAvgValue;
            SensorStatus = analogSignalStatus;
        }

        public void UpdateValues(string instantValue, string hourlyAvgValue, string unit, AnalogSignalStatus analogSignalStatus)
        {
            SensorInstantValue = instantValue;
            SensorHourlyAvgValue = hourlyAvgValue;
            AnalogSensorUnit = unit;
            SensorStatus = analogSignalStatus;
        }
    }
}
