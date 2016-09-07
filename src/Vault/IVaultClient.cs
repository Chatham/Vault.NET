using System;
using Vault.Endpoints.Sys;

namespace Vault
{
    public interface IVaultClient : IDisposable
    {
        ISysEndpoint Sys { get; }
    }
}
