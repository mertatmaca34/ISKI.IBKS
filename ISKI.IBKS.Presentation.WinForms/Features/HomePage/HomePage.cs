using ISKI.IBKS.Application.Features.AnalogSensors.Dtos;
using ISKI.IBKS.Application.Features.DigitalSensors.Dtos;
using ISKI.IBKS.Application.Features.DigitalSensors.Enums;
using ISKI.IBKS.Application.Features.HealthSummary.Dtos;
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

    // Digital controls cache
    private readonly Dictionary<string, DigitalSensorControl> _digitalControls = new(StringComparer.OrdinalIgnoreCase);

    // Health cards
    private HealthSummaryCardControl? _plcCard;
    private HealthSummaryCardControl? _apiCard;
    private HealthSummaryCardControl? _phCard;
    private HealthSummaryCardControl? _iletCard;

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

            string format = sensor.Format ?? "N2";
            string instant = sensor.Value.HasValue ? sensor.Value.Value.ToString(format) : "-";
            string hourly = sensor.Value.HasValue ? sensor.Value.Value.ToString(format) : "-";

            control.UpdateValues(
                instantValue: instant ?? "-",
                hourlyAvgValue: hourly ?? "-",
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

    public void RenderDigitalSensors(IReadOnlyList<DigitalReadingDto> digitalReadingDtos)
    {
        if (IsDisposed) return;

        if (InvokeRequired)
        {
            BeginInvoke(new Action(() => RenderDigitalSensors(digitalReadingDtos)));
            return;
        }

        TableLayoutPanelDigitalSensors.SuspendLayout();

        foreach (var sensor in digitalReadingDtos)
        {
            var key = sensor.Key ?? "-";

            var status = sensor.Value == true ? DigitalSignalStatus.Critical
                       : sensor.Value == false ? DigitalSignalStatus.Normal
                       : DigitalSignalStatus.Undefined;

            if (!_digitalControls.TryGetValue(key, out var control))
            {
                control = new DigitalSensorControl()
                {
                    SensorName = sensor.Label ?? key,
                    SensorStatus = status
                };

                _digitalControls[key] = control;
                TableLayoutPanelDigitalSensors.Controls.Add(control);
            }
            else
            {
                control.SensorName = sensor.Label ?? key;
                control.SensorStatus = status;
            }
        }

        TableLayoutPanelDigitalSensors.ResumeLayout();
    }

    public void RenderHealthSummary(HealthSummaryDto dto)
    {
        if (IsDisposed) return;

        if (InvokeRequired)
        {
            BeginInvoke(new Action(() => RenderHealthSummary(dto)));
            return;
        }

        // ensure controls exist and are added
        if (_plcCard is null)
        {
            _plcCard = new HealthSummaryCardControl();
            TableLayoutPanelHealthSummaryCard.Controls.Add(_plcCard);
        }

        if (_apiCard is null)
        {
            _apiCard = new HealthSummaryCardControl();
            TableLayoutPanelHealthSummaryCard.Controls.Add(_apiCard);
        }

        if (_phCard is null)
        {
            _phCard = new HealthSummaryCardControl();
            TableLayoutPanelHealthSummaryCard.Controls.Add(_phCard);
        }

        if (_iletCard is null)
        {
            _iletCard = new HealthSummaryCardControl();
            TableLayoutPanelHealthSummaryCard.Controls.Add(_iletCard);
        }

        // set texts and icons using public API
        _plcCard.Title = "PLC İletişimi:";
        _plcCard.Value = dto.PlcConnected ? "Sağlıklı" : "Problemli";
        _plcCard.StatusImage = dto.PlcConnected ? Properties.Resources.Checkmark_12px : Properties.Resources.cancel;

        _apiCard.Title = "API İletişimi:";
        _apiCard.Value = dto.ApiHealthy ? "Sağlıklı" : "Problemli";
        _apiCard.StatusImage = dto.ApiHealthy ? Properties.Resources.Checkmark_12px : Properties.Resources.cancel;

        _phCard.Title = "Son Kalibrasyon (pH):";
        _phCard.Value = dto.LastPhCalibration.HasValue ? dto.LastPhCalibration.Value.ToString("dd.MM.yyyy") : "-";
        _phCard.StatusImage = dto.LastPhCalibration.HasValue ? Properties.Resources.Checkmark_12px : null;

        _iletCard.Title = "Son Kalibrasyon (İletkenlik):";
        _iletCard.Value = dto.LastIletkenlikCalibration.HasValue ? dto.LastIletkenlikCalibration.Value.ToString("dd.MM.yyyy") : "-";
        _iletCard.StatusImage = dto.LastIletkenlikCalibration.HasValue ? Properties.Resources.Checkmark_12px : null;
    }
}
