using ISKI.IBKS.Domain.Enums;
using System.Drawing;

namespace ISKI.IBKS.Presentation.WinForms.Common.Ui;

public static class StatusPalette
{
    public static Color Get(AnalogSignalStatus status) => status switch
    {
        AnalogSignalStatus.Normal => Color.FromArgb(76, 175, 80),
        AnalogSignalStatus.High => Color.FromArgb(244, 67, 54),
        AnalogSignalStatus.Low => Color.FromArgb(255, 152, 0),
        _ => Color.Gray
    };

    public static Color Get(DigitalSignalStatus status) => status switch
    {
        DigitalSignalStatus.Active => Color.FromArgb(76, 175, 80),
        DigitalSignalStatus.Passive => Color.Gray,
        _ => Color.DarkGray
    };
}
