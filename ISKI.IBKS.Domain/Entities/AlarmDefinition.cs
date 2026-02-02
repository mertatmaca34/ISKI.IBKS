using ISKI.IBKS.Domain.IoT;
using ISKI.IBKS.Domain.Common.Entities;
using ISKI.IBKS.Domain.Enums;

namespace ISKI.IBKS.Domain.Entities;

public sealed class AlarmDefinition : AuditableEntity<Guid>
{
    public string Name { get; private set; } = string.Empty;
    public string Description { get; private set; } = string.Empty;
    public string SensorName { get; private set; } = string.Empty;
    public string DbColumnName { get; private set; } = string.Empty;
    public AlarmType Type { get; private set; }
    public double? MinThreshold { get; private set; }
    public double? MaxThreshold { get; private set; }
    public bool? ExpectedDigitalValue { get; private set; }
    public AlarmPriority Priority { get; private set; }
    public bool IsActive { get; private set; }

    private AlarmDefinition() { }

    public AlarmDefinition(
        string name, string description, string sensorName, string dbColumnName,
        AlarmType type, double? minThreshold, double? maxThreshold,
        bool? expectedDigitalValue, AlarmPriority priority, bool isActive = true)
    {
        Id = Guid.NewGuid();
        Name = name;
        Description = description;
        SensorName = sensorName;
        DbColumnName = dbColumnName;
        Type = type;
        MinThreshold = minThreshold;
        MaxThreshold = maxThreshold;
        ExpectedDigitalValue = expectedDigitalValue;
        Priority = priority;
        IsActive = isActive;
    }

    public static AlarmDefinition CreateThresholdAlarm(
        string name, string dbColumnName, double? min, double? max,
        string description, AlarmPriority priority)
    {
        return new AlarmDefinition(name, description, dbColumnName, dbColumnName,
            AlarmType.Threshold, min, max, null, priority);
    }

    public static AlarmDefinition CreateDigitalAlarm(
        string name, string dbColumnName, bool expectedValue,
        string description, AlarmPriority priority)
    {
        return new AlarmDefinition(name, description, dbColumnName, dbColumnName,
            AlarmType.Digital, null, null, expectedValue, priority);
    }

    public bool Evaluate(PlcDataSnapshot snapshot)
    {
        if (!IsActive) return false;

        if (Type == AlarmType.Digital)
        {
            var value = GetDigitalValue(snapshot, DbColumnName);
            return value != ExpectedDigitalValue;
        }
        else
        {
            var value = GetAnalogValue(snapshot, DbColumnName);
            if (MinThreshold.HasValue && value < MinThreshold.Value) return true;
            if (MaxThreshold.HasValue && value > MaxThreshold.Value) return true;
        }

        return false;
    }

    public AlarmEvent CreateEvent(Guid stationId, double? triggerValue = null)
    {
        return new AlarmEvent(
            stationId,
            Id,
            SensorName,
            $"{Name}: {Description}",
            triggerValue,
            Priority);
    }

    private static bool GetDigitalValue(PlcDataSnapshot snapshot, string propertyName)
    {
        var prop = typeof(PlcDataSnapshot).GetProperty(propertyName);
        return prop != null && (bool)prop.GetValue(snapshot)!;
    }

    private static double GetAnalogValue(PlcDataSnapshot snapshot, string propertyName)
    {
        var prop = typeof(PlcDataSnapshot).GetProperty(propertyName);
        return prop != null ? (double)prop.GetValue(snapshot)! : 0;
    }

    public void Update(string name, string description, string sensorName, string dbColumnName,
        AlarmType type, double? minThreshold, double? maxThreshold,
        bool? expectedDigitalValue, AlarmPriority priority, bool isActive)
    {
        Name = name;
        Description = description;
        SensorName = sensorName;
        DbColumnName = dbColumnName;
        Type = type;
        MinThreshold = minThreshold;
        MaxThreshold = maxThreshold;
        ExpectedDigitalValue = expectedDigitalValue;
        Priority = priority;
        IsActive = isActive;
    }

    public void Deactivate() => IsActive = false;
}
