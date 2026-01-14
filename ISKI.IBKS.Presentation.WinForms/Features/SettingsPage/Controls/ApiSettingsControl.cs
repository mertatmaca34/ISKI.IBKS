using System.Text.Json;

namespace ISKI.IBKS.Presentation.WinForms.Features.SettingsPage.Controls
{
    public partial class ApiSettingsControl : UserControl
    {
        private TextBox TextBoxSaisUrl;
        private TextBox TextBoxSaisUsername;
        private TextBox TextBoxSaisPassword;
        private Button ButtonSaveSais;
        private GroupBox groupBoxSaisApi;
        private Label LabelSaisUrl;
        private Label LabelSaisUsername;
        private Label LabelSaisPassword;

        private readonly string _configPath;

        public ApiSettingsControl()
        {
            InitializeComponent();
            _configPath = Path.Combine(AppContext.BaseDirectory, "Configuration");
            Load += ApiSettingsControl_Load;
        }

        private void InitializeComponent()
        {
            groupBoxSaisApi = new GroupBox();
            LabelSaisUrl = new Label();
            TextBoxSaisUrl = new TextBox();
            LabelSaisUsername = new Label();
            TextBoxSaisUsername = new TextBox();
            LabelSaisPassword = new Label();
            TextBoxSaisPassword = new TextBox();
            ButtonSaveSais = new Button();

            groupBoxSaisApi.SuspendLayout();
            SuspendLayout();

            // groupBoxSaisApi
            groupBoxSaisApi.Controls.Add(LabelSaisUrl);
            groupBoxSaisApi.Controls.Add(TextBoxSaisUrl);
            groupBoxSaisApi.Controls.Add(LabelSaisUsername);
            groupBoxSaisApi.Controls.Add(TextBoxSaisUsername);
            groupBoxSaisApi.Controls.Add(LabelSaisPassword);
            groupBoxSaisApi.Controls.Add(TextBoxSaisPassword);
            groupBoxSaisApi.Controls.Add(ButtonSaveSais);
            groupBoxSaisApi.Dock = DockStyle.Fill;
            groupBoxSaisApi.Font = new Font("Arial", 9F, FontStyle.Bold, GraphicsUnit.Point);
            groupBoxSaisApi.Location = new Point(0, 0);
            groupBoxSaisApi.Name = "groupBoxSaisApi";
            groupBoxSaisApi.Size = new Size(600, 400);
            groupBoxSaisApi.TabIndex = 0;
            groupBoxSaisApi.TabStop = false;
            groupBoxSaisApi.Text = "SAIS API Bağlantı Ayarları";

            LabelSaisUrl.Location = new Point(20, 35);
            LabelSaisUrl.Text = "API URL:";
            LabelSaisUrl.AutoSize = true;

            TextBoxSaisUrl.Location = new Point(120, 32);
            TextBoxSaisUrl.Size = new Size(400, 23);
            TextBoxSaisUrl.Text = "https://entegrationsais.csb.gov.tr/";

            LabelSaisUsername.Location = new Point(20, 70);
            LabelSaisUsername.Text = "Kullanıcı Adı:";
            LabelSaisUsername.AutoSize = true;

            TextBoxSaisUsername.Location = new Point(120, 67);
            TextBoxSaisUsername.Size = new Size(200, 23);

            LabelSaisPassword.Location = new Point(20, 105);
            LabelSaisPassword.Text = "Şifre:";
            LabelSaisPassword.AutoSize = true;

            TextBoxSaisPassword.Location = new Point(120, 102);
            TextBoxSaisPassword.Size = new Size(200, 23);
            TextBoxSaisPassword.PasswordChar = '*';

            ButtonSaveSais.Location = new Point(120, 150);
            ButtonSaveSais.Size = new Size(200, 40);
            ButtonSaveSais.Text = "KAYDET";
            ButtonSaveSais.BackColor = Color.FromArgb(0, 131, 200);
            ButtonSaveSais.ForeColor = Color.White;
            ButtonSaveSais.FlatStyle = FlatStyle.Flat;
            ButtonSaveSais.Click += ButtonSaveSais_Click;

            Controls.Add(groupBoxSaisApi);
            Size = new Size(600, 400);
            groupBoxSaisApi.ResumeLayout(false);
            groupBoxSaisApi.PerformLayout();
            ResumeLayout(false);
        }

        private void ApiSettingsControl_Load(object? sender, EventArgs e)
        {
            LoadSaisSettings();
        }

        private void LoadSaisSettings()
        {
            try
            {
                var saisPath = Path.Combine(_configPath, "sais.json");
                if (File.Exists(saisPath))
                {
                    var json = File.ReadAllText(saisPath);
                    var doc = JsonDocument.Parse(json);
                    
                    if (doc.RootElement.TryGetProperty("SaisApi", out var sais))
                    {
                        TextBoxSaisUrl.Text = sais.GetProperty("BaseUrl").GetString() ?? "https://entegrationsais.csb.gov.tr/";
                        TextBoxSaisUsername.Text = sais.GetProperty("Username").GetString() ?? "";
                        TextBoxSaisPassword.Text = sais.GetProperty("Password").GetString() ?? "";
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"SAIS ayarları yüklenirken hata: {ex.Message}", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void ButtonSaveSais_Click(object? sender, EventArgs e)
        {
            try
            {
                var settings = new
                {
                    SaisApi = new
                    {
                        BaseUrl = TextBoxSaisUrl.Text.Trim(),
                        Username = TextBoxSaisUsername.Text.Trim(),
                        Password = TextBoxSaisPassword.Text
                    }
                };

                var json = JsonSerializer.Serialize(settings, new JsonSerializerOptions { WriteIndented = true });
                Directory.CreateDirectory(_configPath);
                File.WriteAllText(Path.Combine(_configPath, "sais.json"), json);

                MessageBox.Show("SAIS API ayarları kaydedildi.", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Kayıt hatası: {ex.Message}", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
