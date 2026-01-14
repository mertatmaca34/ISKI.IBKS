using ISKI.IBKS.Domain.Entities;
using ISKI.IBKS.Persistence.Contexts;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace ISKI.IBKS.Presentation.WinForms.Features.MailPage.Controls
{
    public partial class MailUsersControl : UserControl
    {
        private DataGridView DataGridViewUsers;
        private TextBox TextBoxFullName;
        private TextBox TextBoxEmail;
        private TextBox TextBoxPhone;
        private TextBox TextBoxDepartment;
        private Button ButtonAddUser;
        private Button ButtonDeleteUser;
        private GroupBox groupBoxAddUser;
        private Label LabelFullName;
        private Label LabelEmail;
        private Label LabelPhone;
        private Label LabelDepartment;
        
        private readonly IServiceScopeFactory _scopeFactory;

        public MailUsersControl(IServiceScopeFactory scopeFactory)
        {
            _scopeFactory = scopeFactory;
            InitializeComponent();
            Load += MailUsersControl_Load;
        }

        private void InitializeComponent()
        {
            groupBoxAddUser = new GroupBox();
            LabelFullName = new Label();
            TextBoxFullName = new TextBox();
            LabelEmail = new Label();
            TextBoxEmail = new TextBox();
            LabelPhone = new Label();
            TextBoxPhone = new TextBox();
            LabelDepartment = new Label();
            TextBoxDepartment = new TextBox();
            ButtonAddUser = new Button();
            ButtonDeleteUser = new Button();
            DataGridViewUsers = new DataGridView();

            ((System.ComponentModel.ISupportInitialize)DataGridViewUsers).BeginInit();
            groupBoxAddUser.SuspendLayout();
            SuspendLayout();

            // groupBoxAddUser
            groupBoxAddUser.Controls.Add(LabelFullName);
            groupBoxAddUser.Controls.Add(TextBoxFullName);
            groupBoxAddUser.Controls.Add(LabelEmail);
            groupBoxAddUser.Controls.Add(TextBoxEmail);
            groupBoxAddUser.Controls.Add(LabelPhone);
            groupBoxAddUser.Controls.Add(TextBoxPhone);
            groupBoxAddUser.Controls.Add(LabelDepartment);
            groupBoxAddUser.Controls.Add(TextBoxDepartment);
            groupBoxAddUser.Controls.Add(ButtonAddUser);
            groupBoxAddUser.Dock = DockStyle.Top;
            groupBoxAddUser.Height = 200;
            groupBoxAddUser.Text = "Yeni Kullanıcı Ekle";

            LabelFullName.Location = new Point(20, 30);
            LabelFullName.Text = "Ad Soyad:";
            LabelFullName.AutoSize = true;
            TextBoxFullName.Location = new Point(100, 27);
            TextBoxFullName.Size = new Size(300, 23);

            LabelEmail.Location = new Point(20, 60);
            LabelEmail.Text = "E-posta:";
            LabelEmail.AutoSize = true;
            TextBoxEmail.Location = new Point(100, 57);
            TextBoxEmail.Size = new Size(300, 23);

            LabelPhone.Location = new Point(20, 90);
            LabelPhone.Text = "Telefon:";
            LabelPhone.AutoSize = true;
            TextBoxPhone.Location = new Point(100, 87);
            TextBoxPhone.Size = new Size(300, 23);

            LabelDepartment.Location = new Point(20, 120);
            LabelDepartment.Text = "Birim:";
            LabelDepartment.AutoSize = true;
            TextBoxDepartment.Location = new Point(100, 117);
            TextBoxDepartment.Size = new Size(300, 23);

            ButtonAddUser.Location = new Point(100, 150);
            ButtonAddUser.Text = "KULLANICI EKLE";
            ButtonAddUser.Size = new Size(300, 30);
            ButtonAddUser.BackColor = Color.FromArgb(0, 131, 200);
            ButtonAddUser.ForeColor = Color.White;
            ButtonAddUser.FlatStyle = FlatStyle.Flat;
            ButtonAddUser.Click += ButtonAddUser_Click;

            // DataGridViewUsers
            DataGridViewUsers.Dock = DockStyle.Fill;
            DataGridViewUsers.AllowUserToAddRows = false;
            DataGridViewUsers.AllowUserToDeleteRows = false;
            DataGridViewUsers.ReadOnly = true;
            DataGridViewUsers.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            DataGridViewUsers.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            DataGridViewUsers.BackgroundColor = Color.White;

            // ButtonDeleteUser
            ButtonDeleteUser.Dock = DockStyle.Bottom;
            ButtonDeleteUser.Height = 40;
            ButtonDeleteUser.Text = "SEÇİLİ KULLANICIYI SİL";
            ButtonDeleteUser.BackColor = Color.FromArgb(200, 50, 50);
            ButtonDeleteUser.ForeColor = Color.White;
            ButtonDeleteUser.FlatStyle = FlatStyle.Flat;
            ButtonDeleteUser.Click += ButtonDeleteUser_Click;

            Controls.Add(DataGridViewUsers);
            Controls.Add(groupBoxAddUser);
            Controls.Add(ButtonDeleteUser);

            ((System.ComponentModel.ISupportInitialize)DataGridViewUsers).EndInit();
            groupBoxAddUser.ResumeLayout(false);
            groupBoxAddUser.PerformLayout();
            ResumeLayout(false);
        }

        private async void MailUsersControl_Load(object? sender, EventArgs e)
        {
            await LoadDataAsync();
        }

        private async Task LoadDataAsync()
        {
             try 
             {
                using var scope = _scopeFactory.CreateScope();
                var dbContext = scope.ServiceProvider.GetRequiredService<IbksDbContext>();
                var users = await dbContext.AlarmUsers.ToListAsync();
                DataGridViewUsers.DataSource = users;
             }
             catch (Exception ex)
             {
                 MessageBox.Show($"Hata: {ex.Message}");
             }
        }

        private async void ButtonAddUser_Click(object? sender, EventArgs e)
        {
            var fullName = TextBoxFullName.Text.Trim();
            var email = TextBoxEmail.Text.Trim();
            var phone = TextBoxPhone.Text.Trim();
            var dept = TextBoxDepartment.Text.Trim();

            if (string.IsNullOrEmpty(fullName) || string.IsNullOrEmpty(email))
            {
                MessageBox.Show("Ad Soyad ve E-posta gereklidir.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                 using var scope = _scopeFactory.CreateScope();
                 var dbContext = scope.ServiceProvider.GetRequiredService<IbksDbContext>();

                 var newUser = new AlarmUser(fullName, email, phone, dept, "Personel");
                 dbContext.AlarmUsers.Add(newUser);
                 await dbContext.SaveChangesAsync();

                 MessageBox.Show($"Kullanıcı '{fullName}' eklendi.", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
                 TextBoxFullName.Clear();
                 TextBoxEmail.Clear();
                 TextBoxPhone.Clear();
                 TextBoxDepartment.Clear();
                 await LoadDataAsync();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Hata: {ex.Message}", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private async void ButtonDeleteUser_Click(object? sender, EventArgs e)
        {
            if (DataGridViewUsers.SelectedRows.Count == 0)
            {
                MessageBox.Show("Lütfen silmek için bir kullanıcı seçin.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var result = MessageBox.Show("Seçili kullanıcıyı silmek istediğinize emin misiniz?", "Onay", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (result == DialogResult.Yes)
            {
                try 
                {
                    var selectedUser = (AlarmUser)DataGridViewUsers.SelectedRows[0].DataBoundItem;
                    
                    using var scope = _scopeFactory.CreateScope();
                    var dbContext = scope.ServiceProvider.GetRequiredService<IbksDbContext>();
                    
                    var user = await dbContext.AlarmUsers.FindAsync(selectedUser.Id);
                    if (user != null)
                    {
                        dbContext.AlarmUsers.Remove(user);
                        await dbContext.SaveChangesAsync();
                        MessageBox.Show("Kullanıcı silindi.", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        await LoadDataAsync();
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Hata: {ex.Message}", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }
    }
}
