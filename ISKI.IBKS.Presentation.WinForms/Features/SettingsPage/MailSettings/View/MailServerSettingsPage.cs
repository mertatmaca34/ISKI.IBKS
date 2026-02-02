using ISKI.IBKS.Presentation.WinForms.Features.SettingsPage.MailSettings.Presenter;
using ISKI.IBKS.Shared.Localization;
using Microsoft.Extensions.DependencyInjection;

namespace ISKI.IBKS.Presentation.WinForms.Features.SettingsPage.MailSettings.View;

    public partial class MailServerSettingsPage : UserControl, IMailServerSettingsPageView
    {
        public event EventHandler SaveRequested;
        public event EventHandler UseCredentialsChanged;

        public string Host { get => TextBoxHost.Text; set => TextBoxHost.Text = value; }
        public int Port { get => int.TryParse(TextBoxPort.Text, out int p) ? p : 587; set => TextBoxPort.Text = value.ToString(); }
        public string Username { get => TextBoxUsername.Text; set => TextBoxUsername.Text = value; }
        public string Password { get => TextBoxPassword.Text; set => TextBoxPassword.Text = value; }
        public bool UseSsl { get => CheckBoxSSL.Checked; set => CheckBoxSSL.Checked = value; }
        public string FromName { get => TextBoxFromName.Text; set => TextBoxFromName.Text = value; }
        public bool CredentialsEnabled
        {
            get => CheckBoxCredentials.Checked;
            set
            {
                if (CheckBoxCredentials.Checked != value)
                {
                    CheckBoxCredentials.Checked = value;
                }
                TextBoxUsername.Enabled = !value;
                TextBoxPassword.Enabled = !value;
            }
        }

        public MailServerSettingsPage(IServiceProvider serviceProvider)
        {
            InitializeComponent();
            InitializeLocalization();
            ButtonSave.Click += (s, e) => SaveRequested?.Invoke(this, EventArgs.Empty);
            CheckBoxCredentials.CheckedChanged += (s, e) => 
            {
                // Update UI state immediately
                TextBoxUsername.Enabled = !CheckBoxCredentials.Checked;
                TextBoxPassword.Enabled = !CheckBoxCredentials.Checked;
                UseCredentialsChanged?.Invoke(this, EventArgs.Empty);
            };

            ActivatorUtilities.CreateInstance<MailServerSettingsPresenter>(serviceProvider, this);
        }

        private void InitializeLocalization()
        {
            titleBarControl1.TitleBarText = Strings.Mail_ConfigTitle;
            label8.Text = Strings.Mail_SenderInfo;
            label1.Text = Strings.Mail_EnableSslQuestion;
            CheckBoxSSL.Text = Strings.Mail_UseSsl;
            label2.Text = "Host:"; // Standard
            label5.Text = Strings.Mail_Port;
            label4.Text = Strings.Common_Username_Label;
            label6.Text = Strings.Common_Password_Label;
            label7.Text = Strings.Mail_Auth;
            CheckBoxCredentials.Text = Strings.Mail_UseDefaultCredentials;
            ButtonSave.Text = Strings.Common_Save;
            
            TextBoxPort.PlaceholderText = "";
            TextBoxUsername.PlaceholderText = "";
            TextBoxPassword.PlaceholderText = "";
            TextBoxFromName.PlaceholderText = "";
        }

        public void ShowInfo(string message) => MessageBox.Show(message, Strings.Common_Information, MessageBoxButtons.OK, MessageBoxIcon.Information);
        public void ShowError(string message) => MessageBox.Show(message, Strings.Common_Error, MessageBoxButtons.OK, MessageBoxIcon.Error);
    }

