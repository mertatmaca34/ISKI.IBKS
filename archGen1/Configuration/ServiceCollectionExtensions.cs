using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ISKI.IBKS.Presentation.WinForms.Configuration;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddView<TInterface, TConcrete, TPresenter>(this IServiceCollection services)
    where TInterface : class
    where TConcrete : class, TInterface
    where TPresenter : class
    {
        services.AddSingleton(sp =>
        {
            var view = ActivatorUtilities.CreateInstance<TConcrete>(sp);
            _ = ActivatorUtilities.CreateInstance<TPresenter>(sp, view);
            return view;

        });

        services.AddSingleton<TInterface>(sp => sp.GetRequiredService<TConcrete>());
        services.AddSingleton<TPresenter>();

        return services;
    }
}
