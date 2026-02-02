using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using ISKI.IBKS.Shared.Localization;
using Microsoft.Extensions.DependencyInjection;
using ISKI.IBKS.Presentation.WinForms.Features.MailPage.MailUsers.Presenter;
using ISKI.IBKS.Presentation.WinForms.Features.MailPage.MailUsers.Model;

namespace ISKI.IBKS.Presentation.WinForms.Features.MailPage.MailUsers.View;

public partial class MailUsersPage : UserControl, IMailUsersPageView
{
    private readonly IServiceProvider _serviceProvider;
    
    public event EventHandler<string> SearchTextChanged;
    public event EventHandler AddNewRequested;
    public event EventHandler<Guid> EditRequested;
    public event EventHandler<Guid> DeleteRequested;

    public MailUsersPage(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
        InitializeComponent();

        TextBoxSearch.TextChanged += (s, e) => SearchTextChanged?.Invoke(this, TextBoxSearch.Text);
        ButtonAddNew.Click += (s, e) => AddNewRequested?.Invoke(this, EventArgs.Empty);
        DataGridViewUsers.CellContentClick += DataGridViewUsers_CellContentClick;

        // Instantiate presenter
        var scopeFactory = serviceProvider.GetRequiredService<IServiceScopeFactory>();
        _ = new MailUsersPagePresenter(scopeFactory, this);
    }

    public void DisplayUsers(IEnumerable<UserDisplayModel> users)
    {
        if (InvokeRequired)
        {
            Invoke(new Action(() => DisplayUsers(users)));
            return;
        }
        DataGridViewUsers.DataSource = users.ToList();
        if (DataGridViewUsers.Columns.Count > 0 && DataGridViewUsers.Columns.Contains("Id"))
        {
            DataGridViewUsers.Columns["Id"].Visible = false;
        }
    }

    public void ShowError(string message)
    {
        MessageBox.Show(message, Strings.Common_Error, MessageBoxButtons.OK, MessageBoxIcon.Error);
    }

    public void ShowInfo(string message)
    {
        MessageBox.Show(message, Strings.Common_Information, MessageBoxButtons.OK, MessageBoxIcon.Information);
    }

    public bool ConfirmDelete()
    {
        return MessageBox.Show(Strings.Mail_ConfirmDeleteUser, Strings.Common_Confirmation,
            MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes;
    }

    private void DataGridViewUsers_CellContentClick(object? sender, DataGridViewCellEventArgs e)
    {
        if (e.RowIndex < 0) return;
        var column = DataGridViewUsers.Columns[e.ColumnIndex];
        if (column is not DataGridViewButtonColumn) return;

        // Get the Id from the bound data item
        if (DataGridViewUsers.Rows[e.RowIndex].DataBoundItem is UserDisplayModel model)
        {
            var id = model.Id;
            if (column.Name == "EditColumn")
                EditRequested?.Invoke(this, id);
            else if (column.Name == "DeleteColumn")
                DeleteRequested?.Invoke(this, id);
        }
    }

    public bool ShowEditDialog(Guid? userId)
    {
        var scopeFactory = _serviceProvider.GetRequiredService<IServiceScopeFactory>();
        using var dialog = new UserEditDialog(scopeFactory, userId);
        return dialog.ShowDialog() == DialogResult.OK;
    }
}
