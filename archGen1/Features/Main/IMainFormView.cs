using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace archGen1.Features.Main;

public interface IMainFormView
{
    event EventHandler Load;
    event EventHandler HomePageButtonClick;

    void ShowMert();
}
