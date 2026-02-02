using System;
using System.Windows.Forms;

namespace ISKI.IBKS.Presentation.WinForms.Features.MailPage.MailMain.View;

public interface IMailPageView
{
    event EventHandler Load;
    event EventHandler ShowStatementsRequested;
    event EventHandler ShowUsersRequested;

    void SetContent<T>() where T : UserControl;
}
