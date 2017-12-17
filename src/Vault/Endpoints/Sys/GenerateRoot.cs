using System.Threading;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Vault.Endpoints.Sys
{
    public class GenerateRootStatusResponse
    {
        [JsonProperty("nonce")]
        public string Nonce { get; set; }

        [JsonProperty("started")]
        public bool Started { get; set; }

        [JsonProperty("progress")]
        public int Progress { get; set; }

        [JsonProperty("required")]
        public int Required { get; set; }

        [JsonProperty("complete")]
        public bool Complete { get; set; }

        [JsonProperty("encoded_root_token")]
        public string EncodedRootToken { get; set; }
    }

    public partial class SysEndpoint
    {
        public Task<GenerateRootStatusResponse> GenerateRootStatus(CancellationToken ct = default(CancellationToken))
        {
            return _client.Get<GenerateRootStatusResponse>($"{UriPathBase}/generate-root/attempt", ct);
        }

        public Task<GenerateRootStatusResponse> GenerateRootInit(string otp, string pgpKey, CancellationToken ct = default(CancellationToken))
        {
            var request = new GenerateRootInitRequest
            {
                Otp = otp,
                PgpKey = pgpKey
            };

            return
                _client.Put<GenerateRootInitRequest, GenerateRootStatusResponse>(
                    $"{UriPathBase}/generate-root/attempt", request, ct);
        }

        public Task GenerateRootCancel(CancellationToken ct = default(CancellationToken))
        {
            return _client.DeleteVoid($"{UriPathBase}/generate-root/attempt", ct);
        }

        public Task<GenerateRootStatusResponse> GenerateRootUpdate(string shard, string nonce, 
            CancellationToken ct = default(CancellationToken))
        {
            var request = new GenerateRootUpdateRequest
            {
                Shard = shard,
                Nonce = nonce
            };

            return _client.Put<GenerateRootUpdateRequest, GenerateRootStatusResponse>($"{UriPathBase}/generate-root/update", 
                request, ct);
        }

        private class GenerateRootInitRequest
        {
            [JsonProperty("otp")]
            public string Otp { get; set; }

            [JsonProperty("pgp_key")]
            public string PgpKey { get; set; }
        }

        private class GenerateRootUpdateRequest
        {
            [JsonProperty("shard")]
            public string Shard { get; set; }

            [JsonProperty("nonce")]
            public string Nonce { get; set; }
        }
    }
}
