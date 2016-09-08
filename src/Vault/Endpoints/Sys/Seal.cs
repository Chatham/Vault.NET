using System.Threading;
using System.Threading.Tasks;

namespace Vault.Endpoints.Sys
{
    public class SealStatusResponse
    {
        public bool Sealed { get; set; }
        public int T { get; set; }
        public int N { get; set; }
        public int Progress { get; set; }
        public string Version { get; set; }
        public string ClusterName { get; set; }
        public string ClusterId { get; set; }
    } 

    public partial class SysEndpoint
    {
        public Task<SealStatusResponse> SealStatus()
        {
            return SealStatus(CancellationToken.None);
        }

        public Task<SealStatusResponse> SealStatus(CancellationToken ct)
        {
            return _client.Get<SealStatusResponse>($"${UriPathBase}/seal-status", ct);
        }

        public Task Seal()
        {
            return Seal(CancellationToken.None);
        }

        public Task Seal(CancellationToken ct)
        {
            return _client.PutVoid($"{UriPathBase}/seal", ct);
        }

        public Task<SealStatusResponse> Unseal(string shard)
        {
            return Unseal(shard, CancellationToken.None);
        }

        public Task<SealStatusResponse> Unseal(string shard, CancellationToken ct)
        {
            var unsealRequest = new UnsealRequest
            {
                Key = shard
            };

            return _client.Put<UnsealRequest, SealStatusResponse>($"{UriPathBase}/unseal", unsealRequest, ct);
        }

        public Task<SealStatusResponse> ResetUnsealProcess()
        {
            return ResetUnsealProcess(CancellationToken.None);
        }

        public Task<SealStatusResponse> ResetUnsealProcess(CancellationToken ct)
        {
            return _client.Put<ResetUnsealProcessRequest, SealStatusResponse>($"{UriPathBase}/unseal",
                new ResetUnsealProcessRequest {Reset = true}, ct);
        }

        internal class UnsealRequest
        {
            public string Key { get; set; }
        }

        internal class ResetUnsealProcessRequest
        {
            public bool Reset { get; set; }
        }
    }
}

