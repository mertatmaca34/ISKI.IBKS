using ISKI.IBKS.Shared.Localization;
using System;
using System.Drawing;
using System.Windows.Forms;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using ISKI.IBKS.Domain.Entities;
using ISKI.IBKS.Domain.Enums;
using ISKI.IBKS.Infrastructure.Persistence.Contexts;

namespace ISKI.IBKS.Presentation.WinForms.Features.MailPage.MailUsers.View
{
    public partial class UserEditDialog : Form
    {
        private readonly IServiceScopeFactory _scopeFactory;
        private readonly Guid? _userId;

        public UserEditDialog(IServiceScopeFactory scopeFactory, Guid? userId)
        {
            _scopeFactory = scopeFactory;
            _userId = userId;
            InitializeComponent();
            Load += UserEditDialog_Load;
            ButtonSave.Click += ButtonSave_Click;
        }

        private async void UserEditDialog_Load(object? sender, EventArgs e)
        {
            ComboBoxPriority.SelectedIndex = 1;

            if (_userId.HasValue)
            {
                try
                {
                    using var scope = _scopeFactory.CreateScope();
                    var dbContext = scope.ServiceProvider.GetRequiredService<IbksDbContext>();
                    var user = await dbContext.AlarmUsers.FindAsync(_userId.Value);
                    if (user != null)
                    {
                        TextBoxFullName.Text = user.FullName;
                        TextBoxEmail.Text = user.Email;
                        TextBoxPhoneNumber.Text = user.PhoneNumber ?? "";
                        TextBoxDepartment.Text = user.Department ?? "";
                        TextBoxTitle.Text = user.Title ?? "";
                        CheckBoxIsActive.Checked = user.IsActive;
                        CheckBoxReceiveEmail.Checked = user.ReceiveEmailNotifications;
                        ComboBoxPriority.SelectedIndex = (int)user.MinimumPriorityLevel;
                    }
                }
                catch { }
            }
        }

        private async void ButtonSave_Click(object? sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(TextBoxFullName.Text) || string.IsNullOrWhiteSpace(TextBoxEmail.Text))
            {
                MessageBox.Show(Strings.Mail_NameEmailRequired, Strings.Common_Validation, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (!TextBoxEmail.Text.Contains("@"))
            {
                MessageBox.Show(Strings.Mail_InvalidEmail, Strings.Common_Validation, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                using var scope = _scopeFactory.CreateScope();
                var dbContext = scope.ServiceProvider.GetRequiredService<IbksDbContext>();

                var priority = (AlarmPriority)ComboBoxPriority.SelectedIndex;

                if (_userId.HasValue)
                {
                    var user = await dbContext.AlarmUsers.FindAsync(_userId.Value);
                    if (user != null)
                    {
                        user.Update(
                            TextBoxFullName.Text.Trim(),
                            TextBoxEmail.Text.Trim(),
                            string.IsNullOrWhiteSpace(TextBoxPhoneNumber.Text) ? null : TextBoxPhoneNumber.Text.Trim(),
                            string.IsNullOrWhiteSpace(TextBoxDepartment.Text) ? null : TextBoxDepartment.Text.Trim(),
                            string.IsNullOrWhiteSpace(TextBoxTitle.Text) ? null : TextBoxTitle.Text.Trim(),
                            CheckBoxIsActive.Checked,
                            CheckBoxReceiveEmail.Checked,
                            priority
                        );
                    }
                }
                else
                {
                    var newUser = new AlarmUser(
                        TextBoxFullName.Text.Trim(),
                        TextBoxEmail.Text.Trim(),
                        string.IsNullOrWhiteSpace(TextBoxPhoneNumber.Text) ? null : TextBoxPhoneNumber.Text.Trim(),
                        string.IsNullOrWhiteSpace(TextBoxDepartment.Text) ? null : TextBoxDepartment.Text.Trim(),
                        string.IsNullOrWhiteSpace(TextBoxTitle.Text) ? null : TextBoxTitle.Text.Trim()
                    );

                    if (!CheckBoxIsActive.Checked) newUser.Deactivate();
                    newUser.SetNotificationPreferences(CheckBoxReceiveEmail.Checked, priority);

                    dbContext.AlarmUsers.Add(newUser);
                }

                await dbContext.SaveChangesAsync();
                DialogResult = DialogResult.OK;
                Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(string.Format(Strings.Common_SaveError, ex.Message), Strings.Common_Error, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
