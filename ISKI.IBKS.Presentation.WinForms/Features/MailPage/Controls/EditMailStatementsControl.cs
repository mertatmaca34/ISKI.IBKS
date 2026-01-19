using ISKI.IBKS.Domain.Entities;
using ISKI.IBKS.Persistence.Contexts;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace ISKI.IBKS.Presentation.WinForms.Features.MailPage.Controls
{
    public partial class EditMailStatementsControl : UserControl
    {
        private DataGridView DataGridViewAlarms = null!;
        private Button ButtonRefresh = null!;
        private GroupBox groupBoxAlarms = null!;

        private readonly IServiceScopeFactory _scopeFactory;

        public EditMailStatementsControl(IServiceScopeFactory scopeFactory)
        {
            _scopeFactory = scopeFactory;
            InitializeComponent();
            Load += EditMailStatementsControl_Load;
            DataGridViewAlarms.CellDoubleClick += DataGridViewAlarms_CellDoubleClick;
        }

        private void InitializeComponent()
        {
            groupBoxAlarms = new GroupBox();
            DataGridViewAlarms = new DataGridView();
            ButtonRefresh = new Button();

            ((System.ComponentModel.ISupportInitialize)DataGridViewAlarms).BeginInit();
            groupBoxAlarms.SuspendLayout();
            SuspendLayout();

            // groupBoxAlarms
            groupBoxAlarms.Controls.Add(DataGridViewAlarms);
            groupBoxAlarms.Controls.Add(ButtonRefresh);
            groupBoxAlarms.Dock = DockStyle.Fill;
            groupBoxAlarms.Text = "Alarm Tanımları ve Eşik Değerleri (Çift tıklayarak düzenleyin)";
            
            ButtonRefresh.Dock = DockStyle.Bottom;
            ButtonRefresh.Height = 40;
            ButtonRefresh.Text = "YENİLE";
            ButtonRefresh.BackColor = Color.FromArgb(100, 150, 100);
            ButtonRefresh.ForeColor = Color.White;
            ButtonRefresh.FlatStyle = FlatStyle.Flat;
            ButtonRefresh.Click += ButtonRefresh_Click;

            DataGridViewAlarms.Dock = DockStyle.Fill;
            DataGridViewAlarms.AllowUserToAddRows = false;
            DataGridViewAlarms.AllowUserToDeleteRows = false;
            DataGridViewAlarms.ReadOnly = true;
            DataGridViewAlarms.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            DataGridViewAlarms.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            DataGridViewAlarms.BackgroundColor = Color.White;

            Controls.Add(groupBoxAlarms);
            ((System.ComponentModel.ISupportInitialize)DataGridViewAlarms).EndInit();
            groupBoxAlarms.ResumeLayout(false);
            ResumeLayout(false);
        }

        private async void EditMailStatementsControl_Load(object? sender, EventArgs e)
        {
            await LoadDataAsync();
        }

        private async Task LoadDataAsync()
        {
            try
            {
                using var scope = _scopeFactory.CreateScope();
                var dbContext = scope.ServiceProvider.GetRequiredService<IbksDbContext>();
                var alarms = await dbContext.AlarmDefinitions.ToListAsync();
                DataGridViewAlarms.DataSource = alarms;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Hata: {ex.Message}");
            }
        }

        private async void DataGridViewAlarms_CellDoubleClick(object? sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;

            var alarm = (AlarmDefinition)DataGridViewAlarms.Rows[e.RowIndex].DataBoundItem;
            if (alarm == null) return;

            string newMinStr = ShowInputDialog("Alt Eşik Değeri (Boş bırakmak için iptal):", alarm.MinThreshold?.ToString() ?? "");
            // Check if user cancelled
            if (newMinStr == "_CANCEL_") return;

            string newMaxStr = ShowInputDialog("Üst Eşik Değeri (Boş bırakmak için iptal):", alarm.MaxThreshold?.ToString() ?? "");
            if (newMaxStr == "_CANCEL_") return;

            double? newMin = double.TryParse(newMinStr, out double minVal) ? minVal : null;
            double? newMax = double.TryParse(newMaxStr, out double maxVal) ? maxVal : null;

            try
            {
                using var scope = _scopeFactory.CreateScope();
                var dbContext = scope.ServiceProvider.GetRequiredService<IbksDbContext>();
                
                var dbAlarm = await dbContext.AlarmDefinitions.FindAsync(alarm.Id);
                if (dbAlarm != null)
                {
                    dbAlarm.UpdateThresholds(newMin, newMax);
                    await dbContext.SaveChangesAsync();
                    MessageBox.Show("Alarm güncellendi.", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    await LoadDataAsync();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Güncelleme hatası: {ex.Message}", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private string ShowInputDialog(string text, string defaultValue)
        {
            Form prompt = new Form()
            {
                Width = 350,
                Height = 150,
                FormBorderStyle = FormBorderStyle.FixedDialog,
                Text = "Değer Giriniz",
                StartPosition = FormStartPosition.CenterScreen
            };
            Label textLabel = new Label() { Left = 20, Top = 20, Text = text, AutoSize = true };
            TextBox textBox = new TextBox() { Left = 20, Top = 50, Width = 300, Text = defaultValue };
            Button confirmation = new Button() { Text = "Tamam", Left = 220, Width = 100, Top = 80, DialogResult = DialogResult.OK };
            Button cancel = new Button() { Text = "İptal", Left = 110, Width = 100, Top = 80, DialogResult = DialogResult.Cancel };
            
            prompt.Controls.Add(textLabel);
            prompt.Controls.Add(textBox);
            prompt.Controls.Add(confirmation);
            prompt.Controls.Add(cancel);
            prompt.AcceptButton = confirmation;
            prompt.CancelButton = cancel;

            return prompt.ShowDialog() == DialogResult.OK ? textBox.Text : "_CANCEL_";
        }

        private async void ButtonRefresh_Click(object? sender, EventArgs e)
        {
            await LoadDataAsync();
        }
    }
}
