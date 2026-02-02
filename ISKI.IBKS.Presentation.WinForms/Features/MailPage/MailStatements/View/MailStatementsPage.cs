using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using ISKI.IBKS.Shared.Localization;
using Microsoft.Extensions.DependencyInjection;
using ISKI.IBKS.Presentation.WinForms.Features.MailPage.MailStatements.Presenter;
using ISKI.IBKS.Presentation.WinForms.Features.MailPage.MailStatements.Model;

namespace ISKI.IBKS.Presentation.WinForms.Features.MailPage.MailStatements.View;

public partial class MailStatementsPage : UserControl, IMailStatementsPageView
{
    private readonly IServiceProvider _serviceProvider;
    
    public event EventHandler<string> SearchTextChanged;
    public event EventHandler AddNewRequested;
    public event EventHandler<Guid> EditRequested;
    public event EventHandler<Guid> DeleteRequested;
    public event EventHandler<Guid> ManageUsersRequested;

    public MailStatementsPage(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
        InitializeComponent();

        TextBoxSearch.TextChanged += (s, e) => SearchTextChanged?.Invoke(this, TextBoxSearch.Text);
        ButtonAddNew.Click += (s, e) => AddNewRequested?.Invoke(this, EventArgs.Empty);
        DataGridViewAlarms.CellContentClick += DataGridViewAlarms_CellContentClick;

        // Instantiate presenter
        var scopeFactory = serviceProvider.GetRequiredService<IServiceScopeFactory>();
        _ = new MailStatementsPagePresenter(scopeFactory, this);
    }

    public void DisplayAlarms(IEnumerable<AlarmDisplayModel> alarms)
    {
        if (InvokeRequired)
        {
            Invoke(new Action(() => DisplayAlarms(alarms)));
            return;
        }
        DataGridViewAlarms.DataSource = alarms.ToList();
        if (DataGridViewAlarms.Columns.Count > 0 && DataGridViewAlarms.Columns.Contains("Id"))
        {
            DataGridViewAlarms.Columns["Id"].Visible = false;
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
        return MessageBox.Show(Strings.Mail_ConfirmDeleteAlarm, Strings.Common_Confirmation,
            MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes;
    }

    private void DataGridViewAlarms_CellContentClick(object? sender, DataGridViewCellEventArgs e)
    {
        if (e.RowIndex < 0) return;
        var column = DataGridViewAlarms.Columns[e.ColumnIndex];
        if (column is not DataGridViewButtonColumn) return;

        // Get the Id from the bound data item
        if (DataGridViewAlarms.Rows[e.RowIndex].DataBoundItem is AlarmDisplayModel model)
        {
            var id = model.Id;
            if (column.Name == "EditColumn")
                EditRequested?.Invoke(this, id);
            else if (column.Name == "DeleteColumn")
                DeleteRequested?.Invoke(this, id);
            else if (column.Name == "UsersColumn")
                ManageUsersRequested?.Invoke(this, id);
        }
    }

    public bool ShowEditDialog(Guid? alarmId)
    {
        var scopeFactory = _serviceProvider.GetRequiredService<IServiceScopeFactory>();
        using var dialog = new AlarmDefinitionEditDialog(scopeFactory, alarmId);
        return dialog.ShowDialog() == DialogResult.OK;
    }

    public bool ShowUsersEditDialog(Guid alarmId, string alarmName)
    {
        var scopeFactory = _serviceProvider.GetRequiredService<IServiceScopeFactory>();
        using var dialog = new AlarmUsersEditDialog(scopeFactory, alarmId, alarmName);
        return dialog.ShowDialog() == DialogResult.OK;
    }
}
