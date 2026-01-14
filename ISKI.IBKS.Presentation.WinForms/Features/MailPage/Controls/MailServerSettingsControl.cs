using System.Text.Json;

namespace ISKI.IBKS.Presentation.WinForms.Features.MailPage.Controls
{
    public partial class MailServerSettingsControl : UserControl
    {
        private TextBox TextBoxSmtpHost;
        private NumericUpDown NumericUpDownSmtpPort;
        private TextBox TextBoxSmtpUsername;
        private TextBox TextBoxSmtpPassword;
        private CheckBox CheckBoxUseSsl;
        private TextBox TextBoxFromAddress;
        private Button ButtonTestMail;
        private Label LabelMailStatus;
        private Button ButtonSaveMail;
        private GroupBox groupBoxMailServer;
        
        // Labels
        private Label LabelSmtpHost;
        private Label LabelSmtpPort;
        private Label LabelSmtpUsername;
        private Label LabelSmtpPassword;
        private Label LabelFromAddress; 

        private readonly string _configPath;

        public MailServerSettingsControl()
        {
            InitializeComponent();
            _configPath = Path.Combine(AppContext.BaseDirectory, "Configuration");
            Load += MailServerSettingsControl_Load;
        }

        private void InitializeComponent()
        {
            groupBoxMailServer = new GroupBox();
            LabelSmtpHost = new Label();
            TextBoxSmtpHost = new TextBox();
            LabelSmtpPort = new Label();
            NumericUpDownSmtpPort = new NumericUpDown();
            LabelSmtpUsername = new Label();
            TextBoxSmtpUsername = new TextBox();
            LabelSmtpPassword = new Label();
            TextBoxSmtpPassword = new TextBox();
            CheckBoxUseSsl = new CheckBox();
            LabelFromAddress = new Label();
            TextBoxFromAddress = new TextBox();
            ButtonTestMail = new Button();
            LabelMailStatus = new Label();
            ButtonSaveMail = new Button();

            ((System.ComponentModel.ISupportInitialize)NumericUpDownSmtpPort).BeginInit();
            groupBoxMailServer.SuspendLayout();
            SuspendLayout();

            // groupBoxMailServer
            groupBoxMailServer.Controls.Add(LabelSmtpHost);
            groupBoxMailServer.Controls.Add(TextBoxSmtpHost);
            groupBoxMailServer.Controls.Add(LabelSmtpPort);
            groupBoxMailServer.Controls.Add(NumericUpDownSmtpPort);
            groupBoxMailServer.Controls.Add(LabelSmtpUsername);
            groupBoxMailServer.Controls.Add(TextBoxSmtpUsername);
            groupBoxMailServer.Controls.Add(LabelSmtpPassword);
            groupBoxMailServer.Controls.Add(TextBoxSmtpPassword);
            groupBoxMailServer.Controls.Add(CheckBoxUseSsl);
            groupBoxMailServer.Controls.Add(LabelFromAddress);
            groupBoxMailServer.Controls.Add(TextBoxFromAddress);
            groupBoxMailServer.Controls.Add(ButtonTestMail);
            groupBoxMailServer.Controls.Add(LabelMailStatus);
            groupBoxMailServer.Controls.Add(ButtonSaveMail);
            groupBoxMailServer.Dock = DockStyle.Fill;
            groupBoxMailServer.Font = new Font("Arial", 9F, FontStyle.Bold, GraphicsUnit.Point);
            groupBoxMailServer.Location = new Point(0, 0);
            groupBoxMailServer.Name = "groupBoxMailServer";
            groupBoxMailServer.Size = new Size(600, 400);
            groupBoxMailServer.TabIndex = 0;
            groupBoxMailServer.TabStop = false;
            groupBoxMailServer.Text = "SMTP Mail Sunucu Ayarları";

            LabelSmtpHost.Location = new Point(20, 35);
            LabelSmtpHost.Text = "SMTP Sunucu:";
            LabelSmtpHost.AutoSize = true;

            TextBoxSmtpHost.Location = new Point(140, 32);
            TextBoxSmtpHost.Size = new Size(250, 23);
            TextBoxSmtpHost.Text = "smtp.gmail.com";

            LabelSmtpPort.Location = new Point(20, 70);
            LabelSmtpPort.Text = "Port:";
            LabelSmtpPort.AutoSize = true;

            NumericUpDownSmtpPort.Location = new Point(140, 67);
            NumericUpDownSmtpPort.Maximum = 65535;
            NumericUpDownSmtpPort.Minimum = 1;
            NumericUpDownSmtpPort.Size = new Size(80, 23);
            NumericUpDownSmtpPort.Value = 587;

            LabelSmtpUsername.Location = new Point(20, 105);
            LabelSmtpUsername.Text = "Kullanıcı Adı:";
            LabelSmtpUsername.AutoSize = true;

            TextBoxSmtpUsername.Location = new Point(140, 102);
            TextBoxSmtpUsername.Size = new Size(250, 23);

            LabelSmtpPassword.Location = new Point(20, 140);
            LabelSmtpPassword.Text = "Şifre:";
            LabelSmtpPassword.AutoSize = true;

            TextBoxSmtpPassword.Location = new Point(140, 137);
            TextBoxSmtpPassword.Size = new Size(250, 23);
            TextBoxSmtpPassword.PasswordChar = '*';

            CheckBoxUseSsl.Location = new Point(140, 170);
            CheckBoxUseSsl.Text = "SSL Kullan";
            CheckBoxUseSsl.Checked = true;
            CheckBoxUseSsl.AutoSize = true;

            LabelFromAddress.Location = new Point(20, 205);
            LabelFromAddress.Text = "Gönderen Adresi:";
            LabelFromAddress.AutoSize = true;

            TextBoxFromAddress.Location = new Point(140, 202);
            TextBoxFromAddress.Size = new Size(250, 23);

            ButtonTestMail.Location = new Point(140, 240);
            ButtonTestMail.Size = new Size(150, 35);
            ButtonTestMail.Text = "TEST MAİLİ GÖNDER";
            ButtonTestMail.BackColor = Color.FromArgb(100, 150, 100);
            ButtonTestMail.ForeColor = Color.White;
            ButtonTestMail.FlatStyle = FlatStyle.Flat;
            ButtonTestMail.Click += ButtonTestMail_Click;

            LabelMailStatus.Location = new Point(300, 250);
            LabelMailStatus.AutoSize = true;

            ButtonSaveMail.Location = new Point(140, 290);
            ButtonSaveMail.Size = new Size(200, 40);
            ButtonSaveMail.Text = "KAYDET";
            ButtonSaveMail.BackColor = Color.FromArgb(0, 131, 200);
            ButtonSaveMail.ForeColor = Color.White;
            ButtonSaveMail.FlatStyle = FlatStyle.Flat;
            ButtonSaveMail.Click += ButtonSaveMail_Click;

            Controls.Add(groupBoxMailServer);
            Size = new Size(600, 400);
            ((System.ComponentModel.ISupportInitialize)NumericUpDownSmtpPort).EndInit();
            groupBoxMailServer.ResumeLayout(false);
            groupBoxMailServer.PerformLayout();
            ResumeLayout(false);
        }

