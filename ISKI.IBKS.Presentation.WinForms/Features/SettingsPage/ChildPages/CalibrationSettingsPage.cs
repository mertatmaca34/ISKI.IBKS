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

namespace ISKI.IBKS.Presentation.WinForms.Features.SettingsPage.ChildPages
{
    public partial class CalibrationSettingsPage : UserControl
    {
        private readonly IServiceScopeFactory _scopeFactory;

        public CalibrationSettingsPage(IServiceScopeFactory scopeFactory)
        {
            InitializeComponent();
            _scopeFactory = scopeFactory;
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
                
                settings.UpdateCalibrationSettings(
                    phZeroDuration, phZeroRef, phSpanDuration, phSpanRef,
                    condZeroDuration, condZeroRef, condSpanDuration, condSpanRef,
                    akmZeroDuration, 0, koiZeroDuration, 0
                );

                await dbContext.SaveChangesAsync();
                MessageBox.Show("Kalibrasyon ayarları kaydedildi.");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Kaydetme hatası: {ex.Message}");
            }
        }
    }
}
