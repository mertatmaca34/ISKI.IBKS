using System.Windows.Forms;
using Microsoft.Extensions.DependencyInjection;
using ISKI.IBKS.Shared.Localization;
using ISKI.IBKS.Presentation.WinForms.Features.MailPage.MailMain.Presenter;

namespace ISKI.IBKS.Presentation.WinForms.Features.MailPage.MailMain.View;

public partial class MailPage : UserControl, IMailPageView
{
    private readonly IServiceProvider _serviceProvider;

    public event EventHandler ShowStatementsRequested;
    public event EventHandler ShowUsersRequested;

    public MailPage(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
        InitializeComponent();
        InitializeLocalization();
        ActivatorUtilities.CreateInstance<MailPagePresenter>(serviceProvider, this);
        
        ButtonMailStatements.Click += (s, e) => ShowStatementsRequested?.Invoke(this, EventArgs.Empty);
        ButtonMailUsers.Click += (s, e) => ShowUsersRequested?.Invoke(this, EventArgs.Empty);
    }

    private void InitializeLocalization()
    {
        ButtonMailStatements.Text = Strings.Mail_StatementsTitle;
        ButtonMailUsers.Text = Strings.Mail_UsersTitle;
    }

    public void SetContent<T>() where T : UserControl
    {
        PanelContent.Controls.Clear();
        var control = _serviceProvider.GetService<T>() ?? ActivatorUtilities.CreateInstance<T>(_serviceProvider);
        control.Dock = DockStyle.Fill;
        PanelContent.Controls.Add(control);
    }
}
