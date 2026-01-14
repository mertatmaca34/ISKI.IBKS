using Microsoft.Extensions.DependencyInjection;

namespace ISKI.IBKS.Presentation.WinForms.Common.Navigation;

/// <summary>
/// Navigation service implementation for switching between pages in the main content area.
/// Uses DI to resolve page instances.
/// </summary>
public class NavigationService : INavigationService
{
    private readonly IServiceProvider _serviceProvider;
    private readonly Panel _contentPanel;

    /// <inheritdoc/>
    public UserControl? CurrentPage { get; private set; }

    /// <inheritdoc/>
    public event EventHandler<NavigationEventArgs>? Navigated;

    /// <summary>
    /// Initializes a new instance of the NavigationService.
    /// </summary>
    /// <param name="serviceProvider">The DI service provider for resolving pages.</param>
    /// <param name="contentPanel">The panel where pages will be displayed.</param>
    public NavigationService(IServiceProvider serviceProvider, Panel contentPanel)
    {
        _serviceProvider = serviceProvider ?? throw new ArgumentNullException(nameof(serviceProvider));
        _contentPanel = contentPanel ?? throw new ArgumentNullException(nameof(contentPanel));
    }

    /// <inheritdoc/>
    public void NavigateTo<TPage>() where TPage : UserControl
    {
        // Resolve the page from DI
        var newPage = _serviceProvider.GetRequiredService<TPage>();

        // Store old page reference for event
        var oldPage = CurrentPage;

        // Clear existing content
        _contentPanel.SuspendLayout();
        
        foreach (Control control in _contentPanel.Controls)
        {
            if (control is IDisposable disposable && control != newPage)
            {
                disposable.Dispose();
            }
        }
        _contentPanel.Controls.Clear();

        // Configure new page
        newPage.Dock = DockStyle.Fill;

        // Add and display new page
        _contentPanel.Controls.Add(newPage);
        CurrentPage = newPage;

        _contentPanel.ResumeLayout();

        // Raise navigation event
        Navigated?.Invoke(this, new NavigationEventArgs(newPage, oldPage));
    }
}
