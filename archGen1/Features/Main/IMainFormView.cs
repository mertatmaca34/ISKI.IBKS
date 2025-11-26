using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Iski.IBKS.Presentation.WinForms.Features.Main;

public interface IMainFormView
{
    event EventHandler Load;
    event EventHandler HomePageButtonClick;

    void ShowMert();
}
