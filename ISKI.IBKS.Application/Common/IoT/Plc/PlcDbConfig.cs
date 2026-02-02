namespace ISKI.IBKS.Application.Common.IoT.Plc;

public class PlcDbConfig
{
    public int DbNumber { get; set; }
    public int Size { get; set; }
    public string Type { get; set; } = default!;
    public Dictionary<string, PlcOffsetConfig> Offsets { get; set; } = new();
}

