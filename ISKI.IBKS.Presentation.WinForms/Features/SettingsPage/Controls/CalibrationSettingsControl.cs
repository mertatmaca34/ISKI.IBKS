using ISKI.IBKS.Persistence.Contexts;
using ISKI.IBKS.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace ISKI.IBKS.Presentation.WinForms.Features.SettingsPage.Controls
{
    public partial class CalibrationSettingsControl : UserControl
    {
        private TextBox TextBoxPhZero;
        private TextBox TextBoxPhSpan;
        private TextBox TextBoxCondZero;
        private TextBox TextBoxCondSpan;
        private Button ButtonSave;
        private GroupBox groupBoxCalibration;
        private Label LabelPhZero;
        private Label LabelPhSpan;
        private Label LabelCondZero;
        private Label LabelCondSpan;

        private readonly IServiceScopeFactory _scopeFactory;

        public CalibrationSettingsControl(IServiceScopeFactory scopeFactory)
        {
            _scopeFactory = scopeFactory;
            InitializeComponent();
            Load += CalibrationSettingsControl_Load;
        }

        private void InitializeComponent()
        {
            groupBoxCalibration = new GroupBox();
            LabelPhZero = new Label();
            TextBoxPhZero = new TextBox();
            LabelPhSpan = new Label();
            TextBoxPhSpan = new TextBox();
            LabelCondZero = new Label();
            TextBoxCondZero = new TextBox();
            LabelCondSpan = new Label();
            TextBoxCondSpan = new TextBox();
            ButtonSave = new Button();

            groupBoxCalibration.SuspendLayout();
            SuspendLayout();

            // groupBoxCalibration
            groupBoxCalibration.Controls.Add(LabelPhZero);
            groupBoxCalibration.Controls.Add(TextBoxPhZero);
            groupBoxCalibration.Controls.Add(LabelPhSpan);
            groupBoxCalibration.Controls.Add(TextBoxPhSpan);
            groupBoxCalibration.Controls.Add(LabelCondZero);
            groupBoxCalibration.Controls.Add(TextBoxCondZero);
            groupBoxCalibration.Controls.Add(LabelCondSpan);
            groupBoxCalibration.Controls.Add(TextBoxCondSpan);
            groupBoxCalibration.Controls.Add(ButtonSave);
            groupBoxCalibration.Dock = DockStyle.Fill;
            groupBoxCalibration.Font = new Font("Arial", 9F, FontStyle.Bold, GraphicsUnit.Point);
            groupBoxCalibration.Location = new Point(0, 0);
            groupBoxCalibration.Name = "groupBoxCalibration";
            groupBoxCalibration.Size = new Size(600, 400);
            groupBoxCalibration.TabIndex = 0;
            groupBoxCalibration.TabStop = false;
            groupBoxCalibration.Text = "Kalibrasyon Referans Değerleri";

            LabelPhZero.Location = new Point(20, 35);
            LabelPhZero.Text = "pH Zero Referans:";
            LabelPhZero.AutoSize = true;

            TextBoxPhZero.Location = new Point(160, 32);
            TextBoxPhZero.Size = new Size(100, 23);

            LabelPhSpan.Location = new Point(20, 70);
            LabelPhSpan.Text = "pH Span Referans:";
            LabelPhSpan.AutoSize = true;

            TextBoxPhSpan.Location = new Point(160, 67);
            TextBoxPhSpan.Size = new Size(100, 23);

            LabelCondZero.Location = new Point(20, 105);
            LabelCondZero.Text = "İletkenlik Zero Ref:";
            LabelCondZero.AutoSize = true;

            TextBoxCondZero.Location = new Point(160, 102);
            TextBoxCondZero.Size = new Size(100, 23);

            LabelCondSpan.Location = new Point(20, 140);
            LabelCondSpan.Text = "İletkenlik Span Ref:";
            LabelCondSpan.AutoSize = true;

            TextBoxCondSpan.Location = new Point(160, 137);
            TextBoxCondSpan.Size = new Size(100, 23);

            ButtonSave.Location = new Point(160, 180);
            ButtonSave.Size = new Size(200, 40);
            ButtonSave.Text = "KAYDET";
            ButtonSave.BackColor = Color.FromArgb(0, 131, 200);
            ButtonSave.ForeColor = Color.White;
            ButtonSave.FlatStyle = FlatStyle.Flat;
            ButtonSave.Click += ButtonSave_Click;

            Controls.Add(groupBoxCalibration);
            Size = new Size(600, 400);
            groupBoxCalibration.ResumeLayout(false);
            groupBoxCalibration.PerformLayout();
            ResumeLayout(false);
        }

        private async void CalibrationSettingsControl_Load(object? sender, EventArgs e)
        {
            await LoadDataAsync();
        }

        private async Task LoadDataAsync()
        {
            try
            {
                using var scope = _scopeFactory.CreateScope();
                var dbContext = scope.ServiceProvider.GetRequiredService<IbksDbContext>();
                
                // Assuming single station setup, taking the first or default
                var station = await dbContext.StationSettings.FirstOrDefaultAsync();
                if (station != null)
                {
                    TextBoxPhZero.Text = station.PhZeroReference.ToString("F2");
                    TextBoxPhSpan.Text = station.PhSpanReference.ToString("F2");
                    TextBoxCondZero.Text = station.ConductivityZeroReference.ToString("F2");
                    TextBoxCondSpan.Text = station.ConductivitySpanReference.ToString("F2");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Veri yükleme hatası: {ex.Message}", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private async void ButtonSave_Click(object? sender, EventArgs e)
        {
            try
            {
                if (!double.TryParse(TextBoxPhZero.Text, out double phZero)) { MessageBox.Show("Geçersiz pH Zero Değeri"); return; }
                if (!double.TryParse(TextBoxPhSpan.Text, out double phSpan)) { MessageBox.Show("Geçersiz pH Span Değeri"); return; }
                if (!double.TryParse(TextBoxCondZero.Text, out double condZero)) { MessageBox.Show("Geçersiz İletkenlik Zero Değeri"); return; }
                if (!double.TryParse(TextBoxCondSpan.Text, out double condSpan)) { MessageBox.Show("Geçersiz İletkenlik Span Değeri"); return; }

                using var scope = _scopeFactory.CreateScope();
                var dbContext = scope.ServiceProvider.GetRequiredService<IbksDbContext>();
                var station = await dbContext.StationSettings.FirstOrDefaultAsync();

                if (station == null)
                {
                    // Create if not exists (though usually wizard creates it)
                    // Constructor: public StationSettings(Guid stationId, string name)
                    station = new StationSettings(Guid.NewGuid(), "ISKI PENDIK");
                    
                    // Set initial calibration settings
                    station.UpdateCalibrationSettings(
                        60, phZero, 
                        60, phSpan,
                        60, condZero,
                        60, condSpan,
                        60, 0, 60, 0 // AKM and KOI defaults
                    );
                    
                    dbContext.StationSettings.Add(station);
                }
                else
                {
                    // Preserve existing durations
                    station.UpdateCalibrationSettings(
                        station.PhZeroDuration, phZero,
                        station.PhSpanDuration, phSpan,
                        station.ConductivityZeroDuration, condZero,
                        station.ConductivitySpanDuration, condSpan,
                        station.AkmZeroDuration, 0,
                        station.KoiZeroDuration, 0
                    );
                }

                await dbContext.SaveChangesAsync();
                MessageBox.Show("Kalibrasyon referans değerleri kaydedildi.", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Kayıt hatası: {ex.Message}", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
