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
            DataGridViewMailStatements.CellContentClick += DataGridViewMailStatements_CellContentClick;
        }

        public MailStatementsPage()
        {
            InitializeComponent();
        }

        private async void MailStatementsPage_Load(object? sender, EventArgs e)
        {
            if (_scopeFactory == null) return;
            await LoadUsersAsync();
            
            // Adjust Grid Columns if needed
            // ComboBoxSec is the CheckBoxColumn. 
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
                using var scope = _scopeFactory.CreateScope();
                var dbContext = scope.ServiceProvider.GetRequiredService<IbksDbContext>();
                
                var alarms = await dbContext.AlarmDefinitions.Where(a => a.IsActive).OrderBy(a => a.SensorName).ToListAsync();
                var subs = await dbContext.AlarmUserSubscriptions.Where(s => s.AlarmUserId == userId && s.IsActive).ToListAsync();
                var subIds = subs.Select(s => s.AlarmDefinitionId).ToHashSet();

                var list = alarms.Select(a => new SubscriptionViewModel
                {
                    AlarmId = a.Id,
                    SensorName = a.SensorName, // Parametre
                    AlarmName = a.Name, // Durum/Ad
                    IsSelected = subIds.Contains(a.Id)
                }).ToList();

                DataGridViewMailStatements.DataSource = list;
                
                // Map columns
                // "ComboBoxSec" is the checkbox column name in Designer.
                // We map IsSelected to it.
                // The DataSource binding might AutoGenerate columns.
                // We should ensure DataPropertyName of ComboBoxSec is "IsSelected".
                // Since we can't edit Designer, we set it here.
                if (DataGridViewMailStatements.Columns.Contains("ComboBoxSec"))
                {
                    DataGridViewMailStatements.Columns["ComboBoxSec"].DataPropertyName = "IsSelected";
                }
                
                DataGridViewMailStatements.Columns["AlarmId"].Visible = false;
                DataGridViewMailStatements.Columns["SensorName"].HeaderText = "Parametre";
                DataGridViewMailStatements.Columns["AlarmName"].HeaderText = "Alarm Adı";
                // ComboBoxSec (Seç) should be visible.
            }
            catch (Exception ex)
            {
                 MessageBox.Show($"Abonelikler yüklenirken hata: {ex.Message}");
            }
        }

        private async void DataGridViewMailStatements_CellContentClick(object? sender, DataGridViewCellEventArgs e)
        {
            if (_selectedUserId == null) return;
            if (e.RowIndex < 0) return;

            if (DataGridViewMailStatements.Columns[e.ColumnIndex].Name == "ComboBoxSec")
            {
                var row = DataGridViewMailStatements.Rows[e.RowIndex];
                var item = (SubscriptionViewModel)row.DataBoundItem;
                bool newValue = !item.IsSelected; // Toggle
                
                // Update DB immediately
                try
                {
                    using var scope = _scopeFactory.CreateScope();
                    var dbContext = scope.ServiceProvider.GetRequiredService<IbksDbContext>();
                    
                    if (newValue)
                    {
                        // Add
                        if (!await dbContext.AlarmUserSubscriptions.AnyAsync(s => s.AlarmUserId == _selectedUserId && s.AlarmDefinitionId == item.AlarmId))
                        {
                            dbContext.AlarmUserSubscriptions.Add(new AlarmUserSubscription(item.AlarmId, _selectedUserId.Value));
                            await dbContext.SaveChangesAsync();
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
                        }
                    }

                    // Update UI model
                    item.IsSelected = newValue;
                    DataGridViewMailStatements.RefreshEdit(); // Force refresh checkbox
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"İşlem hatası: {ex.Message}");
                    // Revert UI check if needed, but Grid handles check state display via value.
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
