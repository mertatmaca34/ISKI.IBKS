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
    public partial class StationSettingsPage : UserControl
    {
        private readonly IServiceScopeFactory _scopeFactory;

        public StationSettingsPage(IServiceScopeFactory scopeFactory)
        {
            InitializeComponent();
            _scopeFactory = scopeFactory;
            Load += StationSettingsPage_Load;
            ButtonSave.Click += ButtonSave_Click;
        }

        public StationSettingsPage()
        {
            InitializeComponent();
        }

        private async void StationSettingsPage_Load(object? sender, EventArgs e)
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
                    StationSettingsControlStationName.AyarDegeri = settings.Name;
                    StationSettingsControlStationId.AyarDegeri = settings.StationId.ToString();
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

            if (!Guid.TryParse(StationSettingsControlStationId.AyarDegeri, out Guid stationId))
            {
                MessageBox.Show("Geçersiz İstasyon ID (GUID formatı gerekli).");
                return;
            }

            try
            {
                using var scope = _scopeFactory.CreateScope();
                var dbContext = scope.ServiceProvider.GetRequiredService<IbksDbContext>();
                var settings = await dbContext.StationSettings.FirstOrDefaultAsync();

                if (settings == null)
                {
                    settings = new StationSettings(stationId, StationSettingsControlStationName.AyarDegeri.Trim());
                    dbContext.StationSettings.Add(settings);
                }
                else
                {
                    settings.Name = StationSettingsControlStationName.AyarDegeri.Trim();
                    settings.StationId = stationId;
                    settings.UpdateLastDataDate(DateTime.UtcNow); // Just to mark updated
                }

                await dbContext.SaveChangesAsync();
                MessageBox.Show("İstasyon ayarları kaydedildi.");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Kaydetme hatası: {ex.Message}");
            }
        }
    }
}
