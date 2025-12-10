using ISKI.IBKS.Domain.Entities;
using ISKI.IBKS.Presentation.WinForms.Features.HomePage.Controls;
using ISKI.IBKS.Presentation.WinForms.Features.HomePage.ViewModels;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ISKI.IBKS.Presentation.WinForms.Features.HomePage
{
    public partial class HomePage : UserControl, IHomePageView
    {
        public HomePage()
        {
            InitializeComponent();
        }

        public void BindAnalogSensors(StationSnapshot stationSnapshot)
        {
            throw new NotImplementedException();
        }

        public void RenderAnalogSensors(IReadOnlyList<AnalogSensorViewModel> analogSensorList)
        {
            foreach (var analogSensor in analogSensorList)
            {
                TableLayoutPanelAnalogSensors.Controls.Add(new AnalogSensorControl(
                    sensorName: analogSensor.Name, 
                    sensorInstantValue: analogSensor.InstantValue,
                    sensorHourlyAvgValue: analogSensor.HourlyAverageValue, 
                    analogSensorUnit: analogSensor.Unit));
            }
        }
    }
}
