using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using ISKI.IBKS.Domain.Entities;
using ISKI.IBKS.Persistence.Contexts;
using System.Text.Json;

namespace ISKI.IBKS.Presentation.WinForms.Features.SettingsPage.ChildPages
{
    public partial class CalibrationSettingsPage : UserControl
    {
        private readonly IServiceScopeFactory _scopeFactory;
        private readonly string _configPath;

        public CalibrationSettingsPage(IServiceScopeFactory scopeFactory)
        {
            InitializeComponent();
            _scopeFactory = scopeFactory;
            _configPath = Path.Combine(AppContext.BaseDirectory, "Configuration");
            Load += CalibrationSettingsPage_Load;
            ButtonSave.Click += ButtonSave_Click;
        }

        public CalibrationSettingsPage()
        {
            InitializeComponent();
            _scopeFactory = null!;
        }

        private async void CalibrationSettingsPage_Load(object? sender, EventArgs e)
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
                    // pH settings
                    CalibrationSettingsBarPh.ZeroRef = settings.PhZeroReference.ToString();
                    CalibrationSettingsBarPh.ZeroTime = settings.PhZeroDuration.ToString();
                    CalibrationSettingsBarPh.SpanRef = settings.PhSpanReference.ToString();
                    CalibrationSettingsBarPh.SpanTime = settings.PhSpanDuration.ToString();
                    
                    // Conductivity settings
                    CalibrationSettingsBarIletkenlik.ZeroRef = settings.ConductivityZeroReference.ToString();
                    CalibrationSettingsBarIletkenlik.ZeroTime = settings.ConductivityZeroDuration.ToString();
                    CalibrationSettingsBarIletkenlik.SpanRef = settings.ConductivitySpanReference.ToString();
                    CalibrationSettingsBarIletkenlik.SpanTime = settings.ConductivitySpanDuration.ToString();
                    
                    // AKM / KOI - load if available
                    CalibrationSettingsBarAkm.ZeroTime = settings.AkmZeroDuration.ToString();
                    CalibrationSettingsBarKoi.ZeroTime = settings.KoiZeroDuration.ToString();
                    
                    // AKM / KOI reference values not fully supported yet
                    CalibrationSettingsBarAkm.Enabled = false;
                    CalibrationSettingsBarKoi.Enabled = false;
                }

