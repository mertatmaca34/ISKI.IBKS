using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using ISKI.IBKS.Domain.Entities;
using ISKI.IBKS.Persistence.Contexts;

namespace ISKI.IBKS.Presentation.WinForms.Features.MailPage.ChildPages
{
    public partial class MailStatementsPage : UserControl
    {
        private readonly IServiceScopeFactory _scopeFactory;
        private List<AlarmDefinition> _allAlarms = new();
        private Dictionary<Guid, string> _alarmUserNames = new();

        public MailStatementsPage(IServiceScopeFactory scopeFactory)
        {
            _scopeFactory = scopeFactory;
            InitializeComponent();
            
            // Attach event handlers
            Load += MailStatementsEditPage_Load;
            TextBoxSearch.TextChanged += TextBoxSearch_TextChanged;
            ButtonAddNew.Click += ButtonAddNew_Click;
            DataGridViewAlarms.CellContentClick += DataGridViewAlarms_CellContentClick;
        }

        public MailStatementsPage()
        {
            _scopeFactory = null!;
            InitializeComponent();
        }

        private async void MailStatementsEditPage_Load(object? sender, EventArgs e)
        {
            if (_scopeFactory == null) return;
            await LoadDefinitionsAsync();
        }

        private async Task LoadDefinitionsAsync()
        {
            try
            {
                using var scope = _scopeFactory.CreateScope();
                var dbContext = scope.ServiceProvider.GetRequiredService<IbksDbContext>();
                
                _allAlarms = await dbContext.AlarmDefinitions.OrderBy(a => a.Name).ToListAsync();
                
                // Load alarm user subscriptions with user names
                var subscriptions = await dbContext.AlarmUserSubscriptions
                    .Include(s => s.AlarmUser)
                    .Where(s => s.IsActive)
                    .ToListAsync();
                
                _alarmUserNames = subscriptions
                    .GroupBy(s => s.AlarmDefinitionId)
                    .ToDictionary(
                        g => g.Key,
                        g => string.Join(", ", g.Select(s => s.AlarmUser.FullName).Take(3)) + 
                             (g.Count() > 3 ? $" +{g.Count() - 3}" : "")
                    );
                
                ApplyFilter();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Alarmlar yüklenirken hata: {ex.Message}", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ApplyFilter()
        {
            var searchText = TextBoxSearch.Text.Trim().ToLower();
            var filtered = string.IsNullOrEmpty(searchText)
                ? _allAlarms
                : _allAlarms.Where(a => 
                    a.Name.ToLower().Contains(searchText) || 
                    a.Description.ToLower().Contains(searchText) ||
                    a.SensorName.ToLower().Contains(searchText)).ToList();

            DataGridViewAlarms.DataSource = filtered.Select(a => new AlarmDisplayModel
            {
                Id = a.Id,
                Name = a.Name,
                AlarmUsers = _alarmUserNames.TryGetValue(a.Id, out var users) ? users : "-",
                Description = a.Description,
                Type = GetTypeTurkish(a.Type),
                Priority = GetPriorityTurkish(a.Priority),
                Status = a.IsActive ? "Aktif" : "Pasif"
            }).ToList();

            // Configure columns - headers are set in Designer
            if (DataGridViewAlarms.Columns.Count > 0)
            {
                // No need to hide Id column as it is not auto-generated
            }
        }

        private static string GetTypeTurkish(AlarmType type)
        {
            return type switch
            {
                AlarmType.Threshold => "Eşik Değer",
                AlarmType.Digital => "Dijital",
                AlarmType.System => "Sistem",
                _ => type.ToString()
            };
        }

        private static string GetPriorityTurkish(AlarmPriority priority)
        {
            return priority switch
            {
                AlarmPriority.Low => "Düşük",
                AlarmPriority.Medium => "Orta",
                AlarmPriority.High => "Yüksek",
                AlarmPriority.Critical => "Kritik",
                _ => priority.ToString()
            };
        }

        private void TextBoxSearch_TextChanged(object? sender, EventArgs e)
        {
            ApplyFilter();
        }

        private void ButtonAddNew_Click(object? sender, EventArgs e)
        {
            ShowEditDialog(null);
        }

        private void DataGridViewAlarms_CellContentClick(object? sender, DataGridViewCellEventArgs e)
        {
            // Ignore header row and non-button columns
            if (e.RowIndex < 0) return;
            if (e.ColumnIndex < 0) return;
            
            var column = DataGridViewAlarms.Columns[e.ColumnIndex];
            
            // Only handle button column clicks
            if (column is not DataGridViewButtonColumn) return;

            var columnName = column.Name;
            var row = DataGridViewAlarms.Rows[e.RowIndex];
            
            if (row.DataBoundItem is not AlarmDisplayModel item) return;

            var id = item.Id;
            var name = item.Name;

            if (columnName == "EditColumn")
            {
                ShowEditDialog(id);
            }
            else if (columnName == "DeleteColumn")
            {
                DeleteAlarmAsync(id);
            }
            else if (columnName == "UsersColumn")
            {
                ShowUsersDialog(id, name);
            }
        }

        private void ShowEditDialog(Guid? alarmId)
        {
            using var dialog = new AlarmEditDialog(_scopeFactory, alarmId);
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                _ = LoadDefinitionsAsync();
            }
        }

        private void ShowUsersDialog(Guid alarmId, string alarmName)
        {
            using var dialog = new AlarmUsersEditDialog(_scopeFactory, alarmId, alarmName);
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                _ = LoadDefinitionsAsync();
            }
        }

        private async void DeleteAlarmAsync(Guid id)
        {
            if (MessageBox.Show("Bu alarm tanımını silmek istediğinize emin misiniz?", "Onay", 
                MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes)
                return;

            try
            {
                using var scope = _scopeFactory.CreateScope();
                var dbContext = scope.ServiceProvider.GetRequiredService<IbksDbContext>();
                var alarm = await dbContext.AlarmDefinitions.FindAsync(id);
                if (alarm != null)
                {
                    // Also remove subscriptions
                    var subscriptions = await dbContext.AlarmUserSubscriptions
                        .Where(s => s.AlarmDefinitionId == id)
                        .ToListAsync();
                    dbContext.AlarmUserSubscriptions.RemoveRange(subscriptions);
                    
                    dbContext.AlarmDefinitions.Remove(alarm);
                    await dbContext.SaveChangesAsync();
                    await LoadDefinitionsAsync();
                    MessageBox.Show("Alarm silindi.", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Silme hatası: {ex.Message}", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }

    public class AlarmDisplayModel
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = "";
        public string AlarmUsers { get; set; } = "";
        public string Description { get; set; } = "";
        public string Type { get; set; } = "";
        public string Priority { get; set; } = "";
        public string Status { get; set; } = "";
    }
}
