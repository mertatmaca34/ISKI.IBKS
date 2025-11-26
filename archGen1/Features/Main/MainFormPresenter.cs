using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Iski.IBKS.Presentation.WinForms.Features.Main;

public class MainFormPresenter
{
    public MainFormPresenter(IMainFormView view)
    {
        view.Load += View_Load;
        view.HomePageButtonClick += View_HomePageButtonClick;
    }

    private void View_HomePageButtonClick(object? sender, EventArgs e)
    {
        MessageBox.Show("Mert Tıkladı");
    }

    private void View_Load(object? sender, EventArgs e)
    {

        //MessageBox.Show("Mert Başladı");
    }
}
