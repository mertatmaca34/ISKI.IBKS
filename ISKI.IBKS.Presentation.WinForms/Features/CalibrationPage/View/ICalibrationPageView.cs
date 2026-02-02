using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace ISKI.IBKS.Presentation.WinForms.Features.CalibrationPage.View;

public interface ICalibrationPageView
{
    event EventHandler Load;
    event EventHandler<CalibrationEventArgs> CalibrationRequested;

    void SetStationId(Guid stationId);
    void UpdateCalibrationStatus(string channel, string step, int remainingSeconds);
    void ShowError(string message);
    List<Control> GetCalibrationControls();
}

public class CalibrationEventArgs : EventArgs
{
    public string Channel { get; set; }
    public string Step { get; set; }
    public int DurationSeconds { get; set; }
}
