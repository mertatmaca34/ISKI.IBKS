using System;
using System.Collections.Generic;

namespace ISKI.IBKS.Application.Common.IoT.Plc;

public class PlcStationConfig
{
    public Guid StationId { get; set; }
    public string IpAddress { get; set; } = default!;
    public int Rack { get; set; }
    public int Slot { get; set; }
    public List<PlcDbConfig> DBs { get; set; } = new();
}
