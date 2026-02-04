using ISKI.IBKS.Infrastructure;
using ISKI.IBKS.Presentation.WinForms;
using ISKI.IBKS.Presentation.WinForms.Features.Main;
using ISKI.IBKS.Presentation.WinForms.Features.SetupWizard;
using ISKI.IBKS.Persistence.Contexts;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
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

            // SQL Server Check and Installation
            using (var scope = host.Services.CreateScope())
            {
                var sqlInstallService = scope.ServiceProvider.GetRequiredService<Application.Services.Sql.ISqlInstallationService>();
                
                if (!sqlInstallService.IsSqlExpressInstalled())
                {
                    var sqlInstallForm = new Features.Shared.SqlInstallationForm();
                    sqlInstallForm.Show();
                    System.Windows.Forms.Application.DoEvents();

                    // Progress reporter ve CancellationToken
                    var progress = new Progress<string>(status => sqlInstallForm.UpdateStatus(status));
                    var cancellationToken = sqlInstallForm.CancellationToken;
                    
                    // Async kurulum
                    ISKI.IBKS.Application.Services.Sql.SqlInstallationResult? installResult = null;
                    
                    var installTask = Task.Run(async () =>
                    {
                        installResult = await sqlInstallService.InstallSqlExpressAsync(progress, cancellationToken);
                    });

                    // UI thread'i bloklamadan bekle
                    while (!installTask.IsCompleted)
                    {
                        System.Windows.Forms.Application.DoEvents();
                        Thread.Sleep(100);
                    }

                    sqlInstallForm.CloseForm();

                    // İptal edildi mi kontrol et
                    if (sqlInstallForm.IsCancelled)
                    {
                        MessageBox.Show(
                            "Kurulum iptal edildi. Uygulama kapatılacak.",
                            "Kurulum İptal Edildi",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Information);
                        return;
                    }

                    if (installResult == null || !installResult.Success)
                    {
                        string errorMessage = installResult?.ErrorMessage 
                            ?? "Bilinmeyen hata oluştu.";
                        MessageBox.Show(
                            $"SQL Server Express kurulumu başarısız oldu.\n\n{errorMessage}", 
                            "Hata", 
                            MessageBoxButtons.OK, 
                            MessageBoxIcon.Error);
                        return;
                    }
                }
            }

            // Veritabanı Şeması Oluşturma
            using (var scope = host.Services.CreateScope())
            {
                var services = scope.ServiceProvider;
                try
                {
                    var context = services.GetRequiredService<IbksDbContext>();
                    
                    // Veritabanı ve migrasyonları uygula (EnsureCreated kullanılmaz çünkü migrasyon ile çakışır)
                    context.Database.MigrateAsync().GetAwaiter().GetResult();
                    
                    // Seed data
                    ISKI.IBKS.Persistence.Seeders.AlarmSeeder.SeedAsync(context).GetAwaiter().GetResult();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(
                        $"Veritabanı başlatılırken hata oluştu:\n{ex.Message}", 
                        "Kritik Hata", 
                        MessageBoxButtons.OK, 
                        MessageBoxIcon.Error);
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
                var plcClient = scope.ServiceProvider.GetRequiredService<ISKI.IBKS.Application.Features.Plc.Abstractions.IPlcClient>();
                var saisAuthClient = scope.ServiceProvider.GetRequiredService<ISKI.IBKS.Infrastructure.RemoteApi.SAIS.Abstractions.ISaisAuthClient>();
                var mailService = scope.ServiceProvider.GetRequiredService<ISKI.IBKS.Application.Services.Mail.IAlarmMailService>();
                var iisService = scope.ServiceProvider.GetRequiredService<ISKI.IBKS.Application.Services.Iis.IIisDeploymentService>();
                
                var setupWizard = new SetupWizardForm(context, plcClient, saisAuthClient, mailService, iisService);
                
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
