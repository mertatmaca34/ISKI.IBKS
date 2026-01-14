using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.DependencyInjection; // Added
using Microsoft.VisualBasic.Logging;
using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Log = Serilog.Log;

namespace ISKI.IBKS.Presentation.WinForms.Middleware;

public static class LoggingConfiguration
{
    public static IHostBuilder ConfigureAndUseLogging(this IHostBuilder host)
    {
        string desktopPath = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
        string logsDirectory = Path.Combine(desktopPath, "logs");

        Directory.CreateDirectory(logsDirectory);

        string fileName = $"{DateTime.Now:yyyy-MM-dd}.txt";

        string fullPath = Path.Combine(logsDirectory, fileName);

        Log.Logger = new LoggerConfiguration()
            .WriteTo.File(fullPath)
            .CreateLogger();

        return host.UseSerilog()
                   .ConfigureLogging((context, logging) =>
                   {
                       // Add database logger provider to the logging pipeline
                       logging.Services.AddSingleton<Microsoft.Extensions.Logging.ILoggerProvider, ISKI.IBKS.Infrastructure.Logging.DatabaseLoggerProvider>();
                   });
    }
}
