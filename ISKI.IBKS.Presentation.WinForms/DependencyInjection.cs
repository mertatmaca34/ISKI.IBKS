using ISKI.IBKS.Presentation.WinForms.Configuration;
using ISKI.IBKS.Presentation.WinForms.Features.HomePage;
using ISKI.IBKS.Presentation.WinForms.Features.Main;
using ISKI.IBKS.Presentation.WinForms.Navigation;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ISKI.IBKS.Presentation.WinForms
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddPresentation(this IServiceCollection services)
        {
            services.AddView<IMainFormView, MainForm, MainFormPresenter>();
            services.AddSingleton<GlobalExceptionHandler>();
            services.AddSingleton<IViewNavigator, ViewNavigator>();

            return services;
        }
    }
}
