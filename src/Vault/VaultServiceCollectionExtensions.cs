using System;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace Vault
{
    public static class VaultServiceCollectionExtensions
    {
        public static IServiceCollection AddVault(this IServiceCollection services, Action<VaultOptions> setupAction = null)
        {
            if (services == null)
            {
                throw new ArgumentNullException(nameof(services));
            }

            if (setupAction != null)
            {
                services.AddOptions();
                services.Configure(setupAction);
            }

            services.TryAdd(ServiceDescriptor.Singleton<IVaultClient, VaultClient>());

            return services;
        }
    }
}
