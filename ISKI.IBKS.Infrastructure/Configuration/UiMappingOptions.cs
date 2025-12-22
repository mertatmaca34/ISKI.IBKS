using System.Collections.Generic;

namespace ISKI.IBKS.Infrastructure.Configuration;

public sealed class UiMappingOptions
{
    public UiMappingStation Station { get; init; } = new UiMappingStation();
}

public sealed class UiMappingStation
{
    public List<UiMappingEntryRaw> Analog { get; init; } = new();
    public List<UiMappingEntryRaw> Digital { get; init; } = new();
}

public sealed class UiMappingEntryRaw
{
    public string Key { get; init; } = string.Empty; // tag name in StationSnapshot
    public string Label { get; init; } = string.Empty; // text shown in UI
    public string Unit { get; init; } = string.Empty; // unit to show (for analog)
    public string Format { get; init; } = "N2"; // numeric format string
    public int? Order { get; init; }
}
