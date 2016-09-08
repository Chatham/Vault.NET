using System.Threading;
using System.Threading.Tasks;

namespace Vault.Endpoints.Sys
{
    public class GenerateRootStatusResponse
    {
        public string Nonce { get; set; }
        public bool Started { get; set; }
        public int Progress { get; set; }
        public int Required { get; set; }
        public bool Complete { get; set; }
        public string EncodedRootToken { get; set; }
    }

    public partial class SysEndpoint
    {
        public Task<GenerateRootStatusResponse> GenerateRootStatus()
        {
            return GenerateRootStatus(CancellationToken.None);
        }

        public Task<GenerateRootStatusResponse> GenerateRootStatus(CancellationToken ct)
        {
            return _client.Get<GenerateRootStatusResponse>($"{UriPathBase}/generate-root/attempt", ct);
        }

        public Task<GenerateRootStatusResponse> GenerateRootInit(string otp, string pgpKey)
        {
            return GenerateRootInit(otp, pgpKey, CancellationToken.None);
        }

        public Task<GenerateRootStatusResponse> GenerateRootInit(string otp, string pgpKey, CancellationToken ct)
        {
            var request = new GenerateRootInitRequest
            {
                Otp = otp,
                PgpKey = pgpKey
            };

            return
                _client.Put<GenerateRootInitRequest, GenerateRootStatusResponse>(
                    $"{UriPathBase}/generate-root/attempt", request, CancellationToken.None);
        }

        public Task GenerateRootCancel()
        {
            return GenerateRootCancel(CancellationToken.None);
        }

        public Task GenerateRootCancel(CancellationToken ct)
        {
            return _client.Delete($"{UriPathBase}/generate-root/attempt", ct);
        }

        public Task<GenerateRootStatusResponse> GenerateRootUpdate(string shard, string nonce)
        {
            return GenerateRootUpdate(shard, nonce, CancellationToken.None);
        }

        public Task<GenerateRootStatusResponse> GenerateRootUpdate(string shard, string nonce, CancellationToken ct)
        {
            var request = new GenerateRootUpdateRequest
            {
                Shard = shard,
                Nonce = nonce
            };

            return _client.Put<GenerateRootUpdateRequest, GenerateRootStatusResponse>($"{UriPathBase}/generate-root/update", 
                request, ct);
        }

        internal class GenerateRootInitRequest
        {
            public string Otp { get; set; }
            public string PgpKey { get; set; }
        }

        internal class GenerateRootUpdateRequest
        {
            public string Shard { get; set; }
            public string Nonce { get; set; }
        }
    }
}
