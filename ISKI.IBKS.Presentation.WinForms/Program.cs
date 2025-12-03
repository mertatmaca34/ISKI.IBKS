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
        [STAThread]
        static void Main()
        {
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