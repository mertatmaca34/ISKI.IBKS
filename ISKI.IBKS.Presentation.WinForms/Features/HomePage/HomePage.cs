using ISKI.IBKS.Application.Features.AnalogSensors.Dtos;
using ISKI.IBKS.Domain.Entities;
using ISKI.IBKS.Presentation.WinForms.Extensions;
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

        public void RenderAnalogChannels(IReadOnlyList<ChannelReadingDto> channelReadingDtos)
        {
            TableLayoutPanelAnalogSensors.SuspendLayout();
            TableLayoutPanelAnalogSensors.Controls.Clear();

            foreach (var analogSensor in channelReadingDtos)
            {
                TableLayoutPanelAnalogSensors.Controls.Add(new AnalogSensorControl(
                    sensorName: analogSensor.ChannelName ?? "-",
                    sensorInstantValue: analogSensor.Value.ToUiValue(2),
                    sensorHourlyAvgValue: analogSensor.Value.ToUiValue(2),
                    analogSensorUnit: analogSensor.UnitName ?? "-"
                ));
            }

            TableLayoutPanelAnalogSensors.ResumeLayout();
        }
    }
}
