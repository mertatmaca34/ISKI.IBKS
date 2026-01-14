using ISKI.IBKS.Presentation.WinForms.Common.Navigation;
using ISKI.IBKS.Presentation.WinForms.Features.Main.Controls;

// Type aliases to resolve namespace/class name conflicts
using HomePageControl = ISKI.IBKS.Presentation.WinForms.Features.HomePage.HomePage;
using SimulationPageControl = ISKI.IBKS.Presentation.WinForms.Features.SimulationPage.SimulationPage;
using CalibrationPageControl = ISKI.IBKS.Presentation.WinForms.Features.CalibrationPage.CalibrationPage;
using ReportingPageControl = ISKI.IBKS.Presentation.WinForms.Features.ReportingPage.ReportingPage;

using MailPageControl = ISKI.IBKS.Presentation.WinForms.Features.MailPage.MailPage;
using SettingsPageControl = ISKI.IBKS.Presentation.WinForms.Features.SettingsPage.SettingsPage;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.DependencyInjection;

namespace ISKI.IBKS.Presentation.WinForms.Features.Main
{
    /// <summary>
    /// Main application form containing the navigation sidebar and content area.
    /// </summary>
    public partial class MainForm : Form, IMainFormView
    {
        private readonly IServiceProvider _serviceProvider;
        private readonly ILogger<MainForm> _logger;
        private INavigationService? _navigationService;
        private NavigationBarButton? _activeButton;

        /// <summary>
        /// Gets the content panel where pages are displayed.
        /// Used by NavigationService.
        /// </summary>
        public Panel ContentPanel => panel1;

        public MainForm(IServiceProvider serviceProvider, ILogger<MainForm> logger)
        {
            InitializeComponent();

            _serviceProvider = serviceProvider;
            _logger = logger;
            // Navigation service will be set later by DI registration
            // _navigationService = _serviceProvider.GetRequiredService<INavigationService>(); // REMOVED - causes circular dependency

            // Wire up navigation button click events
            HomePageButton.Click += HomePageButton_Click;
            SimulationPageButton.Click += SimulationPageButton_Click;
            CalibrationPageButton.Click += CalibrationPageButton_Click;
            MailPageButton.Click += MailPageButton_Click;
            SettingsPageButton.Click += SettingsPageButton_Click;
            ReportingPageButton.Click += ReportingPageButton_Click;

            // Navigate to home page on load
            Load += MainForm_Load;
        }

        /// <summary>
        /// Sets the navigation service. Used for two-phase initialization in DI.
        /// </summary>
        public void SetNavigationService(INavigationService navigationService)
        {
            _navigationService = navigationService ?? throw new ArgumentNullException(nameof(navigationService));
        }

        private void MainForm_Load(object? sender, EventArgs e)
        {
            // Navigate to home page by default
            if (_navigationService != null)
            {
                NavigateWithHighlight(HomePageButton, () => _navigationService.NavigateTo<HomePageControl>());
            }
        }

        private void HomePageButton_Click(object? sender, EventArgs e)
        {
            NavigateWithHighlight(HomePageButton, () => _navigationService!.NavigateTo<HomePageControl>());
        }

        private void SimulationPageButton_Click(object? sender, EventArgs e)
        {
            NavigateWithHighlight(SimulationPageButton, () => _navigationService!.NavigateTo<SimulationPageControl>());
        }

        private void CalibrationPageButton_Click(object? sender, EventArgs e)
        {
            NavigateWithHighlight(CalibrationPageButton, () => _navigationService!.NavigateTo<CalibrationPageControl>());
        }

        private void MailPageButton_Click(object? sender, EventArgs e)
        {
            NavigateWithHighlight(MailPageButton, () => _navigationService!.NavigateTo<MailPageControl>());
        }

        private void ReportingPageButton_Click(object? sender, EventArgs e)
        {
            NavigateWithHighlight(ReportingPageButton, () => _navigationService!.NavigateTo<ReportingPageControl>());
        }

        private void SettingsPageButton_Click(object? sender, EventArgs e)
        {
            NavigateWithHighlight(SettingsPageButton, () => _navigationService!.NavigateTo<SettingsPageControl>());
        }

        /// <summary>
        /// Navigates to a page and highlights the active navigation button.
        /// </summary>
        private void NavigateWithHighlight(NavigationBarButton button, Action navigateAction)
        {
            // Reset previous active button
            if (_activeButton != null)
            {
                _activeButton.IsActive = false;
            }

            // Set new active button
            _activeButton = button;
            _activeButton.IsActive = true;

            // Perform navigation
            navigateAction();
        }
    }
}
