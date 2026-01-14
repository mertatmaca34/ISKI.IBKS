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
    public partial class MailUsersPage : UserControl
    {
        private readonly IServiceScopeFactory _scopeFactory;
        private Guid? _selectedUserId = null;

        public MailUsersPage(IServiceScopeFactory scopeFactory)
        {
            InitializeComponent();
            _scopeFactory = scopeFactory;
            
            Load += MailUsersPage_Load;
            ButtonSave.Click += ButtonSave_Click;
            DataGridViewUsers.SelectionChanged += DataGridViewUsers_SelectionChanged;
            SilToolStripMenuItem.Click += SilToolStripMenuItem_Click;
        }

        // Default constructor for Designer support
        public MailUsersPage()
        {
            InitializeComponent();
        }

        private async void MailUsersPage_Load(object? sender, EventArgs e)
        {
            if (_scopeFactory == null) return;
            await LoadUsersAsync();
        }

        private async Task LoadUsersAsync()
        {
            try
            {
                using var scope = _scopeFactory.CreateScope();
                var dbContext = scope.ServiceProvider.GetRequiredService<IbksDbContext>();
                var users = await dbContext.AlarmUsers.Where(u => u.IsActive).ToListAsync();

                DataGridViewUsers.DataSource = users.Select(u => new 
                {
                    u.Id,
                    u.FullName,
                    u.Email,
                    u.IsActive
                }).ToList();

                DataGridViewUsers.Columns["Id"].Visible = false;
                DataGridViewUsers.Columns["FullName"].HeaderText = "Ad Soyad";
                DataGridViewUsers.Columns["Email"].HeaderText = "E-Posta";
                DataGridViewUsers.Columns["IsActive"].HeaderText = "Aktif";
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Kullanıcılar yüklenirken hata: {ex.Message}");
            }
        }

        private void DataGridViewUsers_SelectionChanged(object? sender, EventArgs e)
        {
            if (DataGridViewUsers.SelectedRows.Count > 0)
            {
                var row = DataGridViewUsers.SelectedRows[0];
                _selectedUserId = (Guid)row.Cells["Id"].Value;
                var fullName = row.Cells["FullName"].Value.ToString() ?? "";
                var email = row.Cells["Email"].Value.ToString() ?? "";
                
                // Split name if possible or just put in Ad/Soyad
                var names = fullName.Split(' ');
                if (names.Length > 1) 
                {
                    TextBoxAd.Text = string.Join(" ", names.Take(names.Length - 1));
                    TextBoxSoyad.Text = names.Last();
                }
                else
                {
                    TextBoxAd.Text = fullName;
                    TextBoxSoyad.Text = "";
                }
                TextBoxEMail.Text = email;
                ButtonSave.Text = "GÜNCELLE";
            }
            else
            {
                _selectedUserId = null;
                ClearInputs();
                ButtonSave.Text = "KAYDET";
            }
        }

        private void ClearInputs()
        {
            TextBoxAd.Text = "";
            TextBoxSoyad.Text = "";
            TextBoxEMail.Text = "";
            TextBoxPassword.Text = ""; // Not used in Entity but present in UI? AlarmUser doesn't have password. Ignoring.
            _selectedUserId = null;
        }

        private async void ButtonSave_Click(object? sender, EventArgs e)
        {
            if (_scopeFactory == null) return;

            var ad = TextBoxAd.Text.Trim();
            var soyad = TextBoxSoyad.Text.Trim();
            var email = TextBoxEMail.Text.Trim();

            if (string.IsNullOrEmpty(ad) || string.IsNullOrEmpty(email))
            {
                MessageBox.Show("Ad ve E-Posta zorunludur.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var fullName = $"{ad} {soyad}".Trim();

            try
            {
                using var scope = _scopeFactory.CreateScope();
                var dbContext = scope.ServiceProvider.GetRequiredService<IbksDbContext>();

                if (_selectedUserId.HasValue)
                {
                    // Update
                    var user = await dbContext.AlarmUsers.FindAsync(_selectedUserId.Value);
                    if (user != null)
                    {
                        user.Update(fullName, email, null, null, null);
                        await dbContext.SaveChangesAsync();
                        MessageBox.Show("Kullanıcı güncellendi.");
                    }
                }
                else
                {
                    // Add
                    var newUser = new AlarmUser(fullName, email);
                    dbContext.AlarmUsers.Add(newUser);
                    await dbContext.SaveChangesAsync();
                    MessageBox.Show("Kullanıcı eklendi.");
                }

                await LoadUsersAsync();
                ClearInputs();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Kaydetme hatası: {ex.Message}");
            }
        }

        private async void SilToolStripMenuItem_Click(object? sender, EventArgs e)
        {
            if (_scopeFactory == null) return;
            if (DataGridViewUsers.SelectedRows.Count == 0) return;

            var row = DataGridViewUsers.SelectedRows[0];
            var id = (Guid)row.Cells["Id"].Value;

            if (MessageBox.Show("Kullanıcıyı silmek istediğinize emin misiniz?", "Onay", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                try
                {
                    using var scope = _scopeFactory.CreateScope();
                    var dbContext = scope.ServiceProvider.GetRequiredService<IbksDbContext>();
                    var user = await dbContext.AlarmUsers.FindAsync(id);
                    if (user != null)
                    {
                        dbContext.AlarmUsers.Remove(user); // or Deactivate? Entity has IsActive logic.
                        // Ideally Soft Delete or Deactivate. 
                        // User request "Sil" implies delete or deactivate.
                        // I'll call Remove, EF Core might handle soft delete if configured (ApplySoftDeleteQueryFilter exists in DbContext so it supports soft delete).
                        dbContext.AlarmUsers.Remove(user);
                        await dbContext.SaveChangesAsync();
                    }
                    await LoadUsersAsync();
                    ClearInputs();
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Silme hatası: {ex.Message}");
                }
            }
        }
    }
}
