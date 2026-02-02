using Microsoft.Extensions.DependencyInjection;
using ISKI.IBKS.Presentation.WinForms.Features.Main.View;
using ISKI.IBKS.Presentation.WinForms.Features.SetupWizard.WizardMain;
using ISKI.IBKS.Presentation.WinForms.Features.SetupWizard.StationSettings;
using ISKI.IBKS.Presentation.WinForms.Features.SetupWizard.PlcSettings;
using ISKI.IBKS.Presentation.WinForms.Features.SetupWizard.SaisApiSettings;
using ISKI.IBKS.Presentation.WinForms.Features.SetupWizard.CalibrationSettings;
using ISKI.IBKS.Presentation.WinForms.Features.SetupWizard.MailSettings;
using ISKI.IBKS.Presentation.WinForms.Features.SetupWizard.Model;
using ISKI.IBKS.Presentation.WinForms.Features.SetupWizard.Steps;
using ISKI.IBKS.Presentation.WinForms.Features.HomePage.View;
using ISKI.IBKS.Presentation.WinForms.Features.HomePage.Presenter;
using ISKI.IBKS.Presentation.WinForms.Features.SimulationPage.View;
using ISKI.IBKS.Presentation.WinForms.Features.SimulationPage.Presenter;
using ISKI.IBKS.Presentation.WinForms.Features.CalibrationPage.View;
using ISKI.IBKS.Presentation.WinForms.Features.CalibrationPage.Presenter;
using ISKI.IBKS.Presentation.WinForms.Features.MailPage.MailMain.View;
using ISKI.IBKS.Presentation.WinForms.Features.MailPage.MailMain.Presenter;
using ISKI.IBKS.Presentation.WinForms.Features.MailPage.MailStatements.View;
using ISKI.IBKS.Presentation.WinForms.Features.MailPage.MailStatements.Presenter;
using ISKI.IBKS.Presentation.WinForms.Features.MailPage.MailUsers.View;
using ISKI.IBKS.Presentation.WinForms.Features.MailPage.MailUsers.Presenter;
using ISKI.IBKS.Presentation.WinForms.Features.ReportingPage.View;
using ISKI.IBKS.Presentation.WinForms.Features.ReportingPage.Presenter;
using ISKI.IBKS.Presentation.WinForms.Features.SettingsPage.SettingsMain.View;
using ISKI.IBKS.Presentation.WinForms.Features.SettingsPage.PlcSettings.View;
using ISKI.IBKS.Presentation.WinForms.Features.SettingsPage.PlcSettings.Presenter;
using ISKI.IBKS.Presentation.WinForms.Features.SettingsPage.StationSettings.View;
using ISKI.IBKS.Presentation.WinForms.Features.SettingsPage.StationSettings.Presenter;
using ISKI.IBKS.Presentation.WinForms.Features.SettingsPage.ApiSettings.View;
using ISKI.IBKS.Presentation.WinForms.Features.SettingsPage.ApiSettings.Presenter;
using ISKI.IBKS.Presentation.WinForms.Features.SettingsPage.MailSettings.View;
using ISKI.IBKS.Presentation.WinForms.Features.SettingsPage.MailSettings.Presenter;
using ISKI.IBKS.Presentation.WinForms.Features.SettingsPage.CalibrationSettings.View;
using ISKI.IBKS.Presentation.WinForms.Features.SettingsPage.CalibrationSettings.Presenter;
using ISKI.IBKS.Presentation.WinForms.Common.Navigation;
using System.Reflection;

namespace ISKI.IBKS.Presentation.WinForms;

