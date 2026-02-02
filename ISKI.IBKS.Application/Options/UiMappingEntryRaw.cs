namespace ISKI.IBKS.Application.Common.IoT.Plc;

public sealed class UiMappingEntryRaw
{
    public string Key { get; init; } = string.Empty;
    public string Label { get; init; } = string.Empty;
    public string Unit { get; init; } = string.Empty;
    public string Format { get; init; } = "N2";
    public int? Order { get; init; }
}

