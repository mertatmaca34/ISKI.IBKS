using Iski.IBKS.Presentation.WinForms;
using Iski.IBKS.Presentation.WinForms.Features.Main;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Iski.IBKS.Presentation.WinForms
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

            //using var host = CreateHostBuilder().Build();

            //var mainForm = host.Services.GetRequiredService<CounterForm>();

            var mainForm = new MainForm();
            mainForm.Tag = new MainFormPresenter(mainForm);

            Application.Run(mainForm);
        }

        public static IHostBuilder CreateHostBuilder()
        {
            return Host.CreateDefaultBuilder()
                .ConfigureServices((context, services) =>
                {
                    services.AddWinForms();
                });
        }
    }
}