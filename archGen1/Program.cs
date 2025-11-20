using archGen1.Features.Main;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace archGen1
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

            var mainFormPresenter = new MainFormPresenter(mainForm);

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