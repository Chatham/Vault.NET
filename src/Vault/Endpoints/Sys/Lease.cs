using System.Threading;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Vault.Models;

namespace Vault.Endpoints.Sys
{
    public partial class SysEndpoint
    {
        public Task<VaultResponse<TData>> Renew<TData>(string leaseId, int increment, CancellationToken ct = default(CancellationToken))
        {
            var request = new RenewRequest
            {
                Increment = increment,
                LeaseId = leaseId
            };
            return _client.Put<RenewRequest, VaultResponse<TData>>($"{UriPathBase}/renew", request, ct);
        }

        public Task<VaultResponse<TData>> Renew<TData>(string leaseId, CancellationToken ct = default(CancellationToken))
        {
            var request = new RenewRequest
            {
                LeaseId = leaseId
            };
            return _client.Put<RenewRequest, VaultResponse<TData>>($"{UriPathBase}/renew", request, ct);
        }

        public Task Revoke(string id, CancellationToken ct = default(CancellationToken))
        {
            return _client.PutVoid($"{UriPathBase}/revoke/{id}", ct);
        }

        public Task RevokePrefix(string id, CancellationToken ct = default(CancellationToken))
        {
            return _client.PutVoid($"{UriPathBase}/revoke-prefix/{id}", ct);
        }

        public Task RevokeForce(string id, CancellationToken ct = default(CancellationToken))
        {
            return _client.PutVoid($"{UriPathBase}/revoke-force/{id}", ct);
        }

        private class RenewRequest
        {
            [JsonProperty("increment")]
            public int Increment { get; set; }

            [JsonProperty("lease_id")]
            public string LeaseId { get; set; }
        }
    }
}
