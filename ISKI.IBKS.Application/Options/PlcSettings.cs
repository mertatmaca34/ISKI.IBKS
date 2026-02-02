using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ISKI.IBKS.Application.Common.IoT.Plc;

public class PlcSettings
{
    public List<PlcStationConfig> Stations { get; set; } = new();
    public PlcStationConfig Station { get; set; } = new();
}

