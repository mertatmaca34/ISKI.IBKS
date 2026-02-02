using System.Diagnostics.CodeAnalysis;

namespace ISKI.IBKS.Domain.Entities;

public sealed class ChannelInformation
{
    public Guid Id { get; private set; }
    public Guid StationId { get; private set; }

    public string? Brand { get; private set; }

    public string? BrandModel { get; private set; }

    public required string FullName { get; set; }

    public required string Parameter { get; set; }

    public string? ParameterText { get; set; }

    public Guid? UnitId { get; private set; }

    public string? UnitText { get; private set; }

    public bool IsActive { get; private set; } = true;

    public double ChannelMinValue { get; private set; }

    public double ChannelMaxValue { get; private set; }

    public short ChannelNumber { get; private set; }

    public double CalibrationFormulaA { get; private set; }

    public double CalibrationFormulaB { get; private set; } = 1.0;

    public string? SerialNumber { get; private set; }

    public DateTime CreatedAt { get; private set; } = DateTime.UtcNow;
    public DateTime? UpdatedAt { get; private set; }
    public bool IsDeleted { get; private set; }
    public DateTime? DeletedAt { get; private set; }

    private ChannelInformation() { }

    [SetsRequiredMembers]
    public ChannelInformation(Guid stationId, string fullName, string parameter, short channelNumber)
    {
        Id = Guid.NewGuid();
        StationId = stationId;
        FullName = fullName;
        Parameter = parameter;
        ChannelNumber = channelNumber;
    }

    public void UpdateFromSais(string? brand, string? brandModel, string? unitText, Guid? unitId,
        bool isActive, double minValue, double maxValue, double formulaA, double formulaB, string? serialNumber)
    {
        Brand = brand;
        BrandModel = brandModel;
        UnitText = unitText;
        UnitId = unitId;
        IsActive = isActive;
        ChannelMinValue = minValue;
        ChannelMaxValue = maxValue;
        CalibrationFormulaA = formulaA;
        CalibrationFormulaB = formulaB;
        SerialNumber = serialNumber;
        UpdatedAt = DateTime.UtcNow;
    }

    public void Delete()
    {
        IsDeleted = true;
        DeletedAt = DateTime.UtcNow;
    }
}

