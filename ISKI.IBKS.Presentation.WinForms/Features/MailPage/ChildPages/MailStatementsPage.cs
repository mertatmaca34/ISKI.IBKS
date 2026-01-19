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

namespace ISKI.IBKS.Presentation.WinForms.Features.MailPage.ChildPages
{
    public partial class MailStatementsPage : UserControl
    {
        private readonly IServiceScopeFactory _scopeFactory;
        private Guid? _selectedUserId = null;

        public MailStatementsPage(IServiceScopeFactory scopeFactory)
        {
            InitializeComponent();
            _scopeFactory = scopeFactory;
            
            Load += MailStatementsPage_Load;
            ComboBoxSelectedUser.SelectedIndexChanged += ComboBoxSelectedUser_SelectedIndexChanged;
            
            // Standard Checkbox handling pattern
            DataGridViewMailStatements.CurrentCellDirtyStateChanged += DataGridViewMailStatements_CurrentCellDirtyStateChanged;
            DataGridViewMailStatements.CellValueChanged += DataGridViewMailStatements_CellValueChanged;
        }

        public MailStatementsPage()
        {
            InitializeComponent();
            _scopeFactory = null!;
        }

        private async void MailStatementsPage_Load(object? sender, EventArgs e)
        {
            if (_scopeFactory == null) return;
            
            // Allow checkbox interaction
            DataGridViewMailStatements.ReadOnly = false;
            foreach (DataGridViewColumn col in DataGridViewMailStatements.Columns)
            {
                if (col.Name == "ComboBoxSec") col.ReadOnly = false;
                else col.ReadOnly = true; 
            }

            await LoadUsersAsync();
        }

        private async Task LoadUsersAsync()
        {
            try
            {
                using var scope = _scopeFactory.CreateScope();
                var dbContext = scope.ServiceProvider.GetRequiredService<IbksDbContext>();
                var users = await dbContext.AlarmUsers.Where(u => u.IsActive).ToListAsync();

                ComboBoxSelectedUser.DataSource = users;
                ComboBoxSelectedUser.DisplayMember = "FullName";
                ComboBoxSelectedUser.ValueMember = "Id";
                ComboBoxSelectedUser.SelectedIndex = -1;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Kullanıcılar yüklenirken hata: {ex.Message}");
            }
        }

        private async void ComboBoxSelectedUser_SelectedIndexChanged(object? sender, EventArgs e)
        {
            if (ComboBoxSelectedUser.SelectedValue is Guid userId)
            {
                _selectedUserId = userId;
                await LoadSubscriptionsAsync(userId);
            }
            else
            {
                _selectedUserId = null;
                DataGridViewMailStatements.DataSource = null;
            }
        }

        private async Task LoadSubscriptionsAsync(Guid userId)
        {
            try
            {
                // Temporarily detach event to prevent firing during load
                DataGridViewMailStatements.CellValueChanged -= DataGridViewMailStatements_CellValueChanged;

                using var scope = _scopeFactory.CreateScope();
                var dbContext = scope.ServiceProvider.GetRequiredService<IbksDbContext>();
                
                var alarms = await dbContext.AlarmDefinitions.Where(a => a.IsActive).OrderBy(a => a.SensorName).ToListAsync();
                var subs = await dbContext.AlarmUserSubscriptions.Where(s => s.AlarmUserId == userId && s.IsActive).ToListAsync();
                var subIds = subs.Select(s => s.AlarmDefinitionId).ToHashSet();

                // Use BindingList for better two-way binding support
                var list = new BindingList<SubscriptionViewModel>(alarms.Select(a => new SubscriptionViewModel
                {
                    AlarmId = a.Id,
                    SensorName = a.SensorName, 
                    AlarmName = a.Name,
                    IsSelected = subIds.Contains(a.Id)
                }).ToList());

                DataGridViewMailStatements.AutoGenerateColumns = false;
                DataGridViewMailStatements.DataSource = list;
                
                // Map columns
                if (DataGridViewMailStatements.Columns.Contains("ComboBoxSec"))
                {
                    DataGridViewMailStatements.Columns["ComboBoxSec"].DataPropertyName = "IsSelected";
                    DataGridViewMailStatements.Columns["ComboBoxSec"].ReadOnly = false; 
                }
                
                // Add hidden columns for IDs if not present 
                if (!DataGridViewMailStatements.Columns.Contains("AlarmId"))
                {
                    DataGridViewMailStatements.Columns.Add(new DataGridViewTextBoxColumn 
                    { 
                        Name = "AlarmId", 
                        DataPropertyName = "AlarmId",
                        Visible = false 
                    });
                }
                
                if (!DataGridViewMailStatements.Columns.Contains("SensorName"))
                {
                     DataGridViewMailStatements.Columns.Add(new DataGridViewTextBoxColumn 
                    { 
                        Name = "SensorName", 
                        DataPropertyName = "SensorName",
                        HeaderText = "Parametre",
                        ReadOnly = true
                    });
                }
                
                if (!DataGridViewMailStatements.Columns.Contains("AlarmName"))
                {
                     DataGridViewMailStatements.Columns.Add(new DataGridViewTextBoxColumn 
                    { 
                        Name = "AlarmName", 
                        DataPropertyName = "AlarmName",
                        HeaderText = "Alarm Adı",
                        ReadOnly = true
                    });
                }

                 if (DataGridViewMailStatements.Columns.Contains("SensorName")) DataGridViewMailStatements.Columns["SensorName"].HeaderText = "Parametre";
                 if (DataGridViewMailStatements.Columns.Contains("AlarmName")) DataGridViewMailStatements.Columns["AlarmName"].HeaderText = "Alarm Adı";
            }
            catch (Exception ex)
            {
                 MessageBox.Show($"Abonelikler yüklenirken hata: {ex.Message}");
            }
            finally
            {
                // Re-attach event
                DataGridViewMailStatements.CellValueChanged += DataGridViewMailStatements_CellValueChanged;
            }
        }

