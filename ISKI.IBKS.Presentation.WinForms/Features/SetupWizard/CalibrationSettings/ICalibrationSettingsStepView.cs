using System;

namespace ISKI.IBKS.Presentation.WinForms.Features.SetupWizard.CalibrationSettings;

public interface ICalibrationSettingsStepView
{
    double PhZeroRef { get; set; }
    double PhSpanRef { get; set; }
    int PhDuration { get; set; }
    double CondZeroRef { get; set; }
    double CondSpanRef { get; set; }
    int CondDuration { get; set; }
}
