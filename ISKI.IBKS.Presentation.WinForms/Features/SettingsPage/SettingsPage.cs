using ISKI.IBKS.Presentation.WinForms.Features.SettingsPage.ChildPages;
using Microsoft.Extensions.DependencyInjection;

namespace ISKI.IBKS.Presentation.WinForms.Features.SettingsPage
{
    public partial class SettingsPage : UserControl
    {
        private readonly IServiceProvider _serviceProvider;

        public SettingsPage(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
            InitializeComponent();
            InitializeNavigation();
            
            // Load default view
            LoadControl<PlcSettingsPage>(); 
        }

        private void InitializeNavigation()
        {
            ButtonStationSettings.Click += (s, e) => LoadControl<StationSettingsPage>();
            ButtonApiSettings.Click += (s, e) => LoadControl<ApiSettingsPage>();
            ButtonPlcSettings.Click += (s, e) => LoadControl<PlcSettingsPage>();
            ButtonCalibrationSettings.Click += (s, e) => LoadControl<CalibrationSettingsPage>();
        }

        private void LoadControl<T>() where T : UserControl
        {
            PanelContent.Controls.Clear();
            
            // Resolve the control from DI or create it
            // Assuming controls might not be registered as pages, we can use ActivatorUtilities
            var control = _serviceProvider.GetService<T>() ?? ActivatorUtilities.CreateInstance<T>(_serviceProvider);
            
            control.Dock = DockStyle.Fill;
            PanelContent.Controls.Add(control);
        }
    }
}
