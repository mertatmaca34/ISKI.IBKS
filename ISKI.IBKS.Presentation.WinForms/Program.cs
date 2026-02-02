using ISKI.IBKS.Shared.Constants;
using ISKI.IBKS.Shared.Results;
using ISKI.IBKS.Infrastructure;
using ISKI.IBKS.Application.Common.Features.Setup.InstallSql;
using ISKI.IBKS.Infrastructure.Services.Sql;
using ISKI.IBKS.Presentation.WinForms.Features.Main.View;
using ISKI.IBKS.Presentation.WinForms.Features.SetupWizard.WizardMain;
using ISKI.IBKS.Presentation.WinForms.Features.Shared;
using ISKI.IBKS.Shared.Localization;
using ISKI.IBKS.Presentation.WinForms.Middleware;
using ISKI.IBKS.Presentation.WinForms.Common.Navigation;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using ISKI.IBKS.Shared.Constants;
using ISKI.IBKS.Infrastructure.Persistence.Contexts;
using ISKI.IBKS.Infrastructure.Persistence.Seeders;

namespace ISKI.IBKS.Presentation.WinForms
{
    internal static class Program
    {
        [STAThread]
        static void Main()
        {
            ApplicationConfiguration.Initialize();

            using var host = CreateHostBuilder().Build();

            if (!SqlHelper.IsSqlExpressInstalled())
            {
                var sqlInstallForm = new SqlInstallationForm();
                sqlInstallForm.Show();
                System.Windows.Forms.Application.DoEvents();

                var progress = new Progress<string>(status => sqlInstallForm.UpdateStatus(status));
                var cancellationToken = sqlInstallForm.CancellationToken;

                var bus = host.Services.GetRequiredService<Wolverine.IMessageBus>();
                var installTask = bus.InvokeAsync<Result<SqlInstallationResult>>(
                    new InstallSqlCommand());

                while (!installTask.IsCompleted)
                {
                    System.Windows.Forms.Application.DoEvents();
                    Thread.Sleep(100);
                }

                sqlInstallForm.CloseForm();

                if (sqlInstallForm.IsCancelled)
                {
                    MessageBox.Show(
                        Strings.Setup_CancelConfirmation,
                        Strings.Setup_CancelTitle,
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Information);
                    return;
                }

                var installResult = installTask.GetAwaiter().GetResult();
                if (!installResult.IsSuccess)
                {
                    MessageBox.Show(
                        $"{Strings.Setup_SqlInstallFailed}\n\n{installResult.Error.Message}",
                        Strings.Common_Error,
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Error);
                    return;
                }
            }

            bool startMainApp = false;
            bool setupRequired = false;

            using (var scope = host.Services.CreateScope())
            {
                var context = scope.ServiceProvider.GetRequiredService<IbksDbContext>();
                setupRequired = SetupWizardForm.IsSetupRequired(context);
            }

            if (setupRequired)
            {
                using var scope = host.Services.CreateScope();
                var setupWizard = scope.ServiceProvider.GetRequiredService<SetupWizardForm>();

                System.Windows.Forms.Application.Run(setupWizard);

                if (setupWizard.IsCompleted)
                {
                    startMainApp = true;
                }
            }
            else
            {
                startMainApp = true;
            }

            if (startMainApp)
            {
                using (var scope = host.Services.CreateScope())
                {
                    // Activate Global Exception Handler
                    var exceptionHandler = scope.ServiceProvider.GetRequiredService<Middleware.GlobalExceptionHandler>();

                    var services = scope.ServiceProvider;
                    try
                    {
                        var context = services.GetRequiredService<IbksDbContext>();
                        context.Database.EnsureCreated();
                        AlarmSeeder.SeedAsync(context).GetAwaiter().GetResult();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(
                            $"{Strings.Setup_DatabaseError}\n{ex.Message}",
                            Strings.Common_CriticalError,
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Error);
                        return;
                    }
                }

                host.StartAsync().GetAwaiter().GetResult();

                var mainForm = host.Services.GetRequiredService<MainForm>();
                var navService = host.Services.GetRequiredService<INavigationService>();
                mainForm.SetNavigationService(navService);
                
                System.Windows.Forms.Application.Run(mainForm);

                host.StopAsync().GetAwaiter().GetResult();
            }
        }

        public static IHostBuilder CreateHostBuilder()
        {
            return Host.CreateDefaultBuilder()
                .ConfigureAndUseLogging()
                .ConfigureServices((context, services) =>
                {
                    var infraConfigDir = Path.Combine(AppContext.BaseDirectory, ConfigurationConstants.ConfigurationDirectory);

                    if (!Directory.Exists(infraConfigDir))
                    {
                        Directory.CreateDirectory(infraConfigDir);
                    }

                    var infraConfig = new ConfigurationBuilder()
                        .SetBasePath(infraConfigDir)
                        .AddJsonFile(Path.Combine(AppContext.BaseDirectory, ConfigurationConstants.Files.AppSettings), optional: true, reloadOnChange: true)
                        .AddJsonFile(ConfigurationConstants.Files.SaisConfig, optional: true, reloadOnChange: true)
                        .AddJsonFile(ConfigurationConstants.Files.PlcConfig, optional: true, reloadOnChange: true)
                        .AddJsonFile(ConfigurationConstants.Files.StationConfig, optional: true, reloadOnChange: true)
                        .AddJsonFile(ConfigurationConstants.Files.MailConfig, optional: true, reloadOnChange: true)
                        .AddJsonFile(ConfigurationConstants.Files.CalibrationConfig, optional: true, reloadOnChange: true)
                        .AddJsonFile(ConfigurationConstants.Files.UiMappingConfig, optional: true, reloadOnChange: true)
                        .Build();

                    services.AddInfrastructure(infraConfig);
                    services.AddPresentation();
                });
        }
    }
}

