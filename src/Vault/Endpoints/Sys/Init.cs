using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Vault.Endpoints.Sys
{
    public class InitStatusResponse
    {
        [JsonProperty("initialized")]
        public bool Initialized { get; set; }
    }

    public class InitRequest
    {
        [JsonProperty("secret_shares")]
        public int SecretShares { get; set; }

        [JsonProperty("secret_threshold")]
        public int SecretThreshold { get; set; }

        [JsonProperty("pgp_keys")]
        public List<string> PgpKeys { get; set; }
    }

    public class InitResponse
    {
        [JsonProperty("keys")]
        public List<string> Keys { get; set; }

        [JsonProperty("keys_base64")]
        public List<string> KeysBase64 { get; set; }

        [JsonProperty("root_token")]
        public string RootToken { get; set; }
    }

    public partial class SysEndpoint
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
