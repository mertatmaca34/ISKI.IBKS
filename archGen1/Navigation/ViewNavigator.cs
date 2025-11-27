using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace ISKI.IBKS.Presentation.WinForms.Navigation;

public class ViewNavigator : IViewNavigator
{
    private readonly IServiceProvider _serviceProvider;

    public ViewNavigator(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
    }

    public void Navigate<TView>(Control panel) where TView : UserControl
    {
        var view = _serviceProvider.GetRequiredService<TView>();

        panel.Controls.Clear();
        panel.Controls.Add(view);

        view.Dock = DockStyle.Fill;
    }
}
