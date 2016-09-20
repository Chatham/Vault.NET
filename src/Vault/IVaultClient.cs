using System;
using Vault.Endpoints;
using Vault.Endpoints.Sys;

namespace Vault
{
    public interface IVaultClient
    {
        ISysEndpoint Sys { get; }
        ISecretEndpoint Secret { get; }
    }
}
