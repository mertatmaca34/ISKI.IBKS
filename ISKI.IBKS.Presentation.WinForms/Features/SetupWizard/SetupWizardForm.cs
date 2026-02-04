using System.Text.Json;
using ISKI.IBKS.Domain.Entities;
using ISKI.IBKS.Persistence.Contexts;
using Microsoft.EntityFrameworkCore;

namespace ISKI.IBKS.Presentation.WinForms.Features.SetupWizard;

/// <summary>
/// Kurulum sihirbazı ana formu
/// </summary>
public partial class SetupWizardForm : Form
{
    private readonly SetupState _state = new();
    private readonly List<ISetupWizardStep> _steps = new();
    private readonly IbksDbContext _dbContext;
    private readonly ISKI.IBKS.Application.Features.Plc.Abstractions.IPlcClient _plcClient;
    private readonly ISKI.IBKS.Infrastructure.RemoteApi.SAIS.Abstractions.ISaisAuthClient _saisAuthClient;
    private readonly ISKI.IBKS.Application.Services.Mail.IAlarmMailService _mailService;
    private readonly ISKI.IBKS.Application.Services.Iis.IIisDeploymentService _iisService;
    private int _currentStepIndex = 0;

    public bool IsCompleted { get; private set; }
    private bool _iisDeploymentSuccess = false;  // IIS deployment durumunu takip et

    public SetupWizardForm(
        IbksDbContext dbContext,
        ISKI.IBKS.Application.Features.Plc.Abstractions.IPlcClient plcClient,
        ISKI.IBKS.Infrastructure.RemoteApi.SAIS.Abstractions.ISaisAuthClient saisAuthClient,
        ISKI.IBKS.Application.Services.Mail.IAlarmMailService mailService,
        ISKI.IBKS.Application.Services.Iis.IIisDeploymentService iisService)
    {
        _dbContext = dbContext;
        _plcClient = plcClient;
        _saisAuthClient = saisAuthClient;
        _mailService = mailService;
        _iisService = iisService;
        InitializeComponent();
        LoadIcon();
        InitializeSteps();
        SetupEvents();
        ShowStep(0);
    }

    private void LoadIcon()
    {
        string iconPath = Path.Combine(AppContext.BaseDirectory, "Resources", "icons8_water.ico");
        if (File.Exists(iconPath))
        {
            this.Icon = new Icon(iconPath);
        }
    }

    private void InitializeSteps()
    {
        _steps.Add(new PlcSettingsStep(_state));
        _steps.Add(new SaisApiSettingsStep(_state));
        _steps.Add(new StationSettingsStep(_state));
        _steps.Add(new CalibrationSettingsStep(_state));
        _steps.Add(new MailSettingsStep(_state));
    }

    private void SetupEvents()
    {
        _btnNext.Click += async (s, e) => await NextStepAsync();
        _btnPrevious.Click += (s, e) => PreviousStep();
    }

    private void ShowStep(int index)
    {
        if (index < 0 || index >= _steps.Count) return;

        _currentStepIndex = index;
        var step = _steps[index];

        // Update header
        _lblTitle.Text = step.Title;
        _lblDescription.Text = step.Description;

        // Update step indicator
        _lblStepIndicator.Text = $"Adım {step.StepNumber} / {_steps.Count}";

        // Update buttons
        _btnPrevious.Enabled = index > 0;
        _btnNext.Text = index == _steps.Count - 1 ? "Bitir ✓" : "İleri >";

        // Show control
        _contentPanel.Controls.Clear();
        var control = step.GetControl();
        control.Dock = DockStyle.Fill;
        _contentPanel.Controls.Add(control);

        // Load step data
        _ = step.LoadAsync();
    }

