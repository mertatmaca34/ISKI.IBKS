using ISKI.IBKS.Presentation.WinForms.Features.SettingsPage.ApiSettings.Presenter;
using ISKI.IBKS.Shared.Localization;
using Microsoft.Extensions.DependencyInjection;

namespace ISKI.IBKS.Presentation.WinForms.Features.SettingsPage.ApiSettings.View;

public partial class ApiSettingsPage : UserControl, IApiSettingsPageView
{
        public event EventHandler SaveRequested;

        public string BaseUrl
        {
            get => SettingsControlApiUrl.AyarDegeri;
            set => SettingsControlApiUrl.AyarDegeri = value;
        }

        public string Username
        {
            get => SettingsControlUsername.AyarDegeri;
            set => SettingsControlUsername.AyarDegeri = value;
        }

        public string Password
        {
            get => SettingsControlPassword.AyarDegeri;
            set => SettingsControlPassword.AyarDegeri = value;
        }

        public ApiSettingsPage(IServiceProvider serviceProvider)
        {
            InitializeComponent();
            InitializeLocalization();
            ButtonSave.Click += (s, e) => SaveRequested?.Invoke(this, EventArgs.Empty);

            ActivatorUtilities.CreateInstance<ApiSettingsPresenter>(serviceProvider, this);
        }

        private void InitializeLocalization()
        {
            titleBarControl1.TitleBarText = Strings.Settings_Api;
            SettingsControlApiUrl.AyarAdi = "API URL:";
            titleBarControl2.TitleBarText = Strings.Settings_LoginInfo;
            SettingsControlUsername.AyarAdi = Strings.Common_Username_Label;
            SettingsControlPassword.AyarAdi = Strings.Common_Password_Label;
            ButtonSave.Text = Strings.Common_Save;
        }

        public void ShowInfo(string message) => MessageBox.Show(message, Strings.Common_Information, MessageBoxButtons.OK, MessageBoxIcon.Information);
        public void ShowError(string message) => MessageBox.Show(message, Strings.Common_Error, MessageBoxButtons.OK, MessageBoxIcon.Error);
    }

