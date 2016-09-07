using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Vault.Endpoints.Sys
{
    public class InitStatusResponse
    {
        public bool Initialized { get; set; }
    }

    public class InitRequest
    {
        public int SecretShares { get; set; }
        public int SecretThreshold { get; set; }
        public List<string> PgpKeys { get; set; }
    }

    public class InitResponse
    {
        public List<string> Keys { get; set; }
        public List<string> KeysBase64 { get; set; }
        public string RootToken { get; set; }
    }

    public partial class Endpoint
    {
        public Task<bool> InitStatus()
        {
            return InitStatus(CancellationToken.None);
        }

        public async Task<bool> InitStatus(CancellationToken ct)
        {
            var result = await _client.Get<InitStatusResponse>($"{UriPathBase}/init", ct);
            return result.Initialized;
        }

        public Task<InitResponse> Init(InitRequest request)
        {
            return Init(request, CancellationToken.None);
        }

        public Task<InitResponse> Init(InitRequest request, CancellationToken ct)
        {
            return _client.Put<InitRequest, InitResponse>($"{UriPathBase}/init", request, ct);
        }
    }
}
