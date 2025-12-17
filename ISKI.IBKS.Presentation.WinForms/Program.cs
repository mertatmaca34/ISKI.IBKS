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
using System.Threading.Tasks;

namespace ISKI.IBKS.Presentation.WinForms
{
    internal static class Program
    {
        [STAThread]
        static async Task Main()
        {
            ApplicationConfiguration.Initialize();

            using var host = CreateHostBuilder().Build();

            await host.StartAsync();
            
            var mainForm = host.Services.GetRequiredService<MainForm>();

            System.Windows.Forms.Application.Run(mainForm);

            await host.StopAsync();
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

                    services.AddInfrastructure(saisConfig);
                    services.AddPresentation();
                });
        }
    }
}