namespace ISKI.IBKS.Application.Common.IoT.Plc;

public class PlcOffsetConfig
{
    public string DataType { get; set; } = default!;
    public int ByteOffset { get; set; }
    public int? BitOffset { get; set; }
}

