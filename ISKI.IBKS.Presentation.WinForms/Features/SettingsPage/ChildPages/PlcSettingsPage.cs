using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using ISKI.IBKS.Domain.Entities;
using ISKI.IBKS.Persistence.Contexts;

namespace ISKI.IBKS.Presentation.WinForms.Features.SettingsPage.ChildPages
{
    public partial class PlcSettingsPage : UserControl
    {
        private readonly IServiceScopeFactory _scopeFactory;

        private static readonly string[] AvailableSensors = new[]
        {
            // Analog Sensörler
            "TesisDebi", "OlcumCihaziAkisHizi", "Ph", "Iletkenlik", "CozunmusOksijen",
            "Koi", "Akm", "KabinSicakligi",
            // Opsiyonel
            "DesarjDebi", "HariciDebi", "HariciDebi2"
        };

        public PlcSettingsPage(IServiceScopeFactory scopeFactory)
        {
            InitializeComponent();
            _scopeFactory = scopeFactory;
            Load += PlcSettingsPage_Load;
            ButtonSave.Click += ButtonSave_Click;
            PopulateSensorsList();
        }

        public PlcSettingsPage()
        {
            InitializeComponent();
            _scopeFactory = null!;
        }

        private void PopulateSensorsList()
        {
            FlowLayoutPanelSensors.Controls.Clear();
            foreach (var sensor in AvailableSensors)
            {
                var cb = CreateSensorCheckBox(sensor);
                FlowLayoutPanelSensors.Controls.Add(cb);
            }
        }

        private CheckBox CreateSensorCheckBox(string sensorName)
        {
            var cb = new CheckBox
            {
                Text = GetFriendlyName(sensorName),
                Tag = sensorName,
                AutoSize = false,
                Size = new Size(180, 45),
                Appearance = Appearance.Button,
                FlatStyle = FlatStyle.Flat,
                TextAlign = ContentAlignment.MiddleCenter,
                Font = new Font("Segoe UI", 10F, FontStyle.Bold),
                Margin = new Padding(5),
                BackColor = Color.FromArgb(245, 245, 245),
                ForeColor = Color.DimGray,
                Cursor = Cursors.Hand
            };

            cb.FlatAppearance.BorderSize = 1;
            cb.FlatAppearance.BorderColor = Color.FromArgb(220, 220, 220);
            cb.FlatAppearance.CheckedBackColor = Color.FromArgb(0, 131, 200);
            cb.FlatAppearance.MouseOverBackColor = Color.FromArgb(230, 230, 230);

            cb.CheckedChanged += (s, e) =>
            {
                if (cb.Checked)
                {
                    cb.ForeColor = Color.White;
                    cb.FlatAppearance.BorderColor = Color.FromArgb(0, 131, 200);
                }
                else
                {
                    cb.ForeColor = Color.DimGray;
                    cb.FlatAppearance.BorderColor = Color.FromArgb(220, 220, 220);
                }
            };

            return cb;
        }

        private string GetFriendlyName(string sensorKey)
        {
            return sensorKey switch
            {
                "TesisDebi" => "Tesis Debisi",
                "OlcumCihaziAkisHizi" => "Akış Hızı",
                "Ph" => "pH",
                "Iletkenlik" => "İletkenlik",
                "CozunmusOksijen" => "Çözünmüş Oksijen",
                "Koi" => "KOİ",
                "Akm" => "AKM",
                "KabinSicakligi" => "Kabin Sıcaklığı",
                "Pompa1Hz" => "Pompa 1 Hz",
                "Pompa2Hz" => "Pompa 2 Hz",
                "DesarjDebi" => "Deşarj Debisi",
                "HariciDebi" => "Harici Debi 1",
                "HariciDebi2" => "Harici Debi 2",
                _ => sensorKey
            };
        }

        private async void PlcSettingsPage_Load(object? sender, EventArgs e)
        {
            if (_scopeFactory == null) return;
            await LoadSettingsAsync();
        }

