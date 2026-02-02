using ISKI.IBKS.Presentation.WinForms.Common.Navigation;
using ISKI.IBKS.Shared.Localization;
using ISKI.IBKS.Presentation.WinForms.Features.Main.Controls;
using Microsoft.Extensions.Logging;
using CalibrationPageControl = ISKI.IBKS.Presentation.WinForms.Features.CalibrationPage.View.CalibrationPage;
using HomePageControl = ISKI.IBKS.Presentation.WinForms.Features.HomePage.View.HomePage;
using MailPageControl = ISKI.IBKS.Presentation.WinForms.Features.MailPage.MailMain.View.MailPage;
using ReportingPageControl = ISKI.IBKS.Presentation.WinForms.Features.ReportingPage.View.ReportingPage;
using SettingsPageControl = ISKI.IBKS.Presentation.WinForms.Features.SettingsPage.SettingsMain.View.SettingsPage;
using SimulationPageControl = ISKI.IBKS.Presentation.WinForms.Features.SimulationPage.View.SimulationPage;

namespace ISKI.IBKS.Presentation.WinForms.Features.Main.View
{
    public partial class MainForm : Form, IMainFormView
    {
        private readonly IServiceProvider _serviceProvider;
        private readonly ILogger<MainForm> _logger;
        private INavigationService? _navigationService;
        private NavigationBarButton? _activeButton;

        public Panel ContentPanel => panel1;

        public MainForm(IServiceProvider serviceProvider, ILogger<MainForm> logger)
        {
            InitializeComponent();

            _serviceProvider = serviceProvider;
            _logger = logger;

            HomePageButton.Click += HomePageButton_Click;
            SimulationPageButton.Click += SimulationPageButton_Click;
            CalibrationPageButton.Click += CalibrationPageButton_Click;
            MailPageButton.Click += MailPageButton_Click;
            SettingsPageButton.Click += SettingsPageButton_Click;
            ReportingPageButton.Click += ReportingPageButton_Click;

            Load += MainForm_Load;
            InitializeLocalization();
        }

        private void InitializeLocalization()
        {
            this.Text = Strings.App_Title;
            HomePageButton.Text = Strings.Nav_HomePage;
            SimulationPageButton.Text = Strings.Nav_Simulation;
            CalibrationPageButton.Text = Strings.Nav_Calibration;
            MailPageButton.Text = Strings.Nav_Mail;
            ReportingPageButton.Text = Strings.Nav_Reporting;
            SettingsPageButton.Text = Strings.Nav_Settings;
            ModeSwitcherButton.Text = Strings.Nav_NightMode;
        }

        public void SetNavigationService(INavigationService navigationService)
        {
            _navigationService = navigationService ?? throw new ArgumentNullException(nameof(navigationService));
        }

        private void MainForm_Load(object? sender, EventArgs e)
        {
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

        private void NavigateWithHighlight(NavigationBarButton button, Action navigateAction)
        {
            if (_activeButton != null)
            {
                _activeButton.IsActive = false;
            }

            _activeButton = button;
            _activeButton.IsActive = true;

            navigateAction();
        }
    }
}

