using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.DependencyInjection;

namespace ISKI.IBKS.Presentation.WinForms.Middleware;

public static class LoggingConfiguration
{
    public static IHostBuilder ConfigureAndUseLogging(this IHostBuilder host)
    {
        // Only use DatabaseLogger - no file logging
        return host.ConfigureLogging((context, logging) =>
        {
            // Add database logger provider to the logging pipeline
            logging.Services.AddSingleton<Microsoft.Extensions.Logging.ILoggerProvider, ISKI.IBKS.Infrastructure.Logging.DatabaseLoggerProvider>();
        });
    }
}
