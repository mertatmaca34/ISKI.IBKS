namespace ISKI.IBKS.Application.Common.IoT.Plc;

public sealed class UiMappingStation
{
    public List<UiMappingEntryRaw> Analog { get; init; } = new();
    public List<UiMappingEntryRaw> Digital { get; init; } = new();
}

