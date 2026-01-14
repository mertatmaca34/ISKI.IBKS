using ISKI.IBKS.Domain.Entities;
using ISKI.IBKS.Persistence.Contexts;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace ISKI.IBKS.Infrastructure.Logging;

public interface IApplicationLogger
{
    Task LogInfo(string title, string description, LogCategory category = LogCategory.System);
    Task LogWarning(string title, string description, LogCategory category = LogCategory.System);
    Task LogError(string title, string description, LogCategory category = LogCategory.System);
    Task LogError(string title, Exception exception, LogCategory category = LogCategory.System);
}

public class ApplicationLogger : IApplicationLogger
{
    private readonly IServiceScopeFactory _scopeFactory;

    public ApplicationLogger(IServiceScopeFactory scopeFactory)
    {
        _scopeFactory = scopeFactory;
    }

    public async Task LogInfo(string title, string description, LogCategory category = LogCategory.System)
    {
        await CreateLogEntry(title, description, LogLevel.Info, category);
    }

    public async Task LogWarning(string title, string description, LogCategory category = LogCategory.System)
    {
        await CreateLogEntry(title, description, LogLevel.Warning, category);
    }

    public async Task LogError(string title, string description, LogCategory category = LogCategory.System)
    {
        await CreateLogEntry(title, description, LogLevel.Error, category);
    }

    public async Task LogError(string title, Exception exception, LogCategory category = LogCategory.System)
    {
        var description = $"{exception.GetType().Name}: {exception.Message}";
        if (exception.StackTrace != null)
        {
            description += $"\n\nStack Trace:\n{exception.StackTrace}";
        }
        await CreateLogEntry(title, description, LogLevel.Error, category);
    }

    private async Task CreateLogEntry(string title, string description, LogLevel level, LogCategory category)
    {
        try
        {
            using var scope = _scopeFactory.CreateScope();
            var dbContext = scope.ServiceProvider.GetRequiredService<IbksDbContext>();

            var stationId = Guid.Empty;
            try
            {
                var settings = await dbContext.StationSettings.AsNoTracking().FirstOrDefaultAsync();
                if (settings != null) stationId = settings.StationId;
            }
            catch { /* Ignore if station settings not available */ }

            var logEntry = new LogEntry(stationId, title, description, level, category);
            dbContext.LogEntries.Add(logEntry);
            await dbContext.SaveChangesAsync();
        }
        catch
        {
            // Fail silently to avoid breaking the application
            System.Diagnostics.Debug.WriteLine($"Failed to create log entry: {title}");
        }
    }
}
