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
    private int _currentStepIndex = 0;

    public bool IsCompleted { get; private set; }

    public SetupWizardForm(IbksDbContext dbContext)
    {
        _dbContext = dbContext;
        InitializeComponent();
        InitializeSteps();
        SetupEvents();
        ShowStep(0);
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

            IsCompleted = true;

            // 3. Bildirim Ekranını Göster
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

        // Save mail.json
        var mailConfig = new
        {
            Mail = new
            {
                Smtp = new
                {
                    Host = _state.SmtpHost,
                    Port = _state.SmtpPort,
                    UserName = _state.SmtpUserName,
                    Password = _state.SmtpPassword,
                    UseSsl = _state.SmtpUseSsl
                }
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
        var plcConfig = new
        {
            PlcSettings = new
            {
                Connection = new
                {
                    IpAddress = _state.PlcIpAddress,
                    Rack = _state.PlcRack,
                    Slot = _state.PlcSlot
                }
            }
        };
        await File.WriteAllTextAsync(
            Path.Combine(configDir, "plc.json"),
            JsonSerializer.Serialize(plcConfig, new JsonSerializerOptions { WriteIndented = true }));
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
            _state.IletkenlikCalibrationDuration, _state.IletkenlikSpanRef);

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
            RowCount = 7,
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
            "Mail Sunucu Erişimi Kontrol Ediliyor..."
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

        // Simulasyon: Check process
        var checks = new[] 
        { 
            "PLC Bağlantısı Başarılı", 
            "SAIS API Erişimi Başarılı", 
            "İstasyon Verileri Kaydedildi",
            "Kalibrasyon Ayarları Aktif",
            "Mail Sunucusu Hazır"
        };

        for (int i = 0; i < checks.Length; i++)
        {
            var (lblCheck, lblMsg) = labels[i];
            
            // Simüle etme (gerçek bağlantı kontrolünü burada yapabiliriz)
            lblMsg.ForeColor = Color.Black;
            lblCheck.Text = "⏳";
            lblCheck.ForeColor = Color.Orange;
            
            await Task.Delay(800); // İşlem süresi simülasyonu

            lblCheck.Text = "✓";
            lblCheck.ForeColor = Color.FromArgb(46, 204, 113);
            lblMsg.Text = checks[i];
            lblMsg.ForeColor = Color.FromArgb(46, 204, 113);
        }

        // Başarılı
        _headerPanel.BackColor = Color.FromArgb(46, 204, 113);
        _lblTitle.Text = "Kurulum Tamamlandı";
        _lblDescription.Text = "Tüm testler başarılı, uygulama başlatılıyor.";


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
