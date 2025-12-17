using ISKI.IBKS.Application.Features.AnalogSensors.Dtos;
using ISKI.IBKS.Application.Features.StationStatus.Dtos;
using ISKI.IBKS.Presentation.WinForms.Extensions;
using ISKI.IBKS.Presentation.WinForms.Features.HomePage.Controls;
using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace ISKI.IBKS.Presentation.WinForms.Features.HomePage;

public partial class HomePage : UserControl, IHomePageView
{
    // ChannelName -> Control cache
    private readonly Dictionary<string, AnalogSensorControl> _controlsByChannel =
        new(StringComparer.OrdinalIgnoreCase);

    public HomePage()
    {
        InitializeComponent();
    }

    public void RenderAnalogChannels(IReadOnlyList<ChannelReadingDto> channelReadingDtos)
    {
        if (IsDisposed) return;

        if (InvokeRequired)
        {
            BeginInvoke(new Action(() => RenderAnalogChannels(channelReadingDtos)));
            return;
        }

        TableLayoutPanelAnalogSensors.SuspendLayout();

        foreach (var sensor in channelReadingDtos)
        {
            var channelName = sensor.ChannelName ?? "-";

            if (!_controlsByChannel.TryGetValue(channelName, out var control))
            {
                control = new AnalogSensorControl(
                    sensorName: channelName,
                    sensorInstantValue: "-",
                    sensorHourlyAvgValue: "-",
                    analogSensorUnit: sensor.UnitName ?? "-",
                    analogSignalStatus: sensor.Status ?? Application.Features.AnalogSensors.Enums.AnalogSignalStatus.Undefined


                );

                _controlsByChannel[channelName] = control;
                TableLayoutPanelAnalogSensors.Controls.Add(control);
            }

            // AnalogSensorControl içine bunu ekleyeceğiz (aşağıda)
            control.UpdateValues(
                instantValue: sensor.Value.ToUiValue(2).ToString() ?? "-",
                hourlyAvgValue: sensor.Value.ToUiValue(2).ToString() ?? "-",
                unit: sensor.UnitName ?? "-",
                analogSignalStatus: sensor.Status ?? Application.Features.AnalogSensors.Enums.AnalogSignalStatus.Undefined
            );
        }

        TableLayoutPanelAnalogSensors.ResumeLayout();
    }

    public void RenderStationStatusBar(StationStatusDto? stationStatusDto)
    {
        stationStatusBar1.IsConnected = stationStatusDto?.IsConnected ?? false;
        stationStatusBar1.UpTime = stationStatusDto?.UpTime ?? new TimeSpan(0,0,0);
        stationStatusBar1.WeeklyWashRemainingTime = stationStatusDto?.WeeklyWashRemainingTime ?? TimeSpan.Zero;
        stationStatusBar1.DailyWashRemainingTime = stationStatusDto?.DailyWashRemainingTime ?? TimeSpan.Zero;
        stationStatusBar1.SystemTime = stationStatusDto?.SystemTime ?? DateTime.MinValue;
    }
}
