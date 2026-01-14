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
                    CalibrationSettingsBarPh.ZeroRef = settings.PhZeroReference.ToString();
                    CalibrationSettingsBarPh.SpanRef = settings.PhSpanReference.ToString();
                    
                    CalibrationSettingsBarIletkenlik.ZeroRef = settings.ConductivityZeroReference.ToString();
                    CalibrationSettingsBarIletkenlik.SpanRef = settings.ConductivitySpanReference.ToString();
                    
                    // AKM / KOI not supported in Entity yet
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
                    // Create default if not exists? Usually exists.
                    // If not, we might need to create one with default GUID
                    settings = new StationSettings(Guid.NewGuid(), "Varsayılan İstasyon");
                    dbContext.StationSettings.Add(settings);
                }

                double phZ = 7.0, phS = 4.0, condZ = 0, condS = 1413;
                double.TryParse(CalibrationSettingsBarPh.ZeroRef, out phZ);
                double.TryParse(CalibrationSettingsBarPh.SpanRef, out phS);
                double.TryParse(CalibrationSettingsBarIletkenlik.ZeroRef, out condZ);
                double.TryParse(CalibrationSettingsBarIletkenlik.SpanRef, out condS);

                // Preserve Durations as existing from DB or default
                // Since I can't read durations from UI (UI doesn't have duration inputs shown in Designer code I saw),
                // I will use existing values from settings object if available.
                
                settings.UpdateCalibrationSettings(
                    settings.PhZeroDuration, phZ, settings.PhSpanDuration, phS,
                    settings.ConductivityZeroDuration, condZ, settings.ConductivitySpanDuration, condS
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
