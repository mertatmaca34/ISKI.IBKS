using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ISKI.IBKS.Presentation.WinForms.Features.HomePage;

public class HomePagePresenter
{
    public HomePagePresenter(IHomePageView view)
    {
        view.Load += OnLoad;
    }

    private void OnLoad(object? sender, EventArgs e)
    {

    }
}
