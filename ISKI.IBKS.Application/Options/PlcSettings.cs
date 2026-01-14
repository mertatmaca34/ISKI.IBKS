using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ISKI.IBKS.Infrastructure.IoT.Plc.Configuration;

public class PlcSettings
{
    public List<PlcStationConfig> Stations { get; set; } = new();
    public PlcStationConfig Station { get; set; } = new();
}

public class PlcStationConfig
{
    public Guid StationId { get; set; } = default!;
    public string IpAddress { get; set; } = default!;
    public int Rack { get; set; }
    public int Slot { get; set; }
    public List<PlcDbConfig> DBs { get; set; } = new();
}

public class PlcDbConfig
{
    public int DbNumber { get; set; }
    public int Size { get; set; }
    public string Type { get; set; } = default!; // "Analog" / "Digital" / "Time"
    public Dictionary<string, PlcOffsetConfig> Offsets { get; set; } = new();
}

public class PlcOffsetConfig
{
    public string DataType { get; set; } = default!; // "Real", "Bool", "DateTime", "Byte"
    public int ByteOffset { get; set; }
    public int? BitOffset { get; set; } // sadece Bool için dolu
}
