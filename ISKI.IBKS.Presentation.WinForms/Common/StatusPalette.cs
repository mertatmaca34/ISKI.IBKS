using ISKI.IBKS.Application.Features.AnalogSensors.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ISKI.IBKS.Presentation.WinForms.Common;

public static class StatusPalette
{
    private readonly static Color SimGreen = Color.FromArgb(19, 162, 97);
    private readonly static Color SimRed = Color.FromArgb(235, 85, 101);

    public static Color Get(AnalogSignalStatus status) => status switch
    {
        AnalogSignalStatus.Normal => SimGreen,
        AnalogSignalStatus.Critical => SimRed,
        AnalogSignalStatus.Undefined => Color.Gray,
        _ => Color.Gray
    };
}
