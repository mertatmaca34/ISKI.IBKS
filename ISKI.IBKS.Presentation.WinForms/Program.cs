using ISKI.IBKS.Infrastructure;
using ISKI.IBKS.Presentation.WinForms;
using ISKI.IBKS.Presentation.WinForms.Configuration;
using ISKI.IBKS.Presentation.WinForms.Features.Main;
using ISKI.IBKS.Presentation.WinForms.Navigation;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.Configuration;
using System.Runtime;

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
                    var saisConfig = new ConfigurationBuilder()
                .SetBasePath(AppContext.BaseDirectory)
                .AddJsonFile("saisappsettings.json", optional: false, reloadOnChange: true)
                .Build();

                    var marbinConfig = new ConfigurationBuilder()
                .SetBasePath(AppContext.BaseDirectory)
                .AddJsonFile("saisappsettings.json", optional: false, reloadOnChange: true)
                .Build();

                    services.AddPresentation();
                    services.AddInfrastructure(saisConfig);
                });
        }
    }
}