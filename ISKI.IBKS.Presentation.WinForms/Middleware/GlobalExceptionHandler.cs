using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ISKI.IBKS.Presentation.WinForms.Middleware;

public class GlobalExceptionHandler
{
    public GlobalExceptionHandler(ILogger<GlobalExceptionHandler> logger)
    {
        System.Windows.Forms.Application.SetUnhandledExceptionMode(UnhandledExceptionMode.CatchException);

        System.Windows.Forms.Application.ThreadException += (sender, args) =>
        {
            logger.LogError(args.Exception, "Unhandled Thread Exception");
        };

        AppDomain.CurrentDomain.UnhandledException += (sender, args) =>
        {
            if (args.ExceptionObject is Exception ex)
            {
                logger.LogError(ex, "Unhandled Domain Exception");
            }
        };

        TaskScheduler.UnobservedTaskException += (sender, args) =>
        {
            logger.LogError(args.Exception, "Unobserved Task Exception");
            args.SetObserved();
        };
    }
}