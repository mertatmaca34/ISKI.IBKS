namespace ISKI.IBKS.Presentation.WinForms.Features.Main;

/// <summary>
/// Presenter for the main form. Manages any business logic that doesn't belong in the view.
/// Navigation is handled directly by MainForm via INavigationService.
/// </summary>
public class MainFormPresenter
{
    private readonly IMainFormView _view;

    public MainFormPresenter(IMainFormView view)
    {
        _view = view;
    }
}

