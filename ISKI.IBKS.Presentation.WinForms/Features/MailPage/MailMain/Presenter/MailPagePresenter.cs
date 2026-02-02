using ISKI.IBKS.Presentation.WinForms.Features.MailPage.MailStatements.View;
using ISKI.IBKS.Presentation.WinForms.Features.MailPage.MailUsers.View;
using ISKI.IBKS.Presentation.WinForms.Features.MailPage.MailMain.View;

namespace ISKI.IBKS.Presentation.WinForms.Features.MailPage.MailMain.Presenter;

public sealed class MailPagePresenter
{
    private readonly IMailPageView _view;

    public MailPagePresenter(IMailPageView view)
    {
        _view = view;
        _view.Load += (s, e) => _view.SetContent<MailStatementsPage>();
        _view.ShowStatementsRequested += (s, e) => _view.SetContent<MailStatementsPage>();
        _view.ShowUsersRequested += (s, e) => _view.SetContent<MailUsersPage>();
    }
}
