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
    public partial class MailUsersPage : UserControl
    {
        private readonly IServiceScopeFactory _scopeFactory;
        private List<AlarmUser> _allUsers = new();

        public MailUsersPage(IServiceScopeFactory scopeFactory)
        {
            _scopeFactory = scopeFactory;
            InitializeComponent();
            
            // Attach event handlers
            Load += MailUsersPage_Load;
            TextBoxSearch.TextChanged += TextBoxSearch_TextChanged;
            ButtonAddNew.Click += ButtonAddNew_Click;
            DataGridViewUsers.CellContentClick += DataGridViewUsers_CellContentClick;
        }

        public MailUsersPage()
        {
            _scopeFactory = null!;
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
                _allUsers = await dbContext.AlarmUsers.OrderBy(u => u.FullName).ToListAsync();
                ApplyFilter();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Kullanıcılar yüklenirken hata: {ex.Message}", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ApplyFilter()
        {
            var searchText = TextBoxSearch.Text.Trim().ToLower();
            var filtered = string.IsNullOrEmpty(searchText)
                ? _allUsers
                : _allUsers.Where(u => 
                    u.FullName.ToLower().Contains(searchText) || 
                    u.Email.ToLower().Contains(searchText) ||
                    (u.Department?.ToLower().Contains(searchText) ?? false) ||
                    (u.Title?.ToLower().Contains(searchText) ?? false)).ToList();

            DataGridViewUsers.DataSource = filtered.Select(u => new UserDisplayModel
            {
                Id = u.Id,
                FullName = u.FullName,
                Email = u.Email,
                PhoneNumber = u.PhoneNumber ?? "",
                Department = u.Department ?? "",
                Title = u.Title ?? "",
                Status = u.IsActive ? "Aktif" : "Pasif",
                EmailNotifications = u.ReceiveEmailNotifications ? "✓" : "✗"
            }).ToList();

            // Configure columns - headers are set in Designer
            if (DataGridViewUsers.Columns.Count > 0)
            {
                DataGridViewUsers.Columns["Id"].Visible = false;
            }
        }

        private void TextBoxSearch_TextChanged(object? sender, EventArgs e)
        {
            ApplyFilter();
        }

        private void ButtonAddNew_Click(object? sender, EventArgs e)
        {
            ShowEditDialog(null);
        }

        private void DataGridViewUsers_CellContentClick(object? sender, DataGridViewCellEventArgs e)
        {
            // Ignore header row and non-button columns
            if (e.RowIndex < 0) return;
            if (e.ColumnIndex < 0) return;
            
            var column = DataGridViewUsers.Columns[e.ColumnIndex];
            
            // Only handle button column clicks
            if (column is not DataGridViewButtonColumn) return;

            var columnName = column.Name;
            var id = (Guid)DataGridViewUsers.Rows[e.RowIndex].Cells["Id"].Value;

            if (columnName == "EditColumn")
            {
                ShowEditDialog(id);
            }
            else if (columnName == "DeleteColumn")
            {
                DeleteUserAsync(id);
            }
        }

        private void ShowEditDialog(Guid? userId)
        {
            using var dialog = new UserEditDialog(_scopeFactory, userId);
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                _ = LoadUsersAsync();
            }
        }

        private async void DeleteUserAsync(Guid id)
        {
            if (MessageBox.Show("Bu kullanıcıyı silmek istediğinize emin misiniz?", "Onay", 
                MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes)
                return;

            try
            {
                using var scope = _scopeFactory.CreateScope();
                var dbContext = scope.ServiceProvider.GetRequiredService<IbksDbContext>();
                var user = await dbContext.AlarmUsers.FindAsync(id);
                if (user != null)
                {
                    dbContext.AlarmUsers.Remove(user);
                    await dbContext.SaveChangesAsync();
                    await LoadUsersAsync();
                    MessageBox.Show("Kullanıcı silindi.", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Silme hatası: {ex.Message}", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }

    public class UserDisplayModel
    {
        public Guid Id { get; set; }
        public string FullName { get; set; } = "";
        public string Email { get; set; } = "";
        public string PhoneNumber { get; set; } = "";
        public string Department { get; set; } = "";
        public string Title { get; set; } = "";
        public string Status { get; set; } = "";
        public string EmailNotifications { get; set; } = "";
    }
}
