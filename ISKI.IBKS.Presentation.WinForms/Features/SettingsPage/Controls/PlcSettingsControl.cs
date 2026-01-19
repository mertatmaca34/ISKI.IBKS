using System.Net.NetworkInformation;
using System.Text.Json;

namespace ISKI.IBKS.Presentation.WinForms.Features.SettingsPage.Controls
{
    public partial class PlcSettingsControl : UserControl
    {
        private TextBox TextBoxPlcIp = null!;
        private NumericUpDown NumericUpDownRack = null!;
        private NumericUpDown NumericUpDownSlot = null!;
        private Button ButtonTestPlc = null!;
        private Label LabelPlcStatus = null!;
        private Button ButtonSavePlc = null!;
        private Label LabelPlcIp = null!;
        private Label LabelRack = null!;
        private Label LabelSlot = null!;
        private GroupBox groupBoxPlcConnection = null!;

        private readonly string _configPath;

        public PlcSettingsControl()
        {
            InitializeComponent();
            _configPath = Path.Combine(AppContext.BaseDirectory, "Configuration");
            Load += PlcSettingsControl_Load;
        }

        private void InitializeComponent()
        {
            groupBoxPlcConnection = new GroupBox();
            LabelPlcIp = new Label();
            TextBoxPlcIp = new TextBox();
            LabelRack = new Label();
            NumericUpDownRack = new NumericUpDown();
            LabelSlot = new Label();
            NumericUpDownSlot = new NumericUpDown();
            ButtonTestPlc = new Button();
            LabelPlcStatus = new Label();
            ButtonSavePlc = new Button();

            ((System.ComponentModel.ISupportInitialize)NumericUpDownRack).BeginInit();
            ((System.ComponentModel.ISupportInitialize)NumericUpDownSlot).BeginInit();
            groupBoxPlcConnection.SuspendLayout();
            SuspendLayout();

            // groupBoxPlcConnection
            groupBoxPlcConnection.Controls.Add(LabelPlcIp);
            groupBoxPlcConnection.Controls.Add(TextBoxPlcIp);
            groupBoxPlcConnection.Controls.Add(LabelRack);
            groupBoxPlcConnection.Controls.Add(NumericUpDownRack);
            groupBoxPlcConnection.Controls.Add(LabelSlot);
            groupBoxPlcConnection.Controls.Add(NumericUpDownSlot);
            groupBoxPlcConnection.Controls.Add(ButtonTestPlc);
            groupBoxPlcConnection.Controls.Add(LabelPlcStatus);
            groupBoxPlcConnection.Controls.Add(ButtonSavePlc);
            groupBoxPlcConnection.Dock = DockStyle.Fill;
            groupBoxPlcConnection.Font = new Font("Arial", 9F, FontStyle.Bold, GraphicsUnit.Point);
            groupBoxPlcConnection.Location = new Point(0, 0);
            groupBoxPlcConnection.Name = "groupBoxPlcConnection";
            groupBoxPlcConnection.Size = new Size(600, 400);
            groupBoxPlcConnection.TabIndex = 0;
            groupBoxPlcConnection.TabStop = false;
            groupBoxPlcConnection.Text = "PLC Bağlantı Ayarları";

            // UI Layout (Reuse from previous version)
            LabelPlcIp.Location = new Point(20, 35);
            LabelPlcIp.Text = "IP Adresi:";
            LabelPlcIp.AutoSize = true;

            TextBoxPlcIp.Location = new Point(120, 32);
            TextBoxPlcIp.Size = new Size(200, 23);
            TextBoxPlcIp.Text = "10.33.3.253";

            LabelRack.Location = new Point(20, 70);
            LabelRack.Text = "Rack:";
            LabelRack.AutoSize = true;

            NumericUpDownRack.Location = new Point(120, 67);
            NumericUpDownRack.Size = new Size(80, 23);

            LabelSlot.Location = new Point(220, 70);
            LabelSlot.Text = "Slot:";
            LabelSlot.AutoSize = true;

            NumericUpDownSlot.Location = new Point(260, 67);
            NumericUpDownSlot.Size = new Size(60, 23);
            NumericUpDownSlot.Value = 1;

            ButtonTestPlc.Location = new Point(120, 110);
            ButtonTestPlc.Size = new Size(150, 35);
            ButtonTestPlc.Text = "BAĞLANTIYI TEST ET";
            ButtonTestPlc.BackColor = Color.FromArgb(100, 150, 100);
            ButtonTestPlc.ForeColor = Color.White;
            ButtonTestPlc.FlatStyle = FlatStyle.Flat;
            ButtonTestPlc.Click += ButtonTestPlc_Click;

            LabelPlcStatus.Location = new Point(280, 120);
            LabelPlcStatus.AutoSize = true;

            ButtonSavePlc.Location = new Point(120, 160);
            ButtonSavePlc.Size = new Size(200, 40);
            ButtonSavePlc.Text = "KAYDET";
            ButtonSavePlc.BackColor = Color.FromArgb(0, 131, 200);
            ButtonSavePlc.ForeColor = Color.White;
            ButtonSavePlc.FlatStyle = FlatStyle.Flat;
            ButtonSavePlc.Click += ButtonSavePlc_Click;

            Controls.Add(groupBoxPlcConnection);
            Size = new Size(600, 400);
            ((System.ComponentModel.ISupportInitialize)NumericUpDownRack).EndInit();
            ((System.ComponentModel.ISupportInitialize)NumericUpDownSlot).EndInit();
            groupBoxPlcConnection.ResumeLayout(false);
            groupBoxPlcConnection.PerformLayout();
            ResumeLayout(false);
        }

