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

namespace ISKI.IBKS.Presentation.WinForms.Features.MailPage.ChildPages
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
                        TextBoxHost.Text = mail.GetProperty("SmtpHost").GetString() ?? "";
                        if (mail.TryGetProperty("SmtpPort", out var portProp))
                        {
                            TextBoxPort.Text = portProp.GetInt32().ToString();
                        }
                        
                        TextBoxUsername.Text = mail.GetProperty("Username").GetString() ?? "";
                        TextBoxPassword.Text = mail.GetProperty("Password").GetString() ?? "";
                        CheckBoxSSL.Checked = mail.GetProperty("UseSsl").GetBoolean();
                        // FromAddress might be missing in UI, but we preserve it in save if we read it? 
                        // Actually we can't preserve it easily without storing it in a field if we overwrite the whole file.
                        // Let's assume Username is FromAddress if FromAddress is missing in UI.
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
                if (!int.TryParse(TextBoxPort.Text, out int port))
                {
                    MessageBox.Show("Port numarası geçerli bir sayı olmalıdır.", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // If UI doesn't have FromAddress, use Username or a default
                string fromAddress = TextBoxUsername.Text.Trim(); 
                
                // Try to read existing file to preserve FromAddress if possible
                try 
                {
                     var mailPath = Path.Combine(_configPath, "mail.json");
                     if (File.Exists(mailPath))
                     {
                        using var doc = JsonDocument.Parse(File.ReadAllText(mailPath));
                        if(doc.RootElement.TryGetProperty("MailSettings", out var ms) && ms.TryGetProperty("FromAddress", out var fa))
                        {
                            var existing = fa.GetString();
                            if(!string.IsNullOrWhiteSpace(existing)) fromAddress = existing;
                        }
                     }
                }
                catch {}

                var settings = new
                {
                    MailSettings = new
                    {
                        SmtpHost = TextBoxHost.Text.Trim(),
                        SmtpPort = port,
                        Username = TextBoxUsername.Text.Trim(),
                        Password = TextBoxPassword.Text,
                        UseSsl = CheckBoxSSL.Checked,
                        FromAddress = fromAddress 
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
