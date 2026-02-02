using System;

namespace ISKI.IBKS.Presentation.WinForms.Features.SettingsPage.CalibrationSettings.View;

public interface ICalibrationSettingsPageView
{
    event EventHandler Load;
    event EventHandler SaveRequested;

    string AkmZeroRef { get; set; }
    string AkmZeroTime { get; set; }
    string KoiZeroRef { get; set; }
    string KoiZeroTime { get; set; }
    string PhZeroRef { get; set; }
    string PhZeroTime { get; set; }
    string PhSpanRef { get; set; }
    string PhSpanTime { get; set; }
    string IletkenlikZeroRef { get; set; }
    string IletkenlikZeroTime { get; set; }
    string IletkenlikSpanRef { get; set; }
    string IletkenlikSpanTime { get; set; }

    void ShowInfo(string message);
    void ShowError(string message);
}
