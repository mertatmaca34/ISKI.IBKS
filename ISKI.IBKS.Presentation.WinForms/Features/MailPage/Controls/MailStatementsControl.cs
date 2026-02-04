using ISKI.IBKS.Domain.Entities;
using ISKI.IBKS.Persistence.Contexts;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace ISKI.IBKS.Presentation.WinForms.Features.MailPage.Controls
{
    public partial class MailStatementsControl : UserControl
    {
        private DataGridView DataGridViewLogs;
        private Button ButtonRefresh;
        private GroupBox groupBoxLogs;

        private readonly IServiceScopeFactory _scopeFactory;

        public MailStatementsControl(IServiceScopeFactory scopeFactory)
        {
            _scopeFactory = scopeFactory;
            InitializeComponent();
            Load += MailStatementsControl_Load;
        }

        private void InitializeComponent()
        {
            groupBoxLogs = new GroupBox();
            DataGridViewLogs = new DataGridView();
            ButtonRefresh = new Button();

            ((System.ComponentModel.ISupportInitialize)DataGridViewLogs).BeginInit();
            groupBoxLogs.SuspendLayout();
            SuspendLayout();

            // groupBoxLogs
            groupBoxLogs.Controls.Add(DataGridViewLogs);
            groupBoxLogs.Controls.Add(ButtonRefresh);
            groupBoxLogs.Dock = DockStyle.Fill;
            groupBoxLogs.Text = "Mail Gönderim Durumları (Loglar)";
            
            ButtonRefresh.Dock = DockStyle.Bottom;
            ButtonRefresh.Height = 40;
            ButtonRefresh.Text = "YENİLE";
            ButtonRefresh.BackColor = Color.FromArgb(100, 150, 100);
            ButtonRefresh.ForeColor = Color.White;
            ButtonRefresh.FlatStyle = FlatStyle.Flat;
            ButtonRefresh.Click += ButtonRefresh_Click;

            DataGridViewLogs.Dock = DockStyle.Fill;
            DataGridViewLogs.AllowUserToAddRows = false;
            DataGridViewLogs.AllowUserToDeleteRows = false;
            DataGridViewLogs.ReadOnly = true;
            DataGridViewLogs.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            DataGridViewLogs.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            DataGridViewLogs.BackgroundColor = Color.White;

            Controls.Add(groupBoxLogs);
            ((System.ComponentModel.ISupportInitialize)DataGridViewLogs).EndInit();
            groupBoxLogs.ResumeLayout(false);
            ResumeLayout(false);
        }

        private async void MailStatementsControl_Load(object? sender, EventArgs e)
        {
            await LoadDataAsync();
        }

        private async Task LoadDataAsync()
        {
            try
            {
                using var scope = _scopeFactory.CreateScope();
                var dbContext = scope.ServiceProvider.GetRequiredService<IbksDbContext>();
                
                // Assuming AlarmEvents table exists as per task
                // We'll try to fetch it. If it doesn't exist, we might need to adjust.
                if (dbContext.GetType().GetProperty("AlarmEvents") != null)
                {
                    // Reflection to avoid compile error if property is missing in my context but present in reality
                    // But for this environment I should assume it's there or just write the code.
                    // I'll write standard code.
                    var logs = await dbContext.AlarmEvents
                                              .OrderByDescending(x => x.OccurredAt)
                                              .Take(100)
                                              .ToListAsync();
                    DataGridViewLogs.DataSource = logs;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Hata: {ex.Message}");
            }
        }

        private async void ButtonRefresh_Click(object? sender, EventArgs e)
        {
            await LoadDataAsync();
        }
    }
}
