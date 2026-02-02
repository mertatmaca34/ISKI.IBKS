using System;
using System.Collections.Generic;
using ISKI.IBKS.Presentation.WinForms.Features.MailPage.MailUsers.Model;

namespace ISKI.IBKS.Presentation.WinForms.Features.MailPage.MailUsers.View;

public interface IMailUsersPageView
{
    event EventHandler Load;
    event EventHandler<string> SearchTextChanged;
    event EventHandler AddNewRequested;
    event EventHandler<Guid> EditRequested;
    event EventHandler<Guid> DeleteRequested;

    void DisplayUsers(IEnumerable<UserDisplayModel> users);
    void ShowError(string message);
    void ShowInfo(string message);
    bool ConfirmDelete();
    bool ShowEditDialog(Guid? userId);
}