        private void DataGridViewMailStatements_CurrentCellDirtyStateChanged(object? sender, EventArgs e)
        {
            if (DataGridViewMailStatements.IsCurrentCellDirty)
            {
                // This triggers the CellValueChanged event immediately when checkbox is clicked
                DataGridViewMailStatements.CommitEdit(DataGridViewDataErrorContexts.Commit);
            }
        }

        private async void DataGridViewMailStatements_CellValueChanged(object? sender, DataGridViewCellEventArgs e)
        {
            if (_selectedUserId == null) return;
            if (e.RowIndex < 0) return;

            // Only act if the checkbox column changed
            if (DataGridViewMailStatements.Columns[e.ColumnIndex].Name == "ComboBoxSec")
            {
                var row = DataGridViewMailStatements.Rows[e.RowIndex];
                var item = (SubscriptionViewModel)row.DataBoundItem;
                
                // Item is already updated by binding thanks to CommitEdit
                bool isSelected = item.IsSelected;
                
                try
                {
                    using var scope = _scopeFactory.CreateScope();
                    var dbContext = scope.ServiceProvider.GetRequiredService<IbksDbContext>();
                    
                    var alarmName = item.AlarmName;
                    var userName = ComboBoxSelectedUser.Text; // Assuming DisplayMember is FullName
                    
                    if (isSelected)
                    {
                        // Add
                        if (!await dbContext.AlarmUserSubscriptions.AnyAsync(s => s.AlarmUserId == _selectedUserId && s.AlarmDefinitionId == item.AlarmId))
                        {
                            dbContext.AlarmUserSubscriptions.Add(new AlarmUserSubscription(item.AlarmId, _selectedUserId.Value));
                            await dbContext.SaveChangesAsync();
                            
                            MessageBox.Show($"'{alarmName}' alarmı '{userName}' kullanıcısına başarıyla tanımlandı.", "İşlem Başarılı", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                    }
                    else
                    {
                        // Remove
                        var sub = await dbContext.AlarmUserSubscriptions.FirstOrDefaultAsync(s => s.AlarmUserId == _selectedUserId && s.AlarmDefinitionId == item.AlarmId);
                        if (sub != null)
                        {
                            dbContext.AlarmUserSubscriptions.Remove(sub);
                            await dbContext.SaveChangesAsync();
                            
                            MessageBox.Show($"'{alarmName}' alarmı '{userName}' kullanıcısından kaldırıldı.", "İşlem Başarılı", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"İşlem hatası: {ex.Message}");
                    // Revert UI if persistence failed? 
                    // Ideally yes, but keeping it simple for now. 
                    // Could detach event, toggle back, reattach.
                }
            }
        }
    }

    public class SubscriptionViewModel
    {
        public Guid AlarmId { get; set; }
        public string SensorName { get; set; } = "";
        public string AlarmName { get; set; } = "";
        public bool IsSelected { get; set; }
    }
}
