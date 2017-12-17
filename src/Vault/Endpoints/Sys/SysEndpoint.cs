namespace Vault.Endpoints.Sys
{
    public partial class SysEndpoint : ISysEndpoint
    {
        private readonly VaultClient _client;
        private const string UriPathBase = "/v1/sys";

        public SysEndpoint(VaultClient client)
        {
            _client = client;
        }
    }
}
