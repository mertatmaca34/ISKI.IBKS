using ISKI.IBKS.Domain.Entities;
using ISKI.IBKS.Domain.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using ISKI.IBKS.Application.Common.Configuration;
using ISKI.IBKS.Infrastructure.Persistence.Contexts;

namespace ISKI.IBKS.Infrastructure.Logging;

public class DatabaseLogger : ILogger
{
    private readonly string _categoryName;
    private readonly IServiceScopeFactory _scopeFactory;
    private readonly IStationConfiguration _stationConfig;

    public DatabaseLogger(string categoryName, IServiceScopeFactory scopeFactory, IStationConfiguration stationConfig)
    {
        _categoryName = categoryName;
        _scopeFactory = scopeFactory;
        _stationConfig = stationConfig;
    }

    public IDisposable? BeginScope<TState>(TState state) where TState : notnull
    {
        return null;
    }

    public bool IsEnabled(Microsoft.Extensions.Logging.LogLevel logLevel)
    {
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

        if (_categoryName.Contains("EntityFrameworkCore")) return;
        if (_categoryName.Contains("Microsoft.EntityFrameworkCore")) return;

        var message = formatter(state, exception);
        if (string.IsNullOrEmpty(message) && exception == null) return;

        ThreadPool.QueueUserWorkItem(_ =>
        {
            try
            {
                using var scope = _scopeFactory.CreateScope();
                var dbContext = scope.ServiceProvider.GetRequiredService<IbksDbContext>();

                var finalMessage = message;
                if (exception != null)
                {
                    finalMessage += $"\n| Exception: {exception.GetType().Name}: {exception.Message}";
                    if (exception.StackTrace != null)
                    {
                        finalMessage += $"\n| StackTrace: {exception.StackTrace.Substring(0, Math.Min(500, exception.StackTrace.Length))}";
                    }
                }

                ISKI.IBKS.Domain.Enums.LogLevel myLevel = logLevel switch
                {
                    Microsoft.Extensions.Logging.LogLevel.Critical => ISKI.IBKS.Domain.Enums.LogLevel.Error,
                    Microsoft.Extensions.Logging.LogLevel.Error => ISKI.IBKS.Domain.Enums.LogLevel.Error,
                    Microsoft.Extensions.Logging.LogLevel.Warning => ISKI.IBKS.Domain.Enums.LogLevel.Warning,
                    Microsoft.Extensions.Logging.LogLevel.Information => ISKI.IBKS.Domain.Enums.LogLevel.Info,
                    _ => ISKI.IBKS.Domain.Enums.LogLevel.Info
                };

                LogCategory category = LogCategory.System;
                if (_categoryName.Contains("Sais") || _categoryName.Contains("Api")) category = LogCategory.Connection;
                else if (_categoryName.Contains("Data") || _categoryName.Contains("Collection")) category = LogCategory.Data;
                else if (_categoryName.Contains("Calibration")) category = LogCategory.Calibration;
                else if (_categoryName.Contains("Plc")) category = LogCategory.Connection;

                var stationId = _stationConfig.StationId;

                var titleParts = _categoryName.Split('.');
                var shortName = titleParts.Length > 0 ? titleParts[^1] : _categoryName;

                var logEntry = new LogEntry(
                    stationId,
                    $"[{myLevel}] {shortName}",
                    finalMessage.Length > 2000 ? finalMessage.Substring(0, 2000) : finalMessage,
                    myLevel,
                    category
                );

                dbContext.LogEntries.Add(logEntry);
                dbContext.SaveChanges();
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"DatabaseLogger failed: {ex.Message}");
            }
        });
    }
}

