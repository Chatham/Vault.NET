namespace Vault.Endpoints.Sys
{
    public partial class Endpoint : ISysEndpoint
    {
        private readonly VaultClient _client;
        private const string UriPathBase = "/v1/sys";

        public Endpoint(VaultClient client)
        {
            _client = client;
        }
    }
}
