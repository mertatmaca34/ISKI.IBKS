using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.DependencyInjection;
using ISKI.IBKS.Infrastructure.Logging;
using Microsoft.Extensions.Logging;

namespace ISKI.IBKS.Presentation.WinForms.Middleware;

public static class LoggingConfiguration
{
    public static IHostBuilder ConfigureAndUseLogging(this IHostBuilder host)
    {
        return host.ConfigureLogging((context, logging) =>
        {
            logging.Services.AddSingleton<ILoggerProvider, DatabaseLoggerProvider>();
        });
    }
}

