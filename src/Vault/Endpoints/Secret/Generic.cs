using System.Collections.Generic;

namespace Vault.Endpoints.Secret
{
    public class Generic : LogicalEndpoint<Dictionary<string, object>>
    {
        public Generic(VaultClient client) : base(client) { }
    }
}
