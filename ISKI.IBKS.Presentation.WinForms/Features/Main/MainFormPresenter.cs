using ISKI.IBKS.Presentation.WinForms.Features.HomePage;
using ISKI.IBKS.Presentation.WinForms.Navigation;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ISKI.IBKS.Presentation.WinForms.Features.Main;

public class MainFormPresenter
{
    private readonly IServiceProvider _serviceProvider;
    private readonly IMainFormView _mainFormView;
    private readonly IViewNavigator _viewNavigator;

    public MainFormPresenter(IMainFormView mainFormView, IViewNavigator viewNavigator, IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
        _mainFormView = mainFormView;
        _viewNavigator = viewNavigator;

        _mainFormView.Load += View_Load;
        _mainFormView.HomePageButtonClick += View_HomePageButtonClick;
    }

    private void View_HomePageButtonClick(object? sender, EventArgs e)
    {
        MessageBox.Show("Anasayfa Butonuna Tıklandı");

        var homePageView = _serviceProvider.GetRequiredService<HomePage.HomePage>();
        _viewNavigator.Navigate<HomePage.HomePage>(_mainFormView.PanelContainer);
    }

    private void View_Load(object? sender, EventArgs e)
    {
        MessageBox.Show("Load Başladı");
    }
}