        private void MailServerSettingsControl_Load(object? sender, EventArgs e)
        {
            LoadMailSettings();
        }

        private void LoadMailSettings()
        {
            try
            {
                var mailPath = Path.Combine(_configPath, "mail.json");
                if (File.Exists(mailPath))
                {
                    var json = File.ReadAllText(mailPath);
                    var doc = JsonDocument.Parse(json);
                    
                    if (doc.RootElement.TryGetProperty("MailSettings", out var mail))
                    {
                        TextBoxSmtpHost.Text = mail.GetProperty("SmtpHost").GetString() ?? "";
                        NumericUpDownSmtpPort.Value = mail.GetProperty("SmtpPort").GetInt32();
                        TextBoxSmtpUsername.Text = mail.GetProperty("Username").GetString() ?? "";
                        TextBoxSmtpPassword.Text = mail.GetProperty("Password").GetString() ?? "";
                        CheckBoxUseSsl.Checked = mail.GetProperty("UseSsl").GetBoolean();
                        TextBoxFromAddress.Text = mail.GetProperty("FromAddress").GetString() ?? "";
                    }
                }
            }
            catch
            {
                // Mail settings might not exist yet
            }
        }

        private async void ButtonTestMail_Click(object? sender, EventArgs e)
        {
             var testEmail = TextBoxFromAddress.Text.Trim();
            if (string.IsNullOrEmpty(testEmail))
            {
                MessageBox.Show("Gönderen e-posta adresi giriniz.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            ButtonTestMail.Enabled = false;
            LabelMailStatus.Text = "Test maili gönderiliyor...";
            LabelMailStatus.ForeColor = Color.Orange;

            try
            {
                // Simple SMTP test
                using var client = new System.Net.Mail.SmtpClient(TextBoxSmtpHost.Text.Trim(), (int)NumericUpDownSmtpPort.Value);
                client.EnableSsl = CheckBoxUseSsl.Checked;
                client.Credentials = new System.Net.NetworkCredential(TextBoxSmtpUsername.Text.Trim(), TextBoxSmtpPassword.Text);
                client.Timeout = 10000;

                var message = new System.Net.Mail.MailMessage(testEmail, testEmail, "IBKS Test Mail", "Bu bir test mailidir.");
                await client.SendMailAsync(message);

                LabelMailStatus.Text = "✓ Test maili gönderildi";
                LabelMailStatus.ForeColor = Color.Green;
            }
            catch (Exception ex)
            {
                LabelMailStatus.Text = $"✗ Hata: {ex.Message}";
                LabelMailStatus.ForeColor = Color.Red;
            }
            finally
            {
                ButtonTestMail.Enabled = true;
            }
        }

        private void ButtonSaveMail_Click(object? sender, EventArgs e)
        {
             try
            {
                var settings = new
                {
                    MailSettings = new
                    {
                        SmtpHost = TextBoxSmtpHost.Text.Trim(),
                        SmtpPort = (int)NumericUpDownSmtpPort.Value,
                        Username = TextBoxSmtpUsername.Text.Trim(),
                        Password = TextBoxSmtpPassword.Text,
                        UseSsl = CheckBoxUseSsl.Checked,
                        FromAddress = TextBoxFromAddress.Text.Trim()
                    }
                };

                var json = JsonSerializer.Serialize(settings, new JsonSerializerOptions { WriteIndented = true });
                Directory.CreateDirectory(_configPath);
                File.WriteAllText(Path.Combine(_configPath, "mail.json"), json);

                MessageBox.Show("Mail sunucu ayarları kaydedildi.", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Kayıt hatası: {ex.Message}", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
