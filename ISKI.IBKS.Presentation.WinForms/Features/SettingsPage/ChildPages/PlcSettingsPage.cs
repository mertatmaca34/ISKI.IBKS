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
    public partial class PlcSettingsPage : UserControl
    {
        private readonly IServiceScopeFactory _scopeFactory;

        public PlcSettingsPage(IServiceScopeFactory scopeFactory)
        {
            InitializeComponent();
            _scopeFactory = scopeFactory;
            Load += PlcSettingsPage_Load;
            ButtonSave.Click += ButtonSave_Click;
        }

        public PlcSettingsPage()
        {
            InitializeComponent();
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
                    settings = new StationSettings(Guid.NewGuid(), "Varsayılan İstasyon");
                    dbContext.StationSettings.Add(settings);
                }

                // Preserve existing Rack/Slot as they are not in UI
                int currentRack = settings.PlcRack;
                int currentSlot = settings.PlcSlot;
                
                settings.UpdatePlcSettings(PlcSettingsControlIp.AyarDegeri.Trim(), currentRack, currentSlot);

                await dbContext.SaveChangesAsync();
                MessageBox.Show("PLC bağlantı ayarları kaydedildi.");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Kaydetme hatası: {ex.Message}");
            }
        }
    }
}