        private async Task LoadSettingsAsync()
        {
             try
             {
                 using var scope = _scopeFactory.CreateScope();
                 var dbContext = scope.ServiceProvider.GetRequiredService<IbksDbContext>();
                 var settings = await dbContext.StationSettings.FirstOrDefaultAsync();

                 if (settings != null)
                 {
                     PlcSettingsControlIp.AyarDegeri = settings.PlcIpAddress;
                     
                     var selectedSensors = settings.GetSelectedSensors();
                     foreach (Control control in FlowLayoutPanelSensors.Controls)
                     {
                         if (control is CheckBox cb && cb.Tag is string sensorKey)
                         {
                             if (selectedSensors.Contains(sensorKey))
                             {
                                 cb.Checked = true;
                             }
                         }
                     }
                 }
             }
             catch (Exception ex)
             {
                 MessageBox.Show($"Ayarlar yüklenirken hata: {ex.Message}");
             }
        }

        private async void ButtonSave_Click(object? sender, EventArgs e)
        {
            if (_scopeFactory == null) return;

            try
            {
                var selectedSensors = new List<string>();
                foreach (Control control in FlowLayoutPanelSensors.Controls)
                {
                    if (control is CheckBox cb && cb.Checked && cb.Tag is string sensorKey)
                    {
                        selectedSensors.Add(sensorKey);
                    }
                }

                using var scope = _scopeFactory.CreateScope();
                var dbContext = scope.ServiceProvider.GetRequiredService<IbksDbContext>();
                var settings = await dbContext.StationSettings.FirstOrDefaultAsync();

                if (settings == null)
                {
                    settings = new StationSettings(Guid.NewGuid(), "Varsayılan İstasyon");
                    dbContext.StationSettings.Add(settings);
                }

                int currentRack = settings.PlcRack;
                int currentSlot = settings.PlcSlot;
                
                settings.UpdatePlcSettings(PlcSettingsControlIp.AyarDegeri.Trim(), currentRack, currentSlot);
                settings.UpdateSelectedSensors(selectedSensors);

                await dbContext.SaveChangesAsync();

                // Save to plc.json as well
                await SaveToPlcJsonAsync(settings.StationId, PlcSettingsControlIp.AyarDegeri.Trim(), currentRack, currentSlot, selectedSensors);

                MessageBox.Show("PLC bağlantı ayarları ve sensör seçimleri kaydedildi.");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Kaydetme hatası: {ex.Message}");
            }
        }

        private async Task SaveToPlcJsonAsync(Guid stationId, string ip, int rack, int slot, List<string> selectedSensors)
        {
            var configDir = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Configuration");
            if (!Directory.Exists(configDir)) Directory.CreateDirectory(configDir);
            
            var plcPath = Path.Combine(configDir, "plc.json");

            if (File.Exists(plcPath))
            {
                try
                {
                    var existingJson = await File.ReadAllTextAsync(plcPath);
                    using var doc = JsonDocument.Parse(existingJson);
                    var root = doc.RootElement;
                    
                    var plcDict = new Dictionary<string, object>();
                    
                    if (root.TryGetProperty("Plc", out var plcElem))
                    {
                        var plcContent = JsonSerializer.Deserialize<Dictionary<string, object>>(plcElem.GetRawText());
                        if (plcContent != null)
                        {
                            if (plcContent.TryGetValue("Station", out var stationObj))
                            {
                                var stationDict = JsonSerializer.Deserialize<Dictionary<string, object>>(stationObj.ToString()!);
                                if (stationDict != null)
                                {
                                    stationDict["IpAddress"] = ip;
                                    stationDict["Rack"] = rack;
                                    stationDict["Slot"] = slot;
                                    stationDict["StationId"] = stationId;
                                    plcContent["Station"] = stationDict;
                                }
                            }
                            
                            plcContent["SelectedSensors"] = selectedSensors;
                            plcDict["Plc"] = plcContent;
                        }
                    }
                    
                    await File.WriteAllTextAsync(
                        plcPath,
                        JsonSerializer.Serialize(plcDict, new JsonSerializerOptions { WriteIndented = true }));
                }
                catch (Exception)
                {
                    // Fallback handled in SetupWizardForm, keeping it minimal here or replicate more robustly if needed
                }
            }
        }
    }
}
