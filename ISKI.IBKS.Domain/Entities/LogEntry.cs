using ISKI.IBKS.Domain.Common.Entities;
using System.Diagnostics.CodeAnalysis;

namespace ISKI.IBKS.Domain.Entities;

/// <summary>
/// Sistem log kayıtlarını temsil eder.
/// SAIS GetLog servisi için kullanılır.
/// </summary>
public sealed class LogEntry : Entity<Guid>
{
    public Guid StationId { get; private set; }
    public string LogTitle { get; private set; } = string.Empty;
    public string LogDescription { get; private set; } = string.Empty;
    public DateTime LogCreatedDate { get; private set; }
    public LogLevel Level { get; private set; }
    public LogCategory Category { get; private set; }

    private LogEntry() { }

    [SetsRequiredMembers]
    public LogEntry(
        Guid stationId,
        string logTitle,
        string logDescription,
        LogLevel level = LogLevel.Info,
        LogCategory category = LogCategory.System)
    {
        Id = Guid.NewGuid();
        StationId = stationId;
        LogTitle = logTitle;
        LogDescription = logDescription;
        LogCreatedDate = DateTime.UtcNow;
        Level = level;
        Category = category;
    }

    public static LogEntry CreateInfo(Guid stationId, string title, string description, LogCategory category = LogCategory.System)
    {
        return new LogEntry(stationId, title, description, LogLevel.Info, category);
    }

    public static LogEntry CreateWarning(Guid stationId, string title, string description, LogCategory category = LogCategory.System)
    {
        return new LogEntry(stationId, title, description, LogLevel.Warning, category);
    }

    public static LogEntry CreateError(Guid stationId, string title, string description, LogCategory category = LogCategory.System)
    {
        return new LogEntry(stationId, title, description, LogLevel.Error, category);
    }

    public static LogEntry CreateCalibrationLog(Guid stationId, string sensorName, string description)
    {
        return new LogEntry(stationId, $"{sensorName} Kalibrasyon", description, LogLevel.Info, LogCategory.Calibration);
    }

    public static LogEntry CreateAlarmLog(Guid stationId, string alarmName, string description)
    {
        return new LogEntry(stationId, alarmName, description, LogLevel.Warning, LogCategory.Alarm);
    }

    public static LogEntry CreatePowerLog(Guid stationId, string description)
    {
        return new LogEntry(stationId, "İstasyon Enerji Durumu", description, LogLevel.Warning, LogCategory.Power);
    }
}

public enum LogLevel
{
    Info = 1,
    Warning = 2,
    Error = 3
}

public enum LogCategory
{
    System = 0,
    Calibration = 1,
    Alarm = 2,
    Power = 3,
    Connection = 4,
    Data = 5,
    Configuration = 6,
    ApiCall = 7
}
