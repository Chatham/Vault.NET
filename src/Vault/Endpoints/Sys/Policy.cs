using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Vault.Endpoints.Sys
{
    public partial class SysEndpoint
    {
        public Task<List<string>> ListPolicies()
        {
            return ListPolicies(CancellationToken.None);
        }

        public async Task<List<string>> ListPolicies(CancellationToken ct)
        {
            var results = await _client.Get<ListPoliciesResponse>($"{UriPathBase}/policy", ct);
            return results.Policies;
        }

        public Task<string> GetPolicy(string name)
        {
            return GetPolicy(name, CancellationToken.None);
        }

        public async Task<string> GetPolicy(string name, CancellationToken ct)
        {
            var result = await _client.Get<PolicyRequest>($"{UriPathBase}/policy/{name}", ct);
            return result.Rules;
        }

        public Task PutPolicy(string name, string rules)
        {
            return PutPolicy(name, rules, CancellationToken.None);
        }

        public Task PutPolicy(string name, string rules, CancellationToken ct)
        {
            var request = new PolicyRequest
            {
                Rules = rules
            };
            return _client.PutVoid($"{UriPathBase}/policy/{name}", request, ct);
        }

        public Task DeletePolicy(string name)
        {
            return DeletePolicy(name, CancellationToken.None);
        }

        public Task DeletePolicy(string name, CancellationToken ct)
        {
            return _client.DeleteVoid($"{UriPathBase}/policy/{name}", ct);
        }

        internal class ListPoliciesResponse
        {
            public List<string> Policies { get; set; }
        }

        internal class PolicyRequest
        {
            public string Rules { get; set; }
        }
    }
}
