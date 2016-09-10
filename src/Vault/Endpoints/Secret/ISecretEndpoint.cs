using System.Collections.Generic;

namespace Vault.Endpoints.Secret
{
    public interface ISecretEndpoint
    {
        ILogicalEndpoint<Dictionary<string, object>> Generic { get; }
    }
}