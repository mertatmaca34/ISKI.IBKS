using ISKI.IBKS.Infrastructure;
using ISKI.IBKS.Presentation.WinForms;
using ISKI.IBKS.Presentation.WinForms.Features.Main;
using ISKI.IBKS.Presentation.WinForms.Features.SetupWizard;
using ISKI.IBKS.Persistence.Contexts;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System.Configuration;
using System.Runtime;
using System.Threading.Tasks;
using System.IO;
using ISKI.IBKS.Presentation.WinForms.Middleware;


namespace ISKI.IBKS.Presentation.WinForms
{
    internal static class Program
    {
        [STAThread]
        static void Main()
        {
            ApplicationConfiguration.Initialize();

            using var host = CreateHostBuilder().Build();

            // 1. Veritabanı Migrasyonlarını Uygula (Veritabanı yoksa oluşturur)
            using (var scope = host.Services.CreateScope())
            {
                var services = scope.ServiceProvider;
                try
                {
                    var context = services.GetRequiredService<IbksDbContext>();
                    // Veritabanı oluşturma ve migrasyonları uygulama
                    context.Database.MigrateAsync().GetAwaiter().GetResult();
                    
                    // Veri tohumlama (Seed)
                    ISKI.IBKS.Persistence.Seeders.AlarmSeeder.SeedAsync(context).GetAwaiter().GetResult();
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Veritabanı başlatılırken hata oluştu:\n{ex.Message}", "Kritik Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
            }


            host.StartAsync().GetAwaiter().GetResult();
            
            // 2. Kurulum Kontrolü ve Akışı
            bool startMainApp = false;
            bool setupRequired = false;

            // Check using a scope
            using (var scope = host.Services.CreateScope())
            {
                var context = scope.ServiceProvider.GetRequiredService<IbksDbContext>();
                setupRequired = SetupWizardForm.IsSetupRequired(context);
            }

            if (setupRequired)
            {
                // Kurulum sihirbazını başlat
                using var scope = host.Services.CreateScope();
                // SetupWizardForm için gerekli servisleri DI container'dan alabiliriz
                var context = scope.ServiceProvider.GetRequiredService<IbksDbContext>();
                var setupWizard = new SetupWizardForm(context);
                
                System.Windows.Forms.Application.Run(setupWizard);

                if (setupWizard.IsCompleted)
                {
                    startMainApp = true;
                }
                else
                {
                    // Kullanıcı kurulumu tamamlamadan kapattı
                    host.StopAsync().GetAwaiter().GetResult();
                    return;
                }
            }
            else
            {
                startMainApp = true;
            }

            // 3. Ana Uygulamanın Başlatılması
            if (startMainApp)
            {
                var mainForm = host.Services.GetRequiredService<MainForm>();
                System.Windows.Forms.Application.Run(mainForm);
            }

            host.StopAsync().GetAwaiter().GetResult();
        }

        public static IHostBuilder CreateHostBuilder()
        {
            return Host.CreateDefaultBuilder()
                .ConfigureAndUseLogging()
                .ConfigureServices((context, services) =>
                {
                    // Configuration dosyaları build output altındaki Configuration klasöründe bulunur
                    var infraConfigDir = Path.Combine(AppContext.BaseDirectory, "Configuration");

                    // Klasör yoksa oluştur (SetupWizard kaydedebilsin diye)
                    if (!Directory.Exists(infraConfigDir))
                    {
                        Directory.CreateDirectory(infraConfigDir);
                    }

                    var infraConfig = new ConfigurationBuilder()
                        .SetBasePath(infraConfigDir)
                        .AddJsonFile(Path.Combine(AppContext.BaseDirectory, "appsettings.json"), optional: true, reloadOnChange: true)
                        .AddJsonFile("sais.json", optional: true, reloadOnChange: true)
                        .AddJsonFile("plc.json", optional: true, reloadOnChange: true)
                        .AddJsonFile("station.json", optional: true, reloadOnChange: true)
                        .AddJsonFile("mail.json", optional: true, reloadOnChange: true)
                        .AddJsonFile("calibration.json", optional: true, reloadOnChange: true)
                        .AddJsonFile("ui-mapping.json", optional: true, reloadOnChange: true)
                        .Build();

                    services.AddInfrastructure(infraConfig);
                    services.AddPresentation();
                });
        }
    }
}
