using System.Threading;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Vault.Endpoints.Sys
{
    public class SealStatusResponse
    {
        [JsonProperty("sealed")]
        public bool Sealed { get; set; }

        [JsonProperty("t")]
        public int T { get; set; }

        [JsonProperty("n")]
        public int N { get; set; }

        [JsonProperty("progress")]
        public int Progress { get; set; }

        [JsonProperty("version")]
        public string Version { get; set; }

        [JsonProperty("cluster_name")]
        public string ClusterName { get; set; }

        [JsonProperty("cluster_id")]
        public string ClusterId { get; set; }
    } 

    public partial class SysEndpoint
    {
        public Task<SealStatusResponse> SealStatus(CancellationToken ct = default(CancellationToken))
        {
            return _client.Get<SealStatusResponse>($"{UriPathBase}/seal-status", ct);
        }

        public Task Seal(CancellationToken ct = default(CancellationToken))
        {
            return _client.PutVoid($"{UriPathBase}/seal", ct);
        }

        public Task<SealStatusResponse> Unseal(string shard, CancellationToken ct = default(CancellationToken))
        {
            var unsealRequest = new UnsealRequest
            {
                Key = shard
            };

            return _client.Put<UnsealRequest, SealStatusResponse>($"{UriPathBase}/unseal", unsealRequest, ct);
        }

        public Task<SealStatusResponse> ResetUnsealProcess(CancellationToken ct = default(CancellationToken))
        {
            return _client.Put<ResetUnsealProcessRequest, SealStatusResponse>($"{UriPathBase}/unseal",
                new ResetUnsealProcessRequest {Reset = true}, ct);
        }

        private class UnsealRequest
        {
            [JsonProperty("key")]
            public string Key { get; set; }
        }

        private class ResetUnsealProcessRequest
        {
            [JsonProperty("reset")]
            public bool Reset { get; set; }
        }
    }
}

