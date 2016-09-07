using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Vault.Endpoints
{
    public interface ISysEndpoint
    {
        Task<GetInitResponse> GetInit();
        Task<GetInitResponse> GetInit(CancellationToken ct);
    }

    public class GetInitResponse
    {
        public bool Initialized { get; set; }
    }

    public class PutInitRequest
    {
        [JsonProperty("secret_shares")]
        public int SecretShares { get; set; }

        [JsonProperty("secret_threshold")]
        public int SecretThreshold { get; set; }

        [JsonProperty("pgp_keys")]
        public List<string> PgpKeys { get; set; }
    }

    public class PutInitResponse
    {
        [JsonProperty("keys")]
        public List<string> Keys { get; set; }

        [JsonProperty("keys_base64")]
        public List<string> KeysBase64 { get; set; }

        [JsonProperty("root_token")]
        public string RootToken { get; set; }
    }
}
