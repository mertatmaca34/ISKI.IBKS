using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Text.Json;
using System.Text.Json.Nodes; // Added for JsonNode
using System.IO;

namespace ISKI.IBKS.Presentation.WinForms.Features.SettingsPage.ChildPages
{
    public partial class ApiSettingsPage : UserControl
    {
        private readonly string _configPath;

        public ApiSettingsPage()
        {
            InitializeComponent();
            _configPath = Path.Combine(AppContext.BaseDirectory, "Configuration");
            Load += ApiSettingsPage_Load;
            ButtonSave.Click += ButtonSave_Click;
        }

        private void ApiSettingsPage_Load(object? sender, EventArgs e)
        {
            LoadSettings();
        }

        private void LoadSettings()
        {
            try
            {
                var path = Path.Combine(_configPath, "sais.json");
                if (File.Exists(path))
                {
                    var json = File.ReadAllText(path);
                    var doc = JsonDocument.Parse(json);
                    // JSON key is "SAIS", not "SaisSettings"
                    if (doc.RootElement.TryGetProperty("SAIS", out var sais)) 
                    {
                        // Case-insensitive options or manual check
                        // JSON property is "UserName", code was "Username"
                        SettingsControlApiUrl.AyarDegeri = GetStringSafe(sais, "BaseUrl");
                        SettingsControlUsername.AyarDegeri = GetStringSafe(sais, "UserName"); 
                        SettingsControlPassword.AyarDegeri = GetStringSafe(sais, "Password");
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ayarlar yüklenirken hata: {ex.Message}");
            }
        }

        private string GetStringSafe(JsonElement element, string propertyName)
        {
            if (element.TryGetProperty(propertyName, out var prop))
            {
                return prop.GetString() ?? "";
            }
            // Fallback for case sensitivity or missing keys? 
            // Try "Username" if "UserName" failed, etc.
             if (propertyName == "UserName" && element.TryGetProperty("Username", out var prop2)) return prop2.GetString() ?? "";
             
            return "";
        }

        private void ButtonSave_Click(object? sender, EventArgs e)
        {
            try
            {
                var path = Path.Combine(_configPath, "sais.json");
                JsonNode rootNode;

                if (File.Exists(path))
                {
                    var jsonContent = File.ReadAllText(path);
                    rootNode = JsonNode.Parse(jsonContent) ?? new JsonObject();
                }
                else
                {
                    rootNode = new JsonObject();
                }

                // Ensure SAIS object exists
                if (rootNode["SAIS"] is not JsonObject saisObj)
                {
                    saisObj = new JsonObject();
                    rootNode["SAIS"] = saisObj;
                }

                saisObj["BaseUrl"] = SettingsControlApiUrl.AyarDegeri.Trim();
                saisObj["UserName"] = SettingsControlUsername.AyarDegeri.Trim();
                saisObj["Password"] = SettingsControlPassword.AyarDegeri.Trim();

                var options = new JsonSerializerOptions { WriteIndented = true };
                var json = rootNode.ToJsonString(options);
                
                Directory.CreateDirectory(_configPath);
                File.WriteAllText(path, json);

                MessageBox.Show("API ayarları kaydedildi.", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Kayıt hatası: {ex.Message}", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
