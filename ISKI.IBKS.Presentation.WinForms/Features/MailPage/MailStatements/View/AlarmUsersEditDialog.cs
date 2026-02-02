using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using ISKI.IBKS.Domain.Entities;
using ISKI.IBKS.Domain.Enums;
using ISKI.IBKS.Infrastructure.Persistence.Contexts;
using ISKI.IBKS.Presentation.WinForms.Features.MailPage.MailUsers.Model;

namespace ISKI.IBKS.Presentation.WinForms.Features.MailPage.MailStatements.View
{
    public partial class AlarmUsersEditDialog : Form
    {
        private readonly IServiceScopeFactory _scopeFactory;
        private readonly Guid _alarmDefinitionId;
        private List<AlarmUser> _allUsers = new();
        private HashSet<Guid> _subscribedUserIds = new();

        public AlarmUsersEditDialog(IServiceScopeFactory scopeFactory, Guid alarmDefinitionId, string alarmName)
        {
            _scopeFactory = scopeFactory;
            _alarmDefinitionId = alarmDefinitionId;
            InitializeComponent();
            Text = $"Alarm Kullanıcıları Listesi: - {alarmName}";
            Load += AlarmUsersEditDialog_Load;
            ButtonSave.Click += ButtonSave_Click;
        }

        private async void AlarmUsersEditDialog_Load(object? sender, EventArgs e)
        {
            try
            {
                using var scope = _scopeFactory.CreateScope();
                var dbContext = scope.ServiceProvider.GetRequiredService<IbksDbContext>();

                _allUsers = await dbContext.AlarmUsers
                    .Where(u => u.IsActive)
                    .OrderBy(u => u.FullName)
                    .ToListAsync();

                var subscriptions = await dbContext.AlarmUserSubscriptions
                    .Where(s => s.AlarmDefinitionId == _alarmDefinitionId && s.IsActive)
                    .ToListAsync();

                _subscribedUserIds = subscriptions.Select(s => s.AlarmUserId).ToHashSet();

                CheckedListBoxUsers.Items.Clear();
                foreach (var user in _allUsers)
                {
                    var isChecked = _subscribedUserIds.Contains(user.Id);
                    CheckedListBoxUsers.Items.Add(new UserItem(user.Id, user.FullName), isChecked);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Kullanıcılar yüklenirken hata: {ex.Message}", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private async void ButtonSave_Click(object? sender, EventArgs e)
        {
            try
            {
                using var scope = _scopeFactory.CreateScope();
                var dbContext = scope.ServiceProvider.GetRequiredService<IbksDbContext>();

                var selectedUserIds = new HashSet<Guid>();
                foreach (var item in CheckedListBoxUsers.CheckedItems)
                {
                    if (item is UserItem userItem)
                    {
                        selectedUserIds.Add(userItem.UserId);
                    }
                }

                var existingSubscriptions = await dbContext.AlarmUserSubscriptions
                    .Where(s => s.AlarmDefinitionId == _alarmDefinitionId)
                    .ToListAsync();

                foreach (var sub in existingSubscriptions)
                {
                    if (!selectedUserIds.Contains(sub.AlarmUserId))
                    {
                        dbContext.AlarmUserSubscriptions.Remove(sub);
                    }
                }

                var existingUserIds = existingSubscriptions.Select(s => s.AlarmUserId).ToHashSet();
                foreach (var userId in selectedUserIds)
                {
                    if (!existingUserIds.Contains(userId))
                    {
                        var newSub = new AlarmUserSubscription(_alarmDefinitionId, userId);
                        dbContext.AlarmUserSubscriptions.Add(newSub);
                    }
                    else
                    {
                        var existingSub = existingSubscriptions.First(s => s.AlarmUserId == userId);
                        if (!existingSub.IsActive)
                        {
                            existingSub.Activate();
                        }
                    }
                }

                await dbContext.SaveChangesAsync();
                DialogResult = DialogResult.OK;
                Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Kayıt Hatası: {ex.Message}", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
