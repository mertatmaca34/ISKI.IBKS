using ISKI.IBKS.Application.Common.IoT.Snapshots;
using System;
using System.Collections.Generic;

namespace ISKI.IBKS.Presentation.WinForms.Features.HomePage.View;

public interface IHomePageView
{
    event EventHandler Load;
    event EventHandler Disposed;

    void UpdateAnalogReadings(IEnumerable<ChannelReadingDto> readings);
    void UpdateStationStatus(StationStatusDto status);
    void UpdateDigitalReadings(IEnumerable<DigitalReadingDto> readings);
    void UpdateHealthSummary(HealthSummaryDto summary);
    void ShowError(string message);
}