    private async Task NextStepAsync()
    {
        var currentStep = _steps[_currentStepIndex];

        // Validate
        var (isValid, errorMessage) = currentStep.Validate();
        if (!isValid)
        {
            MessageBox.Show(errorMessage, "Doğrulama Hatası", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            return;
        }

        // Save
        var saved = await currentStep.SaveAsync();
        if (!saved)
        {
            MessageBox.Show("Ayarlar kaydedilemedi.", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            return;
        }

        // Next or finish
        if (_currentStepIndex < _steps.Count - 1)
        {
            ShowStep(_currentStepIndex + 1);
        }
        else
        {
            await FinishWizardAsync();
        }
    }

    private void PreviousStep()
    {
        if (_currentStepIndex > 0)
        {
            ShowStep(_currentStepIndex - 1);
        }
    }

    private async Task FinishWizardAsync()
    {
        _btnNext.Enabled = false;
        _btnNext.Text = "Kaydediliyor...";

        try
        {
            // 1. JSON Konfigürasyon Dosyalarını Kaydet
            await SaveConfigurationsAsync();

            // 2. Veritabanına Kaydet
            await SaveToDatabaseAsync();

            // 3. IIS ve API Deployment
            await RunIisDeploymentAsync();

            IsCompleted = true;

            // 4. Bildirim Ekranını Göster
            await ShowNotificationScreenAsync();
        }
        catch (Exception ex)
        {
            MessageBox.Show($"Yapılandırma kaydedilemedi.\n{ex.Message}", "Hata", 
                MessageBoxButtons.OK, MessageBoxIcon.Error);
            _btnNext.Enabled = true;
            _btnNext.Text = "Bitir ✓";
        }
    }

    private async Task SaveConfigurationsAsync()
    {
        var configDir = Path.Combine(AppContext.BaseDirectory, "Configuration");
        
        // Ensure directory exists
        if (!Directory.Exists(configDir))
        {
            // Try project path
            var projectRoot = Path.GetFullPath(Path.Combine(AppContext.BaseDirectory, "..", "..", "..", ".."));
            configDir = Path.Combine(projectRoot, "ISKI.IBKS.Presentation.WinForms", "Configuration");
            if (!Directory.Exists(configDir)) Directory.CreateDirectory(configDir);
        }

        // Save station.json
        var stationConfig = new
        {
            Station = new
            {
                _state.StationId,
                _state.StationName,
                LocalApi = new
                {
                    Host = _state.LocalApiHost,
                    Port = _state.LocalApiPort,
                    UserName = _state.LocalApiUserName,
                    Password = _state.LocalApiPassword
                }
            }
        };
        await File.WriteAllTextAsync(
            Path.Combine(configDir, "station.json"),
            JsonSerializer.Serialize(stationConfig, new JsonSerializerOptions { WriteIndented = true }));

        // Save sais.json
        var saisConfig = new
        {
            SAIS = new
            {
                BaseUrl = _state.SaisApiUrl,
                Username = _state.SaisUserName,
                Password = _state.SaisPassword
            }
        };
        await File.WriteAllTextAsync(
            Path.Combine(configDir, "sais.json"),
            JsonSerializer.Serialize(saisConfig, new JsonSerializerOptions { WriteIndented = true }));

        // Save mail.json
        var mailConfig = new
        {
            MailSettings = new
            {
                SmtpHost = _state.SmtpHost,
                SmtpPort = _state.SmtpPort,
                Username = _state.SmtpUserName,
                Password = _state.SmtpPassword,
                UseSsl = _state.SmtpUseSsl,
                FromAddress = _state.SmtpUserName, // Default to username
                FromName = "IBKS Sistem"
            }
        };
        await File.WriteAllTextAsync(
            Path.Combine(configDir, "mail.json"),
            JsonSerializer.Serialize(mailConfig, new JsonSerializerOptions { WriteIndented = true }));

        // Save calibration.json
        var calibrationConfig = new
        {
            Calibration = new
            {
                Ph = new
                {
                    ZeroRef = _state.PhZeroRef,
                    SpanRef = _state.PhSpanRef,
                    Duration = _state.PhCalibrationDuration
                },
                Iletkenlik = new
                {
                    ZeroRef = _state.IletkenlikZeroRef,
                    SpanRef = _state.IletkenlikSpanRef,
                    Duration = _state.IletkenlikCalibrationDuration
                }
            }
        };
        await File.WriteAllTextAsync(
            Path.Combine(configDir, "calibration.json"),
            JsonSerializer.Serialize(calibrationConfig, new JsonSerializerOptions { WriteIndented = true }));

        // Save plc.json
        var plcPath = Path.Combine(configDir, "plc.json");
        object finalPlcConfig;

        if (File.Exists(plcPath))
        {
            try
            {
                var existingJson = await File.ReadAllTextAsync(plcPath);
                using var doc = JsonDocument.Parse(existingJson);
                var root = doc.RootElement;
                
                // Mevcut yapıyı korumak için sözlük kullan
                var plcDict = new Dictionary<string, object>();
                
                if (root.TryGetProperty("Plc", out var plcElem))
                {
                    var plcContent = JsonSerializer.Deserialize<Dictionary<string, object>>(plcElem.GetRawText());
                    if (plcContent != null)
                    {
                        // Station kısmını güncelle
                        if (plcContent.TryGetValue("Station", out var stationObj))
                        {
                            var stationDict = JsonSerializer.Deserialize<Dictionary<string, object>>(stationObj.ToString()!);
                            if (stationDict != null)
                            {
                                stationDict["IpAddress"] = _state.PlcIpAddress;
                                stationDict["Rack"] = _state.PlcRack;
                                stationDict["Slot"] = _state.PlcSlot;
                                stationDict["StationId"] = _state.StationId;
                                plcContent["Station"] = stationDict;
                            }
                        }
                        
                        // Seçili sensörleri ekle/güncelle
                        plcContent["SelectedSensors"] = _state.SelectedSensors;
                        
                        plcDict["Plc"] = plcContent;
                    }
                }
                
                finalPlcConfig = plcDict;
            }
            catch (Exception)
            {
                // Hata durumunda fallback
                finalPlcConfig = new { Plc = new { Station = new { _state.PlcIpAddress, _state.PlcRack, _state.PlcSlot, _state.StationId }, SelectedSensors = _state.SelectedSensors } };
            }
        }
        else
        {
            // Dosya yoksa yeni oluştur
            finalPlcConfig = new { Plc = new { Station = new { _state.PlcIpAddress, _state.PlcRack, _state.PlcSlot, _state.StationId }, SelectedSensors = _state.SelectedSensors } };
        }

        await File.WriteAllTextAsync(
            plcPath,
            JsonSerializer.Serialize(finalPlcConfig, new JsonSerializerOptions { WriteIndented = true }));
    }

    private async Task RunIisDeploymentAsync()
    {
        // LocalAPI.zip dosyası var mı kontrol et - ClickOnce'da olmayabilir
        string zipPath = Path.Combine(AppContext.BaseDirectory, "Resources", "LocalAPI.zip");
        
        if (!File.Exists(zipPath))
        {
            // Dosya yoksa IIS deployment'ı atla
            _iisDeploymentSuccess = false;
            return;
        }

        var deploymentForm = new Shared.DeploymentForm();
        deploymentForm.Show();
        System.Windows.Forms.Application.DoEvents();

        try 
        {
            var progress = new Progress<string>(status => deploymentForm.UpdateStatus(status));

            // Adım 1: IIS Kurulumu
            bool iisOk = await _iisService.EnsureIisInstalledAsync(progress);
            if (!iisOk)
            {
                deploymentForm.UpdateStatus("⚠️ IIS kurulumu yapılamadı. Manual kurulum gerekebilir.");
                await Task.Delay(2000);
                _iisDeploymentSuccess = false;
                return;
            }

            // Adım 2: API Deployment
            string destPath = @"C:\inetpub\wwwroot\ISKI_LocalAPI";
            
            var config = new ISKI.IBKS.Application.Services.Iis.DeploymentConfig(
                ZipPath: zipPath,
                DestinationPath: destPath,
                LocalIp: _state.LocalApiHost,
                Port: int.TryParse(_state.LocalApiPort, out int port) ? port : 5502,
                StationId: _state.StationId
            );

            bool deployOk = await _iisService.DeployApiAsync(config, progress);
            
            if (deployOk)
            {
                _iisDeploymentSuccess = true;
                deploymentForm.UpdateStatus("✅ Deployment başarıyla tamamlandı.");
            }
            else
            {
                _iisDeploymentSuccess = false;
                deploymentForm.UpdateStatus("⚠️ API deployment tamamlanamadı.");
            }
            
            await Task.Delay(1500);
        }
        catch (Exception ex)
        {
            // IIS deployment başarısız oldu ama wizard devam etsin
            _iisDeploymentSuccess = false;
            deploymentForm.UpdateStatus($"⚠️ Deployment hatası: {ex.Message}");
            await Task.Delay(2000);
        }
        finally 
        {
            deploymentForm.CloseForm();
        }
    }

    private async Task SaveToDatabaseAsync()
    {
        if (_dbContext == null) return;

        // Station Settings
        var existingStation = await _dbContext.StationSettings.FirstOrDefaultAsync(s => s.StationId == _state.StationId);
        if (existingStation == null)
        {
            existingStation = new StationSettings(_state.StationId, _state.StationName);
            _dbContext.StationSettings.Add(existingStation);
        }
        else
        {
            existingStation.Name = _state.StationName;
        }

        existingStation.UpdatePlcSettings(_state.PlcIpAddress, _state.PlcRack, _state.PlcSlot);
        existingStation.UpdateFromSais(null, 1, null, "127.0.0.1", "5502", _state.LocalApiUserName, _state.LocalApiPassword, null, null, null, null, "IBKS v1.0");
        
        existingStation.UpdateCalibrationSettings(
            _state.PhCalibrationDuration, _state.PhZeroRef,
            _state.PhCalibrationDuration, _state.PhSpanRef,
            _state.IletkenlikCalibrationDuration, _state.IletkenlikZeroRef,
            _state.IletkenlikCalibrationDuration, _state.IletkenlikSpanRef,
            60, 0, 60, 0); // AKM and KOI defaults

        // Seçili sensörleri kaydet
        existingStation.UpdateSelectedSensors(_state.SelectedSensors);

        await _dbContext.SaveChangesAsync();
    }

    private async Task ShowNotificationScreenAsync()
    {
        _contentPanel.Controls.Clear();
        _buttonPanel.Visible = false;
        _stepPanel.Visible = false;
        _headerPanel.BackColor = Color.FromArgb(0, 122, 204); // İşlem sürüyor rengi
        _lblTitle.Text = "Ayarlar Doğrulanıyor...";
        _lblDescription.Text = "Sistem bağlantıları kontrol ediliyor, lütfen bekleyiniz.";

        var resultPanel = new TableLayoutPanel
        {
            Dock = DockStyle.Fill,
            ColumnCount = 2,
            RowCount = 8,  // 6 test + 1 boşluk + 1 buton
            Padding = new Padding(40, 20, 40, 20)
        };
        resultPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 40F));
        resultPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));

        var messages = new[]
        {
            "PLC Bağlantısı Kontrol Ediliyor...",
            "SAIS API Erişimi Kontrol Ediliyor...",
            "İstasyon Verileri Eşleştiriliyor...",
            "Kalibrasyon Parametreleri Yükleniyor...",
            "Mail Sunucu Erişimi Kontrol Ediliyor...",
            "Local API Yayımlama Durumu Kontrol Ediliyor..."
        };

        var labels = new List<(Label check, Label msg)>();

        for (int i = 0; i < messages.Length; i++)
        {
            var checkmark = new Label
            {
                Text = "⭕", // Bekleme ikonu
                ForeColor = Color.Gray,
                Font = new Font("Segoe UI", 12F, FontStyle.Bold),
                Dock = DockStyle.Fill,
                TextAlign = ContentAlignment.MiddleCenter
            };
            var message = new Label
            {
                Text = messages[i],
                Font = new Font("Segoe UI", 11F),
                Dock = DockStyle.Fill,
                TextAlign = ContentAlignment.MiddleLeft,
                ForeColor = Color.Gray
            };
            resultPanel.Controls.Add(checkmark, 0, i);
            resultPanel.Controls.Add(message, 1, i);
            labels.Add((checkmark, message));

            // Sırayla ekle ki görünsün
            await Task.Delay(100);
        }

        _contentPanel.Controls.Add(resultPanel);

        // REAL Verification: Check process
        var testActions = new List<Func<Task<(bool success, string message)>>>
        {
            async () => {
                try {
                    _plcClient.ForceReconnect(_state.PlcIpAddress, _state.PlcRack, _state.PlcSlot);
                    return (true, "PLC Bağlantısı Başarılı");
                } catch (Exception ex) {
                    return (false, $"PLC Hatası: {ex.Message}");
                }
            },
            async () => {
                try {
                    var response = await _saisAuthClient.LoginAsync(new ISKI.IBKS.Infrastructure.RemoteApi.SAIS.Contracts.Login.LoginRequest {
                        UserName = _state.LocalApiUserName,
                        Password = _state.LocalApiPassword
                    });
                    return (response.Result, response.Result ? "SAIS API Erişimi Başarılı" : $"SAIS Hatası: {response.Message}");
                } catch (Exception ex) {
                    return (false, $"SAIS API Hatası: {ex.Message}");
                }
            },
            async () => {
                try {
                    var exists = await _dbContext.StationSettings.AnyAsync(s => s.StationId == _state.StationId);
                    return (exists, exists ? "İstasyon Verileri Kaydedildi" : "İstasyon Verisi Bulunamadı");
                } catch (Exception ex) {
                    return (false, $"DB Hatası: {ex.Message}");
                }
            },
            async () => {
                try {
                    var settings = await _dbContext.StationSettings.FirstOrDefaultAsync();
                    bool valid = settings != null && settings.PhZeroReference == _state.PhZeroRef;
                    return (valid, valid ? "Kalibrasyon Ayarları Aktif" : "Parametre Uyuşmazlığı");
                } catch (Exception ex) {
                    return (false, $"DB Hatası: {ex.Message}");
                }
            },
            async () => {
                try {
                    // SMTP bağlantı testi - Gerçek bir bağlantı ve gönderim testi yapar
                    using (var client = new System.Net.Mail.SmtpClient(_state.SmtpHost, _state.SmtpPort))
                    {
                        client.EnableSsl = _state.SmtpUseSsl;
                        client.Credentials = new System.Net.NetworkCredential(_state.SmtpUserName, _state.SmtpPassword);
                        client.Timeout = 10000; // 10 saniye timeout
                        
                        var fromAddress = _state.SmtpUserName;
                        // Bazı SMTP sunucuları From adresi ile Username uyuşmazsa hata verir
                        var mailMessage = new System.Net.Mail.MailMessage(fromAddress, fromAddress)
                        {
                            Subject = "IBKS Kurulum Testi",
                            Body = "Bu mesaj IBKS Kurulum Sihirbazı tarafından bağlantı testi amacıyla otomatik olarak gönderilmiştir."
                        };
                        
                        await client.SendMailAsync(mailMessage);
                        return (true, "Mail Sunucusu Hazır");
                    }
                } catch (Exception ex) {
                    return (false, $"Mail Hatası: {ex.Message}");
                }
            },
            async () => {
                // Local API deployment kontrolü
                try {
                    if (!_iisDeploymentSuccess)
                    {
                        string zipPath = Path.Combine(AppContext.BaseDirectory, "Resources", "LocalAPI.zip");
                        if (!File.Exists(zipPath))
                            return (false, "LocalAPI.zip dosyası bulunamadı");
                        return (false, "IIS/API kurulumu yapılamadı");
                    }
                    
                    // API'ye HTTP isteği yaparak çalıştığını doğrula
                    using var client = new System.Net.Http.HttpClient();
                    client.Timeout = TimeSpan.FromSeconds(10);
                    int port = int.TryParse(_state.LocalApiPort, out int p) ? p : 5502;
                    
                    // Birden fazla endpoint dene - herhangi biri yanıt verirse başarılı
                    var endpoints = new[] { "/", "/health", "/api", "/swagger" };
                    foreach (var endpoint in endpoints)
                    {
                        try
                        {
                            var response = await client.GetAsync($"http://localhost:{port}{endpoint}");
                            // Herhangi bir yanıt (404 dahil) API'nin çalıştığını gösterir
                            return (true, "Local API Çalışıyor");
                        }
                        catch { }
                    }
                    
                    // HTTP başarısız olduysa TCP bağlantısı dene
                    using var tcpClient = new System.Net.Sockets.TcpClient();
                    var connectTask = tcpClient.ConnectAsync("localhost", port);
                    if (await Task.WhenAny(connectTask, Task.Delay(3000)) == connectTask && tcpClient.Connected)
                    {
                        return (true, $"Local API Çalışıyor (Port {port})");
                    }
                    
                    return (false, "API Yanıt Vermiyor");
                } catch {
                    return (_iisDeploymentSuccess, _iisDeploymentSuccess ? "Local API Kuruldu (Başlatılıyor)" : "Local API Erişilemedi");
                }
            }
        };

        bool allSuccess = true;

        for (int i = 0; i < testActions.Count; i++)
        {
            var (lblCheck, lblMsg) = labels[i];
            
            lblMsg.ForeColor = Color.Black;
            lblCheck.Text = "⏳";
            lblCheck.ForeColor = Color.Orange;
            
            // Give UI time to update
            await Task.Delay(500);

            var (success, resultMessage) = await testActions[i]();
            
            if (success)
            {
                lblCheck.Text = "✓";
                lblCheck.ForeColor = Color.FromArgb(46, 204, 113);
                lblMsg.Text = resultMessage;
                lblMsg.ForeColor = Color.FromArgb(46, 204, 113);
            }
            else
            {
                lblCheck.Text = "❌";
                lblCheck.ForeColor = Color.FromArgb(231, 76, 60);
                lblMsg.Text = resultMessage;
                lblMsg.ForeColor = Color.FromArgb(231, 76, 60);
                allSuccess = false;
            }
        }

        // Finalize
        if (allSuccess)
        {
            _headerPanel.BackColor = Color.FromArgb(46, 204, 113);
            _lblTitle.Text = "Kurulum Tamamlandı";
            _lblDescription.Text = "Tüm testler başarılı, uygulama başlatılıyor.";
        }
        else
        {
            _headerPanel.BackColor = Color.FromArgb(231, 76, 60);
            _lblTitle.Text = "Kurulum Tamamlandı (Hatalı)";
            _lblDescription.Text = "Bazı bağlantılar kurulamadı, ancak kurulum kaydedildi.";
        }


        var btnContinue = new Button
        {
            Text = "Uygulamayı Başlat",
            Width = 200,
            Height = 45,
            BackColor = Color.FromArgb(0, 120, 215),
            ForeColor = Color.White,
            FlatStyle = FlatStyle.Flat,
            Font = new Font("Segoe UI", 11F, FontStyle.Bold),
            Cursor = Cursors.Hand,
            Anchor = AnchorStyles.None,
            Margin = new Padding(0, 20, 0, 0)
        };
        btnContinue.Click += (s, e) => this.Close();
        resultPanel.Controls.Add(btnContinue, 0, 6);
        resultPanel.SetColumnSpan(btnContinue, 2);
    }

    /// <summary>
    /// Kurulum gerekli mi kontrol eder
    /// </summary>
    /// <summary>
    /// Kurulum gerekli mi kontrol eder.
    /// Hem konfigürasyon dosyasına hem de veritabanına bakar.
    /// </summary>
    public static bool IsSetupRequired(IbksDbContext context)
    {
        // 1. Dosya Kontrolü
        var configDir = Path.Combine(AppContext.BaseDirectory, "Configuration");
        if (!Directory.Exists(configDir))
        {
            var projectRoot = Path.GetFullPath(Path.Combine(AppContext.BaseDirectory, "..", "..", "..", ".."));
            configDir = Path.Combine(projectRoot, "ISKI.IBKS.Presentation.WinForms", "Configuration");
        }

        var stationConfig = Path.Combine(configDir, "station.json");
        if (!File.Exists(stationConfig)) return true;

        // 2. Veritabanı Kontrolü
        try
        {
            // Eğer veritabanında hiç istasyon ayarı yoksa kurulum gerekir
            if (!context.StationSettings.Any()) return true;
        }
        catch
        {
            // Veritabanına erişilemiyorsa güvenli tarafta kalıp kurulumu açalım
             return true; 
        }

        return false;
    }
}
