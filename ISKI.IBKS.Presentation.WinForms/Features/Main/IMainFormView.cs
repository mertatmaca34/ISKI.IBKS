using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ISKI.IBKS.Presentation.WinForms.Features.Main;

public interface IMainFormView
{
    event EventHandler Load;
    event EventHandler HomePageButtonClick;

    Panel PanelContainer { get; }
    void ShowMert();
}
