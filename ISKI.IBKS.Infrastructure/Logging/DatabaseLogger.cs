using ISKI.IBKS.Domain.Entities;
using ISKI.IBKS.Persistence.Contexts;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace ISKI.IBKS.Infrastructure.Logging;

public class DatabaseLogger : ILogger
{
    private readonly string _categoryName;
    private readonly IServiceScopeFactory _scopeFactory;

    public DatabaseLogger(string categoryName, IServiceScopeFactory scopeFactory)
    {
        _categoryName = categoryName;
        _scopeFactory = scopeFactory;
    }

    public IDisposable? BeginScope<TState>(TState state) where TState : notnull
    {
        return null;
    }

    public bool IsEnabled(Microsoft.Extensions.Logging.LogLevel logLevel)
    {
        // Only log Info and above to DB to avoid noise
        return logLevel >= Microsoft.Extensions.Logging.LogLevel.Information;
    }

    public void Log<TState>(
        Microsoft.Extensions.Logging.LogLevel logLevel, 
        EventId eventId, 
        TState state, 
        Exception? exception, 
        Func<TState, Exception?, string> formatter)
    {
        if (!IsEnabled(logLevel)) return;

        // Only filter out EF Core SQL command logs, not all EF logs
        if (_categoryName.Contains("EntityFrameworkCore.Database.Command")) return;

        try
        {
            using var scope = _scopeFactory.CreateScope();
            var dbContext = scope.ServiceProvider.GetRequiredService<IbksDbContext>();

            var message = formatter(state, exception);
            if (string.IsNullOrEmpty(message) && exception == null) return;

            if (exception != null)
            {
                message += $"\n| Exception: {exception.GetType().Name}: {exception.Message}";
                if (exception.StackTrace != null)
                {
                    message += $"\n| StackTrace: {exception.StackTrace.Substring(0, Math.Min(500, exception.StackTrace.Length))}";
                }
            }

            // Map LogLevel
            Domain.Entities.LogLevel myLevel = logLevel switch
            {
                Microsoft.Extensions.Logging.LogLevel.Critical => Domain.Entities.LogLevel.Critical,
                Microsoft.Extensions.Logging.LogLevel.Error => Domain.Entities.LogLevel.Error,
                Microsoft.Extensions.Logging.LogLevel.Warning => Domain.Entities.LogLevel.Warning,
                Microsoft.Extensions.Logging.LogLevel.Information => Domain.Entities.LogLevel.Info,
                _ => Domain.Entities.LogLevel.Debug
            };

            // Identify Category
            LogCategory category = LogCategory.System;
            if (_categoryName.Contains("Sais") || _categoryName.Contains("Api")) category = LogCategory.Connection;
            else if (_categoryName.Contains("Data") || _categoryName.Contains("Collection")) category = LogCategory.Data;
            else if (_categoryName.Contains("Calibration")) category = LogCategory.Calibration;
            else if (_categoryName.Contains("Plc")) category = LogCategory.Connection;

            // Fetch generic StationId (or use Empty if failed)
            var stationId = Guid.Empty;
            try 
            {
                var settings = dbContext.StationSettings.AsNoTracking().FirstOrDefault();
                if (settings != null) stationId = settings.StationId;
            }
            catch { /* Ignore if db access fails here */ }

            // Truncate category name for title
            var titleParts = _categoryName.Split('.');
            var shortName = titleParts.Length > 0 ? titleParts[^1] : _categoryName;
            
            var logEntry = new LogEntry(
                stationId,
                $"[{myLevel}] {shortName}",
                message.Length > 2000 ? message.Substring(0, 2000) : message,
                myLevel,
                category
            );

            dbContext.LogEntries.Add(logEntry);
            dbContext.SaveChanges(); // Synchronous to ensure it actually saves
        }
        catch (Exception ex)
        {
            // Last resort - write to debug output
            System.Diagnostics.Debug.WriteLine($"DatabaseLogger failed: {ex.Message}");
            // Also try to write to console
            Console.WriteLine($"[DatabaseLogger Error] {ex.Message}");
        }
    }
}
