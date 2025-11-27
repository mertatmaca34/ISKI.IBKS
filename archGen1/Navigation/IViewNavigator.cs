using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ISKI.IBKS.Presentation.WinForms.Navigation;

public interface IViewNavigator
{
    void Navigate<TView>(Control panel) where TView : UserControl;
}
