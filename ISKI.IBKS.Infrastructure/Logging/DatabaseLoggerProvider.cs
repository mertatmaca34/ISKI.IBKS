using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace ISKI.IBKS.Infrastructure.Logging;

public class DatabaseLoggerProvider : ILoggerProvider
{
    private readonly IServiceScopeFactory _scopeFactory;

    public DatabaseLoggerProvider(IServiceScopeFactory scopeFactory)
    {
        _scopeFactory = scopeFactory;
    }

    public ILogger CreateLogger(string categoryName)
    {
        return new DatabaseLogger(categoryName, _scopeFactory);
    }

    public void Dispose()
    {
        // Nothing to dispose
    }
}
