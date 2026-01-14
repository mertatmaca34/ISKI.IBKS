using Microsoft.Extensions.DependencyInjection;

namespace ISKI.IBKS.Presentation.WinForms.Extensions;

/// <summary>
/// Extension methods for registering views and pages in the DI container.
/// </summary>
public static class ServiceCollectionExtensions
{
    /// <summary>
    /// Registers a view with its interface and presenter as singleton services.
    /// Used for main form-level views that persist throughout application lifetime.
    /// </summary>
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

        return services;
    }

    /// <summary>
    /// Registers a page (UserControl) with its interface and presenter as transient services.
    /// Pages are created fresh each time they are navigated to.
    /// </summary>
    public static IServiceCollection AddPage<TInterface, TConcrete, TPresenter>(this IServiceCollection services)
        where TInterface : class
        where TConcrete : UserControl, TInterface
        where TPresenter : class
    {
        services.AddTransient(sp =>
        {
            var page = ActivatorUtilities.CreateInstance<TConcrete>(sp);
            _ = ActivatorUtilities.CreateInstance<TPresenter>(sp, page);
            return page;
        });

        services.AddTransient<TInterface>(sp => sp.GetRequiredService<TConcrete>());

        return services;
    }

    /// <summary>
    /// Registers a simple page (UserControl) without a presenter as a transient service.
    /// Use for pages that don't require MVP pattern.
    /// </summary>
    public static IServiceCollection AddPage<TConcrete>(this IServiceCollection services)
        where TConcrete : UserControl
    {
        services.AddTransient<TConcrete>();
        return services;
    }
}
