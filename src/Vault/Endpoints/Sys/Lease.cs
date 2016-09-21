using System.Threading;
using System.Threading.Tasks;

namespace Vault.Endpoints.Sys
{
    public partial class SysEndpoint
    {
        public Task<Secret<object>> Renew(string leaseId, int increment)
        {
            return Renew(leaseId, increment, CancellationToken.None);
        }

        public Task<Secret<object>> Renew(string leaseId, int increment, CancellationToken ct)
        {
            var request = new RenewRequest
            {
                Increment = increment,
                LeaseId = leaseId
            };
            return _client.Put<RenewRequest, Secret<dynamic>>($"{UriPathBase}/renew", request, ct);
        }

        public Task<Secret<object>> Renew(string leaseId)
        {
            return Renew(leaseId, CancellationToken.None);
        }

        public Task<Secret<object>> Renew(string leaseId, CancellationToken ct)
        {
            var request = new RenewRequest
            {
                LeaseId = leaseId
            };
            return _client.Put<RenewRequest, Secret<dynamic>>($"{UriPathBase}/renew", request, ct);
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

        private class RenewRequest
        {
            public int Increment { get; set; }
            public string LeaseId { get; set; }
        }
    }
}
