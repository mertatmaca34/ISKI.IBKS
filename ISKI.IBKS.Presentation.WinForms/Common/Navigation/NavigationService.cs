using Microsoft.Extensions.DependencyInjection;

namespace ISKI.IBKS.Presentation.WinForms.Common.Navigation;

public class NavigationService : INavigationService
{
    private readonly IServiceProvider _serviceProvider;
    private readonly Panel _contentPanel;

    public UserControl? CurrentPage { get; private set; }

    public event EventHandler<NavigationEventArgs>? Navigated;

    public NavigationService(IServiceProvider serviceProvider, Panel contentPanel)
    {
        _serviceProvider = serviceProvider ?? throw new ArgumentNullException(nameof(serviceProvider));
        _contentPanel = contentPanel ?? throw new ArgumentNullException(nameof(contentPanel));
    }

    public void NavigateTo<TPage>() where TPage : UserControl
    {
        var newPage = _serviceProvider.GetRequiredService<TPage>();

        var oldPage = CurrentPage;

        if (CurrentPage == newPage)
            return;

        _contentPanel.SuspendLayout();

        foreach (Control control in _contentPanel.Controls)
        {
            if (control is UserControl page)
            {
                page.Visible = false;
            }
        }

        if (!_contentPanel.Controls.Contains(newPage))
        {
            newPage.Dock = DockStyle.Fill;

            _contentPanel.Controls.Add(newPage);
        }

        newPage.Visible = true;
        newPage.BringToFront();
        CurrentPage = newPage;

        _contentPanel.ResumeLayout();

        Navigated?.Invoke(this, new NavigationEventArgs(newPage, oldPage));
    }
}

