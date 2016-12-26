using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Vault.Endpoints.Sys
{
    public class AuthMount
    {
        [JsonProperty("type")]
        public string Type { get; set; }

        [JsonProperty("description")]
        public string Description { get; set; }

        [JsonProperty("config")]
        public AuthConfigOptions Config { get; set; }
    }

    public class AuthConfigOptions
    {
        [JsonProperty("default_lease_ttl")]
        public int? DefaultLeaseTtl { get; set; }

        [JsonProperty("max_lease_ttl")]
        public int? MaxLeaseTtl { get; set; }
    }

    public partial class SysEndpoint
    {
        public Task<Dictionary<string, AuthMount>> ListAuth(CancellationToken ct = default(CancellationToken))
        {
            return _client.Get<Dictionary<string, AuthMount>>($"{UriPathBase}/auth", ct);
        }

        public Task EnableAuth(string path, string authType, string description, CancellationToken ct = default(CancellationToken))
        {
            var request = new EnableAuthRequest
            {
                Type = authType,
                Description = description
            };
            return _client.PostVoid($"{UriPathBase}/auth/{path}", request, ct);
        }

        public Task DisableAuth(string path, CancellationToken ct = default(CancellationToken))
        {
            return _client.DeleteVoid($"{UriPathBase}/auth/{path}", ct);
        }

        private class EnableAuthRequest
        {
            [JsonProperty("type")]
            public string Type { get; set; }

            [JsonProperty("description")]
            public string Description { get; set; }
        }
    }
}
