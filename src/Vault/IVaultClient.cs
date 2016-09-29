using Vault.Endpoints;
using Vault.Endpoints.Sys;

namespace Vault
{
    public interface IVaultClient
    {
        IEndpoint Auth { get; }
        IEndpoint Secret { get; }
        ISysEndpoint Sys { get; }
    }
}
