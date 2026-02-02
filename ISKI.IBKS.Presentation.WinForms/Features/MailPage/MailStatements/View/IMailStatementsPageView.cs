using System;
using System.Collections.Generic;
using ISKI.IBKS.Presentation.WinForms.Features.MailPage.MailStatements.Model;

namespace ISKI.IBKS.Presentation.WinForms.Features.MailPage.MailStatements.View;

public interface IMailStatementsPageView
{
    event EventHandler Load;
    event EventHandler<string> SearchTextChanged;
    event EventHandler AddNewRequested;
    event EventHandler<Guid> EditRequested;
    event EventHandler<Guid> DeleteRequested;
    event EventHandler<Guid> ManageUsersRequested;

    void DisplayAlarms(IEnumerable<AlarmDisplayModel> alarms);
    void ShowError(string message);
    void ShowInfo(string message);
    bool ConfirmDelete();
    bool ShowEditDialog(Guid? alarmId);
    bool ShowUsersEditDialog(Guid alarmId, string alarmName);
}
