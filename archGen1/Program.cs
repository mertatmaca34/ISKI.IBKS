using ISKI.IBKS.Presentation.WinForms;
using ISKI.IBKS.Presentation.WinForms.Configuration;
using ISKI.IBKS.Presentation.WinForms.Features.Main;
using ISKI.IBKS.Presentation.WinForms.Navigation;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace ISKI.IBKS.Presentation.WinForms
{
    internal static class Program
    {
        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            // To customize application configuration such as set high DPI settings or default font,
            // see https://aka.ms/applicationconfiguration.
            ApplicationConfiguration.Initialize();

            using var host = CreateHostBuilder().Build();

            var mainForm = host.Services.GetRequiredService<MainForm>();

            Application.Run(mainForm);
        }

        public static IHostBuilder CreateHostBuilder()
        {
            return Host.CreateDefaultBuilder()
                .ConfigureAndUseLogging()
                .ConfigureServices((context, services) =>
                {
                    services.AddPresentation();
                });
        }
    }
}