        private void PlcSettingsControl_Load(object? sender, EventArgs e)
        {
            LoadPlcSettings();
        }

        private void LoadPlcSettings()
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
                        if (plc.TryGetProperty("Connection", out var conn))
                        {
                            TextBoxPlcIp.Text = conn.GetProperty("IpAddress").GetString() ?? "10.33.3.253";
                            NumericUpDownRack.Value = conn.GetProperty("Rack").GetInt32();
                            NumericUpDownSlot.Value = conn.GetProperty("Slot").GetInt32();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"PLC ayarları yüklenirken hata: {ex.Message}", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private async void ButtonTestPlc_Click(object? sender, EventArgs e)
        {
            var ip = TextBoxPlcIp.Text.Trim();
            if (string.IsNullOrEmpty(ip))
            {
                MessageBox.Show("IP adresi giriniz.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            ButtonTestPlc.Enabled = false;
            LabelPlcStatus.Text = "Test ediliyor...";
            LabelPlcStatus.ForeColor = Color.Orange;

            try
            {
                using var ping = new Ping();
                var reply = await ping.SendPingAsync(ip, 3000);

                if (reply.Status == IPStatus.Success)
                {
                    LabelPlcStatus.Text = $"✓ Bağlantı başarılı ({reply.RoundtripTime}ms)";
                    LabelPlcStatus.ForeColor = Color.Green;
                }
                else
                {
                    LabelPlcStatus.Text = $"✗ Bağlantı başarısız: {reply.Status}";
                    LabelPlcStatus.ForeColor = Color.Red;
                }
            }
            catch (Exception ex)
            {
                LabelPlcStatus.Text = $"✗ Hata: {ex.Message}";
                LabelPlcStatus.ForeColor = Color.Red;
            }
            finally
            {
                ButtonTestPlc.Enabled = true;
            }
        }

        private void ButtonSavePlc_Click(object? sender, EventArgs e)
        {
            try
            {
                // Preserve existing Station settings if any
                string stationId = "";
                var plcPath = Path.Combine(_configPath, "plc.json");
                
                if (File.Exists(plcPath))
                {
                    var existingJson = File.ReadAllText(plcPath);
                    var doc = JsonDocument.Parse(existingJson);
                    if (doc.RootElement.TryGetProperty("PlcSettings", out var existingPlc) && 
                        existingPlc.TryGetProperty("Station", out var st) && 
                        st.TryGetProperty("StationId", out var sid))
                    {
                        stationId = sid.GetString() ?? "";
                    }
                }

                var settings = new
                {
                    PlcSettings = new
                    {
                        Connection = new
                        {
                            IpAddress = TextBoxPlcIp.Text.Trim(),
                            Rack = (int)NumericUpDownRack.Value,
                            Slot = (int)NumericUpDownSlot.Value
                        },
                        Station = new
                        {
                            StationId = stationId
                        }
                    }
                };

                var json = JsonSerializer.Serialize(settings, new JsonSerializerOptions { WriteIndented = true });
                Directory.CreateDirectory(_configPath);
                File.WriteAllText(plcPath, json);

                MessageBox.Show("PLC ayarları kaydedildi.", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Kayıt hatası: {ex.Message}", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
