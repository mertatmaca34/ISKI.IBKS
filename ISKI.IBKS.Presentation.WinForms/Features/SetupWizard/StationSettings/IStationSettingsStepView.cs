using System;

namespace ISKI.IBKS.Presentation.WinForms.Features.SetupWizard.StationSettings;

public interface IStationSettingsStepView
{
    string StationId { get; set; }
    string StationName { get; set; }
    string LocalApiHost { get; set; }
}
