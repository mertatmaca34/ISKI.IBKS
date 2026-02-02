using ISKI.IBKS.Domain.Entities;
using ISKI.IBKS.Domain.Enums;

namespace ISKI.IBKS.Infrastructure.Logging;

public interface IApplicationLogger
{
    Task LogInfo(string title, string description, LogCategory category = LogCategory.System);
    Task LogWarning(string title, string description, LogCategory category = LogCategory.System);
    Task LogError(string title, string description, LogCategory category = LogCategory.System);
    Task LogError(string title, Exception exception, LogCategory category = LogCategory.System);
}

