namespace ISKI.IBKS.Application.Common.Configuration;

public record CalibrationSettings
{
    public int PhZeroDuration { get; init; } = 60;
    public double PhZeroReference { get; init; } = 7.0;
    public int PhSpanDuration { get; init; } = 60;
    public double PhSpanReference { get; init; } = 4.0;

    public int ConductivityZeroDuration { get; init; } = 60;
    public double ConductivityZeroReference { get; init; } = 0;
    public int ConductivitySpanDuration { get; init; } = 60;
    public double ConductivitySpanReference { get; init; } = 1413;

    public int AkmZeroDuration { get; init; } = 60;
    public double AkmZeroReference { get; init; } = 0;

    public int KoiZeroDuration { get; init; } = 60;
    public double KoiZeroReference { get; init; } = 0;
}

