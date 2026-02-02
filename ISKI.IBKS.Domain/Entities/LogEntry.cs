using ISKI.IBKS.Domain.Common.Entities;
using ISKI.IBKS.Domain.Enums;
using System.Diagnostics.CodeAnalysis;

namespace ISKI.IBKS.Domain.Entities;

public sealed class LogEntry : AuditableEntity<Guid>
{
    public Guid StationId { get; private set; }
    public string Message { get; private set; } = string.Empty;
    public string Source { get; private set; } = string.Empty;
    public LogLevel Level { get; private set; }
    public LogCategory Category { get; private set; }

    private LogEntry() { }

    [SetsRequiredMembers]
    public LogEntry(
        Guid stationId,
        string message,
        string source,
        LogLevel level = LogLevel.Info,
        LogCategory category = LogCategory.System)
    {
        Id = Guid.NewGuid();
        StationId = stationId;
        Message = message;
        Source = source;
        CreatedAt = DateTime.UtcNow;
        Level = level;
        Category = category;
    }

    public static LogEntry CreateInfo(Guid stationId, string source, string message, LogCategory category = LogCategory.System)
    {
        return new LogEntry(stationId, message, source, LogLevel.Info, category);
    }

    public static LogEntry CreateWarning(Guid stationId, string source, string message, LogCategory category = LogCategory.System)
    {
        return new LogEntry(stationId, message, source, LogLevel.Warning, category);
    }

    public static LogEntry CreateError(Guid stationId, string source, string message, LogCategory category = LogCategory.System)
    {
        return new LogEntry(stationId, message, source, LogLevel.Error, category);
    }
}
