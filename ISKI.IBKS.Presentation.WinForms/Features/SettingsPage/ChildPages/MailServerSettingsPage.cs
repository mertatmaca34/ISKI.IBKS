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
using System.IO;

namespace ISKI.IBKS.Presentation.WinForms.Features.SettingsPage.ChildPages
{
    public partial class MailServerSettingsPage : UserControl
    {
        private readonly string _configPath;

        public MailServerSettingsPage()
        {
            InitializeComponent();
            _configPath = Path.Combine(AppContext.BaseDirectory, "Configuration");
            Load += MailServerSettingsPage_Load;
            ButtonSave.Click += ButtonSave_Click;
            CheckBoxCredentials.CheckedChanged += CheckBoxCredentials_CheckedChanged;
        }

        private void MailServerSettingsPage_Load(object? sender, EventArgs e)
        {
            LoadMailSettings();
        }

        private void CheckBoxCredentials_CheckedChanged(object? sender, EventArgs e)
        {
            var useDefault = CheckBoxCredentials.Checked;
            TextBoxUsername.Enabled = !useDefault;
            TextBoxPassword.Enabled = !useDefault;
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
                        if (mail.TryGetProperty("SmtpHost", out var host))
                            TextBoxHost.Text = host.GetString() ?? "";
                        if (mail.TryGetProperty("SmtpPort", out var port))
                            TextBoxPort.Text = port.GetString(); // Configuration reads as string usually, but json might be int
                            // Actually config file has strings in my artifact, but let's be safe
                        
                        // Handle potential int or string for Port to be safe
                        if (mail.TryGetProperty("SmtpPort", out var portProp))
                        {
                            if (portProp.ValueKind == JsonValueKind.Number)
                                TextBoxPort.Text = portProp.GetInt32().ToString();
                            else
                                TextBoxPort.Text = portProp.GetString() ?? "587";
                        }

                        if (mail.TryGetProperty("Username", out var userName))
                            TextBoxUsername.Text = userName.GetString() ?? "";
                        if (mail.TryGetProperty("Password", out var password))
                            TextBoxPassword.Text = password.GetString() ?? "";
                        
                        if (mail.TryGetProperty("UseSsl", out var useSsl))
                        {
                             if (useSsl.ValueKind == JsonValueKind.String)
                                CheckBoxSSL.Checked = bool.Parse(useSsl.GetString() ?? "true");
                             else
                                CheckBoxSSL.Checked = useSsl.GetBoolean();
                        }

                        if (mail.TryGetProperty("FromName", out var fromName))
                            TextBoxFromName.Text = fromName.GetString() ?? "";
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ayarlar yüklenirken hata: {ex.Message}");
            }
        }

        private void ButtonSave_Click(object? sender, EventArgs e)
        {
            try
            {
                // Port validation
                var portStr = TextBoxPort.Text.Trim();
                if (!int.TryParse(portStr, out int port))
                {
                    MessageBox.Show("Port numarası geçerli bir sayı olmalıdır.", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                var settings = new
                {
                    MailSettings = new
                    {
                        SmtpHost = TextBoxHost.Text.Trim(),
                        SmtpPort = portStr, // Save as string to match appsettings style commonly or int?
                                            // The SmtpAlarmMailService parses it: int.Parse(_configuration["..."])
                                            // JsonConfigurationProvider reads numbers as strings mostly fine, but let's save as string to be consistent with appsettings replacement
                        Username = TextBoxUsername.Text.Trim(),
                        Password = TextBoxPassword.Text, // No trim for password
                        UseSsl = CheckBoxSSL.Checked.ToString().ToLower(), // Save as string "true"/"false"
                        FromAddress = TextBoxUsername.Text.Trim(), // Default to Username
                        FromName = TextBoxFromName.Text.Trim()
                    }
                };

                var json = JsonSerializer.Serialize(settings, new JsonSerializerOptions { WriteIndented = true });
                Directory.CreateDirectory(_configPath);
                File.WriteAllText(Path.Combine(_configPath, "mail.json"), json);

                MessageBox.Show("Mail sunucu ayarları ve gönderen bilgisi kaydedildi.", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Kayıt hatası: {ex.Message}", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
