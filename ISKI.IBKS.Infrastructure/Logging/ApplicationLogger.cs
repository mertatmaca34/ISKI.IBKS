using ISKI.IBKS.Application.Common.Configuration;
using ISKI.IBKS.Domain.Entities;
using ISKI.IBKS.Domain.Enums;
using ISKI.IBKS.Infrastructure.Persistence.Contexts;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace ISKI.IBKS.Infrastructure.Logging;

public class ApplicationLogger : IApplicationLogger
{
    private readonly IServiceScopeFactory _scopeFactory;
    private readonly IStationConfiguration _stationConfig;

    public ApplicationLogger(IServiceScopeFactory scopeFactory, IStationConfiguration stationConfig)
    {
        _scopeFactory = scopeFactory;
        _stationConfig = stationConfig;
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

            var logEntry = new LogEntry(_stationConfig.StationId, title, description, level, category);
            dbContext.LogEntries.Add(logEntry);
            await dbContext.SaveChangesAsync();
        }
        catch
        {
            System.Diagnostics.Debug.WriteLine($"Failed to create log entry: {title}");
        }
    }
}

