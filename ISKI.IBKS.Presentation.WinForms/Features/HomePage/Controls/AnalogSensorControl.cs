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

        public AnalogSensorControl(string sensorName, string sensorInstantValue, string sensorHourlyAvgValue, string analogSensorUnit)
        {
            InitializeComponent();

            AnalogSensorUnit = analogSensorUnit;
            SensorName = sensorName;
            SensorInstantValue = sensorInstantValue;
            SensorHourlyAvgValue = sensorHourlyAvgValue;
        }

        public void UpdateValues(string instantValue, string hourlyAvgValue, string unit)
        {
            SensorInstantValue = instantValue;
            SensorHourlyAvgValue = hourlyAvgValue;
            AnalogSensorUnit = unit;
        }
    }
}
