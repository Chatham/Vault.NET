using System.Threading;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Vault.Models;

namespace Vault.Endpoints.Sys
{
    public partial class SysEndpoint
    {
        public Task<Secret<TData>> Renew<TData>(string leaseId, int increment)
        {
            return Renew<TData>(leaseId, increment, CancellationToken.None);
        }

        public Task<Secret<TData>> Renew<TData>(string leaseId, int increment, CancellationToken ct)
        {
            var request = new RenewRequest
            {
                Increment = increment,
                LeaseId = leaseId
            };
            return _client.Put<RenewRequest, Secret<TData>>($"{UriPathBase}/renew", request, ct);
        }

        public Task<Secret<TData>> Renew<TData>(string leaseId)
        {
            return Renew<TData>(leaseId, CancellationToken.None);
        }

        public Task<Secret<TData>> Renew<TData>(string leaseId, CancellationToken ct)
        {
            var request = new RenewRequest
            {
                LeaseId = leaseId
            };
            return _client.Put<RenewRequest, Secret<TData>>($"{UriPathBase}/renew", request, ct);
        }

        public Task Revoke(string id)
        {
            return Revoke(id, CancellationToken.None);
        }

        public Task Revoke(string id, CancellationToken ct)
        {
            return _client.PutVoid($"{UriPathBase}/revoke/{id}", ct);
        }

        public Task RevokePrefix(string id)
        {
            return RevokePrefix(id, CancellationToken.None);
        }

        public Task RevokePrefix(string id, CancellationToken ct)
        {
            return _client.PutVoid($"{UriPathBase}/revoke-prefix/{id}", ct);
        }

        public Task RevokeForce(string id)
        {
            return RevokeForce(id, CancellationToken.None);
        }

        public Task RevokeForce(string id, CancellationToken ct)
        {
            return _client.PutVoid($"{UriPathBase}/revoke-force/{id}", ct);
        }

        internal class RenewRequest
        {
            [JsonProperty("increment")]
            public int Increment { get; set; }

            [JsonProperty("lease_id")]
            public string LeaseId { get; set; }
        }
    }
}
