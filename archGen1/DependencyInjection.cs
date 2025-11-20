using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace archGen1
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddWinForms(this IServiceCollection services)
        {
            //services.AddTransient<CounterPresenter>();
            //services.AddTransient<CounterForm>();

            return services;
        }
    }
}
