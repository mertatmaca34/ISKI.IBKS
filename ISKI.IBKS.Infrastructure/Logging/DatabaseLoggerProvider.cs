using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using ISKI.IBKS.Application.Common.Configuration;

namespace ISKI.IBKS.Infrastructure.Logging;

public class DatabaseLoggerProvider : ILoggerProvider
{
    private readonly IServiceScopeFactory _scopeFactory;
    private readonly IStationConfiguration _stationConfig;

    public DatabaseLoggerProvider(IServiceScopeFactory scopeFactory, IStationConfiguration stationConfig)
    {
        _scopeFactory = scopeFactory;
        _stationConfig = stationConfig;
    }

    public ILogger CreateLogger(string categoryName)
    {
        return new DatabaseLogger(categoryName, _scopeFactory, _stationConfig);
    }

    public void Dispose()
    {
    }
}

