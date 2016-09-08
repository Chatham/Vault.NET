using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Vault.Endpoints.Sys
{
    public class RekeyStatusResponse
    {
        public string Nonce { get; set; } 
        public bool Started { get; set; }
        public int T { get; set; }
        public int N { get; set; }
        public int Progress { get; set; }
        public int Required { get; set; }
        public List<string> PgpFingerprints { get; set; }
        public bool Backup { get; set; }
    }

    public class RekeyInitRequest
    {
        public int SecretShares { get; set; }
        public int SecretThreshold { get; set; }
        public List<string> PgpKeys { get; set; }
        public bool Backup { get; set; }
    }

    public class RekeyUpdateResponse
    {
        public string Nonce { get; set; }
        public bool Complete { get; set; }
        public List<string> Keys { get; set; }
        public List<string> KeysBase64 { get; set; }
        public List<string> PgpFingerprints { get; set; }
        public bool Backup { get; set; }
    }

    public partial class SysEndpoint
    {
        public Task<RekeyStatusResponse> RekeyStatus()
        {
            return RekeyStatus(CancellationToken.None);
        }

        public Task<RekeyStatusResponse> RekeyStatus(CancellationToken ct)
        {
            return _client.Get<RekeyStatusResponse>($"{UriPathBase}/rekey/init", ct);
        }

        public Task<RekeyStatusResponse> RekeyRecoveryKeyStatus()
        {
            return RekeyRecoveryKeyStatus(CancellationToken.None);
        }

        public Task<RekeyStatusResponse> RekeyRecoveryKeyStatus(CancellationToken ct)
        {
            return _client.Get<RekeyStatusResponse>($"{UriPathBase}/rekey-recovery-key/init", ct);
        }

        public Task<RekeyStatusResponse> RekeyInit(RekeyInitRequest config)
        {
            return RekeyInit(config, CancellationToken.None);
        }

        public Task<RekeyStatusResponse> RekeyInit(RekeyInitRequest config, CancellationToken ct)
        {
            return _client.Put<RekeyInitRequest, RekeyStatusResponse>($"{UriPathBase}/rekey/init", config, ct);
        }

        public Task<RekeyStatusResponse> RekeyRecoveryKeyInit(RekeyInitRequest config)
        {
            return RekeyRecoveryKeyInit(config, CancellationToken.None);
        }

        public Task<RekeyStatusResponse> RekeyRecoveryKeyInit(RekeyInitRequest config, CancellationToken ct)
        {
            return _client.Put<RekeyInitRequest, RekeyStatusResponse>($"{UriPathBase}/rekey-recovery-key/init", config, ct);
        }

        public Task RekeyCancel()
        {
            return RekeyCancel(CancellationToken.None);
        }

        public Task RekeyCancel(CancellationToken ct)
        {
            return _client.DeleteVoid($"{UriPathBase}/rekey/init", ct);
        }

        public Task RekeyRecoveryKeyCancel()
        {
            return RekeyRecoveryKeyCancel(CancellationToken.None);
        }

        public Task RekeyRecoveryKeyCancel(CancellationToken ct)
        {
            return _client.DeleteVoid($"{UriPathBase}/rekey-recovery-key/init", ct);
        }

        public Task<RekeyUpdateResponse> RekeyUpdate(string shard, string nonce)
        {
            return RekeyUpdate(shard, nonce, CancellationToken.None);
        }

        public Task<RekeyUpdateResponse> RekeyUpdate(string shard, string nonce, CancellationToken ct)
        {
            var request = new RekeyUpdateRequest
            {
                Key = shard,
                Nonce = nonce
            };
            return _client.Put<RekeyUpdateRequest, RekeyUpdateResponse>($"{UriPathBase}/rekey/update", request, ct);
        }

        internal class RekeyUpdateRequest
        {
            public string Key { get; set; }
            public string Nonce { get; set; }
        }
    }
}

