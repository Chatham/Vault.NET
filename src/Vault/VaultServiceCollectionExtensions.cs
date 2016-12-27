using System;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace Vault
{
    public static class LoggingServiceCollectionExtensions
    {
        public static IServiceCollection AddVault(this IServiceCollection services)
        {
            if (services == null)
            {
                throw new ArgumentNullException(nameof(services));
            }

            services.TryAdd(ServiceDescriptor.Singleton<IVaultHttpClient, VaultHttpClient>());
            services.TryAdd(ServiceDescriptor.Singleton<IVaultClient, VaultClient>());

            return services;
        }
    }
}
