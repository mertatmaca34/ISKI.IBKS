using System.Text.Json;

namespace ISKI.IBKS.Presentation.WinForms.Features.SettingsPage.Controls
{
    public partial class StationSettingsControl : UserControl
    {
        private TextBox TextBoxStationId;
        private Label LabelStationId;
        private Button ButtonSave;
        private GroupBox groupBoxStation;

        private readonly string _configPath;

        public StationSettingsControl()
        {
            InitializeComponent();
            _configPath = Path.Combine(AppContext.BaseDirectory, "Configuration");
            Load += StationSettingsControl_Load;
        }

        private void InitializeComponent()
        {
            groupBoxStation = new GroupBox();
            LabelStationId = new Label();
            TextBoxStationId = new TextBox();
            ButtonSave = new Button();

            groupBoxStation.SuspendLayout();
            SuspendLayout();

            // groupBoxStation
            groupBoxStation.Controls.Add(LabelStationId);
            groupBoxStation.Controls.Add(TextBoxStationId);
            groupBoxStation.Controls.Add(ButtonSave);
            groupBoxStation.Dock = DockStyle.Fill;
            groupBoxStation.Font = new Font("Arial", 9F, FontStyle.Bold, GraphicsUnit.Point);
            groupBoxStation.Location = new Point(0, 0);
            groupBoxStation.Name = "groupBoxStation";
            groupBoxStation.Size = new Size(600, 400);
            groupBoxStation.TabIndex = 0;
            groupBoxStation.TabStop = false;
            groupBoxStation.Text = "İstasyon Ayarları";

            LabelStationId.Location = new Point(20, 35);
            LabelStationId.Text = "İstasyon ID:";
            LabelStationId.AutoSize = true;

            TextBoxStationId.Location = new Point(120, 32);
            TextBoxStationId.Size = new Size(300, 23);

            ButtonSave.Location = new Point(120, 80);
            ButtonSave.Size = new Size(200, 40);
            ButtonSave.Text = "KAYDET";
            ButtonSave.BackColor = Color.FromArgb(0, 131, 200);
            ButtonSave.ForeColor = Color.White;
            ButtonSave.FlatStyle = FlatStyle.Flat;
            ButtonSave.Click += ButtonSave_Click;

            Controls.Add(groupBoxStation);
            Size = new Size(600, 400);
            groupBoxStation.ResumeLayout(false);
            groupBoxStation.PerformLayout();
            ResumeLayout(false);
        }

        private void StationSettingsControl_Load(object? sender, EventArgs e)
        {
            LoadStationSettings();
        }

        private void LoadStationSettings()
        {
            try
            {
                var plcPath = Path.Combine(_configPath, "plc.json");
                if (File.Exists(plcPath))
                {
                    var json = File.ReadAllText(plcPath);
                    var doc = JsonDocument.Parse(json);
                    
                    if (doc.RootElement.TryGetProperty("PlcSettings", out var plc))
                    {
                        if (plc.TryGetProperty("Station", out var station))
                        {
                            TextBoxStationId.Text = station.GetProperty("StationId").GetString() ?? "";
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"İstasyon ayarları yüklenirken hata: {ex.Message}", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void ButtonSave_Click(object? sender, EventArgs e)
        {
            try
            {
                // Preserve Connection settings
                string ip = "10.33.3.253";
                int rack = 0;
                int slot = 1;

                var plcPath = Path.Combine(_configPath, "plc.json");
                if (File.Exists(plcPath))
                {
                    var existingJson = File.ReadAllText(plcPath);
                    var doc = JsonDocument.Parse(existingJson);
                    if (doc.RootElement.TryGetProperty("PlcSettings", out var existingPlc) && 
                        existingPlc.TryGetProperty("Connection", out var conn))
                    {
                        ip = conn.GetProperty("IpAddress").GetString() ?? ip;
                        if (conn.TryGetProperty("Rack", out var r)) rack = r.GetInt32();
                        if (conn.TryGetProperty("Slot", out var s)) slot = s.GetInt32();
                    }
                }

                var settings = new
                {
                    PlcSettings = new
                    {
                        Connection = new
                        {
                            IpAddress = ip,
                            Rack = rack,
                            Slot = slot
                        },
                        Station = new
                        {
                            StationId = TextBoxStationId.Text.Trim()
                        }
                    }
                };

                var json = JsonSerializer.Serialize(settings, new JsonSerializerOptions { WriteIndented = true });
                Directory.CreateDirectory(_configPath);
                File.WriteAllText(plcPath, json);

                MessageBox.Show("İstasyon ayarları kaydedildi.", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Kayıt hatası: {ex.Message}", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
