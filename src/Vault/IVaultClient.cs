using System;
using Vault.Endpoints;
using Vault.Endpoints.Sys;

namespace Vault
{
    public interface IVaultClient
    {
        Uri Address { get; set; }
        string Token { set; }

        IEndpoint Auth { get; }
        IEndpoint Secret { get; }
        ISysEndpoint Sys { get; }
    }
}
