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
using System.IO;

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
                    // Load configuration files from Infrastructure/Configuration within repository
                    var infraConfigDir = Path.Combine(AppContext.BaseDirectory, "Infrastructure", "Configuration");

                    // If running from build output where files are not copied, try project relative path
                    if (!Directory.Exists(infraConfigDir))
                    {
                        var projectRoot = Path.GetFullPath(Path.Combine(AppContext.BaseDirectory, "..", "..", "..", ".."));
                        infraConfigDir = Path.Combine(projectRoot, "ISKI.IBKS.Infrastructure", "Configuration");
                    }

                    var infraConfig = new ConfigurationBuilder()
                        .SetBasePath(infraConfigDir)
                        .AddJsonFile("sais.json", optional: false, reloadOnChange: true)
                        .AddJsonFile("plc.json", optional: false, reloadOnChange: true)
                        .AddJsonFile("ui-mapping.json", optional: false, reloadOnChange: true)
                        .Build();

                    services.AddInfrastructure(infraConfig);
                    services.AddPresentation();
                });
        }
    }
}