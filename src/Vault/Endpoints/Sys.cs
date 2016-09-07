using System.Threading;
using System.Threading.Tasks;

namespace Vault.Endpoints
{
    public class Sys : ISysEndpoint
    {
        private readonly VaultClient _client;
        private const string UriPathBase = "/v1/sys";

        public Sys(VaultClient client)
        {
            _client = client;
        }

        public Task<SysInitResponse> ReadInit()
        {
            return ReadInit(CancellationToken.None);
        }

        public Task<SysInitResponse> ReadInit(CancellationToken ct)
        {
            return _client.Get<SysInitResponse>($"{UriPathBase}/init", null, ct);
        }
    }
}