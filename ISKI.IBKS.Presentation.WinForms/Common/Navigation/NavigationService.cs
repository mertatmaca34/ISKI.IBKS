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
        // Resolve the page from DI (will return same instance for singletons)
        var newPage = _serviceProvider.GetRequiredService<TPage>();

        // Store old page reference for event
        var oldPage = CurrentPage;

        // If we're already on this page, do nothing
        if (CurrentPage == newPage)
            return;

        // Suspend layout for better performance
        _contentPanel.SuspendLayout();
        
        // Hide all existing pages (don't dispose singletons)
        foreach (Control control in _contentPanel.Controls)
        {
            if (control is UserControl page)
            {
                page.Visible = false;
            }
        }

        // Check if the page is already in the panel
        if (!_contentPanel.Controls.Contains(newPage))
        {
            // Configure new page
            newPage.Dock = DockStyle.Fill;
            
            // Add to panel
            _contentPanel.Controls.Add(newPage);
        }

        // Show the page
        newPage.Visible = true;
        newPage.BringToFront();
        CurrentPage = newPage;

        _contentPanel.ResumeLayout();

        // Raise navigation event
        Navigated?.Invoke(this, new NavigationEventArgs(newPage, oldPage));
    }
}