                // Try to overlay from calibration.json if exists
                var path = Path.Combine(_configPath, "calibration.json");
                if (File.Exists(path))
                {
                    try {
                        var json = await File.ReadAllTextAsync(path);
                        var doc = JsonDocument.Parse(json);
                        if (doc.RootElement.TryGetProperty("Calibration", out var cal))
                        {
                            if (cal.TryGetProperty("Ph", out var ph))
                            {
                                if (ph.TryGetProperty("ZeroRef", out var zr)) CalibrationSettingsBarPh.ZeroRef = zr.GetDouble().ToString();
                                if (ph.TryGetProperty("SpanRef", out var sr)) CalibrationSettingsBarPh.SpanRef = sr.GetDouble().ToString();
                                if (ph.TryGetProperty("Duration", out var dur)) CalibrationSettingsBarPh.ZeroTime = dur.GetInt32().ToString();
                            }
                            if (cal.TryGetProperty("Iletkenlik", out var cond))
                            {
                                if (cond.TryGetProperty("ZeroRef", out var zr)) CalibrationSettingsBarIletkenlik.ZeroRef = zr.GetDouble().ToString();
                                if (cond.TryGetProperty("SpanRef", out var sr)) CalibrationSettingsBarIletkenlik.SpanRef = sr.GetDouble().ToString();
                                if (cond.TryGetProperty("Duration", out var dur)) CalibrationSettingsBarIletkenlik.ZeroTime = dur.GetInt32().ToString();
                            }
                        }
                    } catch { /* Ignore loading errors from file, fall back to DB values */ }
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
                using var scope = _scopeFactory.CreateScope();
                var dbContext = scope.ServiceProvider.GetRequiredService<IbksDbContext>();
                var settings = await dbContext.StationSettings.FirstOrDefaultAsync();

                if (settings == null)
                {
                    // Create default if not exists
                    settings = new StationSettings(Guid.NewGuid(), "Varsayılan İstasyon");
                    dbContext.StationSettings.Add(settings);
                }

                // Parse reference values
                double phZeroRef = 7.0, phSpanRef = 4.0, condZeroRef = 0, condSpanRef = 1413;
                double.TryParse(CalibrationSettingsBarPh.ZeroRef, out phZeroRef);
                double.TryParse(CalibrationSettingsBarPh.SpanRef, out phSpanRef);
                double.TryParse(CalibrationSettingsBarIletkenlik.ZeroRef, out condZeroRef);
                double.TryParse(CalibrationSettingsBarIletkenlik.SpanRef, out condSpanRef);

                // Parse duration values from UI
                int phZeroDuration = 60, phSpanDuration = 60, condZeroDuration = 60, condSpanDuration = 60;
                int akmZeroDuration = 60, koiZeroDuration = 60;
                
                int.TryParse(CalibrationSettingsBarPh.ZeroTime, out phZeroDuration);
                int.TryParse(CalibrationSettingsBarPh.SpanTime, out phSpanDuration);
                int.TryParse(CalibrationSettingsBarIletkenlik.ZeroTime, out condZeroDuration);
                int.TryParse(CalibrationSettingsBarIletkenlik.SpanTime, out condSpanDuration);
                int.TryParse(CalibrationSettingsBarAkm.ZeroTime, out akmZeroDuration);
                int.TryParse(CalibrationSettingsBarKoi.ZeroTime, out koiZeroDuration);
                
                // Detect changes for Audit Logging (Madde 3.10.9)
                var logs = new List<LogEntry>();
                if (settings.PhZeroReference != phZeroRef || settings.PhZeroDuration != phZeroDuration)
                    logs.Add(new LogEntry(settings.StationId, "pH Kanal parametresi değiştirildi", $"pH Sıfır Referansı: {settings.PhZeroReference} -> {phZeroRef}, Süre: {settings.PhZeroDuration} -> {phZeroDuration}"));
                if (settings.PhSpanReference != phSpanRef || settings.PhSpanDuration != phSpanDuration)
                    logs.Add(new LogEntry(settings.StationId, "pH Kanal parametresi değiştirildi", $"pH Span Referansı: {settings.PhSpanReference} -> {phSpanRef}, Süre: {settings.PhSpanDuration} -> {phSpanDuration}"));
                
                if (settings.ConductivityZeroReference != condZeroRef || settings.ConductivityZeroDuration != condZeroDuration)
                    logs.Add(new LogEntry(settings.StationId, "İletkenlik Kanal parametresi değiştirildi", $"İletkenlik Sıfır Referansı: {settings.ConductivityZeroReference} -> {condZeroRef}, Süre: {settings.ConductivityZeroDuration} -> {condZeroDuration}"));
                if (settings.ConductivitySpanReference != condSpanRef || settings.ConductivitySpanDuration != condSpanDuration)
                    logs.Add(new LogEntry(settings.StationId, "İletkenlik Kanal parametresi değiştirildi", $"İletkenlik Span Referansı: {settings.ConductivitySpanReference} -> {condSpanRef}, Süre: {settings.ConductivitySpanDuration} -> {condSpanDuration}"));

                settings.UpdateCalibrationSettings(
                    phZeroDuration, phZeroRef, phSpanDuration, phSpanRef,
                    condZeroDuration, condZeroRef, condSpanDuration, condSpanRef,
                    akmZeroDuration, 0, koiZeroDuration, 0
                );

                if (logs.Any()) dbContext.LogEntries.AddRange(logs);
                await dbContext.SaveChangesAsync();

                // Save to calibration.json
                var calibrationConfig = new
                {
                    Calibration = new
                    {
                        Ph = new
                        {
                            ZeroRef = phZeroRef,
                            SpanRef = phSpanRef,
                            Duration = phZeroDuration
                        },
                        Iletkenlik = new
                        {
                            ZeroRef = condZeroRef,
                            SpanRef = condSpanRef,
                            Duration = condZeroDuration
                        }
                    }
                };
                
                var json = System.Text.Json.JsonSerializer.Serialize(calibrationConfig, new System.Text.Json.JsonSerializerOptions { WriteIndented = true });
                Directory.CreateDirectory(_configPath);
                await File.WriteAllTextAsync(Path.Combine(_configPath, "calibration.json"), json);

                MessageBox.Show("Kalibrasyon ayarları kaydedildi.");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Kaydetme hatası: {ex.Message}");
            }
        }
    }
}
