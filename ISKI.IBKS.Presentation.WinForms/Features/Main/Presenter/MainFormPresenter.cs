using ISKI.IBKS.Presentation.WinForms.Features.Main.View;

namespace ISKI.IBKS.Presentation.WinForms.Features.Main.Presenter;

public class MainFormPresenter
{
    private readonly IMainFormView _view;

    public MainFormPresenter(IMainFormView view)
    {
        _view = view;
    }
}

