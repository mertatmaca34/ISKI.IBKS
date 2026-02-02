using ISKI.IBKS.Presentation.WinForms.Features.SettingsPage.StationSettings.Presenter;
using ISKI.IBKS.Shared.Localization;
using Microsoft.Extensions.DependencyInjection;

namespace ISKI.IBKS.Presentation.WinForms.Features.SettingsPage.StationSettings.View;

    public partial class StationSettingsPage : UserControl, IStationSettingsPageView
    {
        public event EventHandler SaveRequested;

        public string StationName
        {
            get => StationSettingsControlStationName.AyarDegeri;
            set => StationSettingsControlStationName.AyarDegeri = value;
        }

        public string StationId
        {
            get => StationSettingsControlStationId.AyarDegeri;
            set => StationSettingsControlStationId.AyarDegeri = value;
        }

        public StationSettingsPage(IServiceProvider serviceProvider)
        {
            InitializeComponent();
            InitializeLocalization();
            ButtonSave.Click += (s, e) => SaveRequested?.Invoke(this, EventArgs.Empty);

            ActivatorUtilities.CreateInstance<StationSettingsPresenter>(serviceProvider, this);
        }

        private void InitializeLocalization()
        {
            titleBarControl1.TitleBarText = Strings.Settings_Station;
            StationSettingsControlStationName.AyarAdi = Strings.Station_Name;
            StationSettingsControlStationId.AyarAdi = Strings.Station_Id;
            ButtonSave.Text = Strings.Common_Save;
        }

        public void ShowInfo(string message) => MessageBox.Show(message, Strings.Common_Information, MessageBoxButtons.OK, MessageBoxIcon.Information);
        public void ShowError(string message) => MessageBox.Show(message, Strings.Common_Error, MessageBoxButtons.OK, MessageBoxIcon.Error);
    }