public static class DependencyInjection
{
    public static IServiceCollection AddPresentation(this IServiceCollection services)
    {
        services.AddSingleton<INavigationService>(sp => 
            new NavigationService(sp, sp.GetRequiredService<MainForm>().ContentPanel));

        // Middleware
        services.AddSingleton<Middleware.GlobalExceptionHandler>();

        // Main
        services.AddSingleton<MainForm>();
        services.AddTransient<IMainFormView>(p => p.GetRequiredService<MainForm>());

        // Setup Wizard
        services.AddScoped<SetupState>();
        services.AddTransient<SetupWizardForm>();
        services.AddTransient<ISetupWizardView>(p => p.GetRequiredService<SetupWizardForm>());

        //Setup Wizard Steps
        services.AddTransient<StationSettingsStep>();
        services.AddTransient<IStationSettingsStepView>(p => p.GetRequiredService<StationSettingsStep>());
        services.AddTransient<ISetupWizardStep>(p => p.GetRequiredService<StationSettingsStep>());

        services.AddTransient<PlcSettingsStep>();
        services.AddTransient<IPlcSettingsStepView>(p => p.GetRequiredService<PlcSettingsStep>());
        services.AddTransient<ISetupWizardStep>(p => p.GetRequiredService<PlcSettingsStep>());

        services.AddTransient<SaisApiSettingsStep>();
        services.AddTransient<ISaisApiSettingsStepView>(p => p.GetRequiredService<SaisApiSettingsStep>());
        services.AddTransient<ISetupWizardStep>(p => p.GetRequiredService<SaisApiSettingsStep>());

        services.AddTransient<CalibrationSettingsStepPage>();
        services.AddTransient<ICalibrationSettingsStepView>(p => p.GetRequiredService<CalibrationSettingsStepPage>());
        services.AddTransient<ISetupWizardStep>(p => p.GetRequiredService<CalibrationSettingsStepPage>());

        services.AddTransient<MailSettingsStepPage>();
        services.AddTransient<IMailSettingsStepView>(p => p.GetRequiredService<MailSettingsStepPage>());
        services.AddTransient<ISetupWizardStep>(p => p.GetRequiredService<MailSettingsStepPage>());

        services.AddTransient<FinalSummaryStep>();
        services.AddTransient<IFinalSummaryStepView>(p => p.GetRequiredService<FinalSummaryStep>());
        services.AddTransient<ISetupWizardStep>(p => p.GetRequiredService<FinalSummaryStep>());

        services.AddTransient<SetupWizardPresenter>();

        // Home Page
        services.AddTransient<HomePage>();
        services.AddTransient<IHomePageView>(p => p.GetRequiredService<HomePage>());
        services.AddTransient<HomePagePresenter>();

        // Simulation Page
        services.AddTransient<SimulationPage>();
        services.AddTransient<ISimulationPageView>(p => p.GetRequiredService<SimulationPage>());
        services.AddTransient<SimulationPagePresenter>();

        // Calibration Page
        services.AddTransient<CalibrationPage>();
        services.AddTransient<ICalibrationPageView>(p => p.GetRequiredService<CalibrationPage>());
        services.AddTransient<CalibrationPagePresenter>();

        // Mail Page
        // Mail Page
        services.AddTransient<MailPage>();
        services.AddTransient<IMailPageView>(p => p.GetRequiredService<MailPage>());
        services.AddTransient<MailPagePresenter>();

        // Mail Statements Page
        services.AddTransient<MailStatementsPage>();
        services.AddTransient<IMailStatementsPageView>(p => p.GetRequiredService<MailStatementsPage>());
        services.AddTransient<MailStatementsPagePresenter>();

        // Mail Users Page
        services.AddTransient<MailUsersPage>();
        services.AddTransient<IMailUsersPageView>(p => p.GetRequiredService<MailUsersPage>());
        services.AddTransient<MailUsersPagePresenter>();

        // Reporting Page
        services.AddTransient<ReportingPage>();
        services.AddTransient<IReportingPageView>(p => p.GetRequiredService<ReportingPage>());
        services.AddTransient<ReportingPagePresenter>();

        // Settings Page
        services.AddTransient<SettingsPage>();
        services.AddTransient<ISettingsPageView>(p => p.GetRequiredService<SettingsPage>());
        // StationSettings Page
        services.AddTransient<StationSettingsPage>();
        services.AddTransient<IStationSettingsPageView>(p => p.GetRequiredService<StationSettingsPage>());
        services.AddTransient<StationSettingsPresenter>();

        // PlcSettings Page
        services.AddTransient<PlcSettingsPage>();
        services.AddTransient<IPlcSettingsPageView>(p => p.GetRequiredService<PlcSettingsPage>());
        services.AddTransient<PlcSettingsPresenter>();

        // ApiSettings Page
        services.AddTransient<ApiSettingsPage>();
        services.AddTransient<IApiSettingsPageView>(p => p.GetRequiredService<ApiSettingsPage>());
        services.AddTransient<ApiSettingsPresenter>();

        // MailSettings Page
        services.AddTransient<MailServerSettingsPage>();
        services.AddTransient<IMailServerSettingsPageView>(p => p.GetRequiredService<MailServerSettingsPage>());
        services.AddTransient<MailServerSettingsPresenter>();

        // CalibrationSettings Page
        services.AddTransient<CalibrationSettingsPage>();
        services.AddTransient<ICalibrationSettingsPageView>(p => p.GetRequiredService<CalibrationSettingsPage>());
        services.AddTransient<CalibrationSettingsPresenter>();

        // Auto-register any other Presenters/Views if missed
        // services.Scan(scan => scan
        //    .FromAssemblyOf<MainForm>()
        //    .AddClasses(classes => classes.Where(type => type.Name.EndsWith("Presenter")))
        //    .AsSelf()
        //    .WithTransientLifetime());

        return services;
    }
}
