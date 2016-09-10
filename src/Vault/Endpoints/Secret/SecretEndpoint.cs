using System.Collections.Generic;

namespace Vault.Endpoints.Secret
{
    public class SecretEndpoint
    {
        private readonly VaultClient _client;
        private readonly object _lock = new object();

        public SecretEndpoint(VaultClient client)
        {
            _client = client;
        }

        private LogicalEndpoint<Dictionary<string, object>> _generic;
        public LogicalEndpoint<Dictionary<string, object>> Generic
        {
            get
            {
                if (_generic == null)
                {
                    lock (_lock)
                    {
                        if (_generic == null)
                        {
                            _generic = new LogicalEndpoint<Dictionary<string, object>>(_client);
                        }
                    }
                }
                return _generic;
            }
        }
    }
}
