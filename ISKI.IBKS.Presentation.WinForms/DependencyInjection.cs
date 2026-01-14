using ISKI.IBKS.Presentation.WinForms.Common.Navigation;
using ISKI.IBKS.Presentation.WinForms.Extensions;
using ISKI.IBKS.Presentation.WinForms.Features.CalibrationPage;
using ISKI.IBKS.Presentation.WinForms.Features.HomePage;
using ISKI.IBKS.Presentation.WinForms.Features.Main;
using ISKI.IBKS.Presentation.WinForms.Features.ReportingPage;
using ISKI.IBKS.Presentation.WinForms.Features.SimulationPage;
using ISKI.IBKS.Presentation.WinForms.Middleware;
using Microsoft.Extensions.DependencyInjection;

namespace ISKI.IBKS.Presentation.WinForms;

/// <summary>
/// Dependency injection configuration for the Presentation layer.
/// </summary>
public static class DependencyInjection
{
    /// <summary>
    /// Adds presentation layer services to the service collection.
    /// </summary>
    public static IServiceCollection AddPresentation(this IServiceCollection services)
    {
        // Core services
        services.AddSingleton<GlobalExceptionHandler>();

        // MainForm and NavigationService are registered together since NavigationService
        // needs to be created after MainForm to access its content panel
        services.AddSingleton<MainForm>(sp =>
        {
            var logger = sp.GetRequiredService<Microsoft.Extensions.Logging.ILogger<MainForm>>();
            var form = new MainForm(sp, logger);

            // Create navigation service with the form's content panel
            var navigationService = new NavigationService(sp, form.ContentPanel);

            // Now set the navigation service on the form
            form.SetNavigationService(navigationService);

            // Create presenter
            _ = ActivatorUtilities.CreateInstance<MainFormPresenter>(sp, (IMainFormView)form);

            return form;
        });

        services.AddSingleton<IMainFormView>(sp => sp.GetRequiredService<MainForm>());
        services.AddSingleton<INavigationService>(sp =>
        {
            var form = sp.GetRequiredService<MainForm>();
            return new NavigationService(sp, form.ContentPanel);
        });

        // Pages - Add new pages here as they are created
        services.AddPage<IHomePageView, HomePage, HomePagePresenter>();
        services.AddPage<SimulationPage>();
        services.AddPage<CalibrationPage>();
        services.AddPage<ReportingPage>();

        services.AddPage<ISKI.IBKS.Presentation.WinForms.Features.MailPage.MailPage>();
        services.AddPage<ISKI.IBKS.Presentation.WinForms.Features.SettingsPage.SettingsPage>();

        return services;
    }
}


