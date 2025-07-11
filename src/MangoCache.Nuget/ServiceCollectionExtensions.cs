using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MangoCache.Nuget
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddMangoCacheClient(
            this IServiceCollection services,
            Action<MangoCacheClientOptions> configureOptions = null)
        {
            if (configureOptions != null)
            {
                services.Configure(configureOptions);
            }
            else
            {
                services.Configure<MangoCacheClientOptions>(_ => { });
            }

            services.AddSingleton<IMangoCacheClient, MangoCacheClient>();

            return services;
        }
    }
}
