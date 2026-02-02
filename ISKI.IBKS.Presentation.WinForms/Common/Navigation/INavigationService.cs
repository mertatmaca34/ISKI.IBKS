namespace ISKI.IBKS.Presentation.WinForms.Common.Navigation;

public interface INavigationService
{
    void NavigateTo<TPage>() where TPage : UserControl;

    UserControl? CurrentPage { get; }

    event EventHandler<NavigationEventArgs>? Navigated;
}

