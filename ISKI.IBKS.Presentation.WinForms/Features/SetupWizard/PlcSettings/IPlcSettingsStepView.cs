using System;
using System.Collections.Generic;

namespace ISKI.IBKS.Presentation.WinForms.Features.SetupWizard.PlcSettings;

public interface IPlcSettingsStepView
{
    event EventHandler TestConnectionRequested;

    string IpAddress { get; set; }
    decimal Rack { get; set; }
    decimal Slot { get; set; }
    List<string> AvailableSensors { set; }
    List<string> SelectedSensors { get; set; }

    void SetTestButtonState(bool enabled, string text);
    void ShowMessage(string message, string title, bool isError);
}
