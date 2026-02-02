using ISKI.IBKS.Application.Common.IoT.Snapshots;
using ISKI.IBKS.Domain.Enums;
using ISKI.IBKS.Presentation.WinForms.Features.HomePage.Controls;
using ISKI.IBKS.Presentation.WinForms.Features.HomePage.Presenter;
using ISKI.IBKS.Shared.Localization;
using Microsoft.Extensions.DependencyInjection;

namespace ISKI.IBKS.Presentation.WinForms.Features.HomePage.View;

public partial class HomePage : UserControl, IHomePageView
{
    private readonly Dictionary<string, AnalogSensorControl> _controlsByChannel = new(StringComparer.OrdinalIgnoreCase);
    private readonly Dictionary<string, DigitalSensorControl> _digitalControls = new(StringComparer.OrdinalIgnoreCase);

    public HomePage(IServiceProvider serviceProvider)
    {
        InitializeComponent();
        InitializeLocalization();
        ActivatorUtilities.CreateInstance<HomePagePresenter>(serviceProvider, this);
    }

    private void InitializeLocalization()
    {
        AnalogSensorsHeaderControl.HeaderTitle = Strings.Sensor_Analog;
        AnalogSensorsHeaderControl.HeaderTitle2 = Strings.Data_Instant;
        AnalogSensorsHeaderControl.HeaderTitle3 = Strings.Data_Hourly;

        DigitalSensorsHeaderControl.HeaderTitle = Strings.Sensor_Digital;
        DigitalSensorsHeaderControl.HeaderTitle2 = "-"; // Status
        DigitalSensorsHeaderControl.HeaderTitle3 = "-";
    }

    public void UpdateAnalogReadings(IEnumerable<ChannelReadingDto> readings)
    {
        if (InvokeRequired)
        {
            BeginInvoke(new Action(() => UpdateAnalogReadings(readings)));
            return;
        }

        foreach (var reading in readings)
        {
            if (_controlsByChannel.TryGetValue(reading.ChannelName, out var control))
            {
                control.UpdateValues(reading.Value.ToString(), "0", reading.Unit, AnalogSignalStatus.Normal);
            }
        }
    }

    public void UpdateStationStatus(StationStatusDto status)
    {
        if (InvokeRequired)
        {
            BeginInvoke(new Action(() => UpdateStationStatus(status)));
            return;
        }
    }

    public void UpdateDigitalReadings(IEnumerable<DigitalReadingDto> readings)
    {
        if (InvokeRequired)
        {
            BeginInvoke(new Action(() => UpdateDigitalReadings(readings)));
            return;
        }
    }

    public void UpdateHealthSummary(HealthSummaryDto summary)
    {
        if (InvokeRequired)
        {
            BeginInvoke(new Action(() => UpdateHealthSummary(summary)));
            return;
        }
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
}
