namespace ISKI.IBKS.Presentation.WinForms.Common.Navigation;

/// <summary>
/// Navigation service interface for switching between pages in the main content area.
/// </summary>
public interface INavigationService
{
    /// <summary>
    /// Navigates to the specified page type.
    /// </summary>
    /// <typeparam name="TPage">The type of UserControl page to navigate to.</typeparam>
    void NavigateTo<TPage>() where TPage : UserControl;

    /// <summary>
    /// Gets the currently displayed page, if any.
    /// </summary>
    UserControl? CurrentPage { get; }

    /// <summary>
    /// Event raised when navigation occurs.
    /// </summary>
    event EventHandler<NavigationEventArgs>? Navigated;
}

/// <summary>
/// Event arguments for navigation events.
/// </summary>
public class NavigationEventArgs : EventArgs
{
    /// <summary>
    /// The page being navigated to.
    /// </summary>
    public UserControl NewPage { get; }

    /// <summary>
    /// The page being navigated from (null if no previous page).
    /// </summary>
    public UserControl? OldPage { get; }

    public NavigationEventArgs(UserControl newPage, UserControl? oldPage)
    {
        NewPage = newPage;
        OldPage = oldPage;
    }
}
