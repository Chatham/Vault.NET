using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Vault.Endpoints.Sys
{
    public partial class SysEndpoint
    {
        public async Task<List<string>> ListPolicies(CancellationToken ct = default(CancellationToken))
        {
            var results = await _client.Get<ListPoliciesResponse>($"{UriPathBase}/policy", ct);
            return results.Policies;
        }

        public async Task<string> GetPolicy(string name, CancellationToken ct = default(CancellationToken))
        {
            var result = await _client.Get<PolicyRequest>($"{UriPathBase}/policy/{name}", ct);
            return result.Rules;
        }

        public Task PutPolicy(string name, string rules, CancellationToken ct = default(CancellationToken))
        {
            var request = new PolicyRequest
            {
                Rules = rules
            };
            return _client.PutVoid($"{UriPathBase}/policy/{name}", request, ct);
        }

        public Task DeletePolicy(string name, CancellationToken ct = default(CancellationToken))
        {
            return _client.DeleteVoid($"{UriPathBase}/policy/{name}", ct);
        }

        private class ListPoliciesResponse
        {
            [JsonProperty("policies")]
            public List<string> Policies { get; set; }
        }

        private class PolicyRequest
        {
            [JsonProperty("rules")]
            public string Rules { get; set; }
        }
    }
}
