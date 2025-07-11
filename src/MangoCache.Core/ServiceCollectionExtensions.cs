using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MangoCache.Core
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddMangoCache(this IServiceCollection services)
        {
            services.AddSingleton<IMangoCache, MangoCache>();
            return services;
        }
    }
}
