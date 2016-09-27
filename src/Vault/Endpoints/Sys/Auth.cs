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
        public int? DefaultLeaseTTL { get; set; }

        [JsonProperty("max_lease_ttl")]
        public int? MaxLeaseTTL { get; set; }
    }

    public partial class SysEndpoint
    {
        public Task<Dictionary<string, AuthMount>> ListAuth()
        {
            return ListAuth(CancellationToken.None);
        }

        public Task<Dictionary<string, AuthMount>> ListAuth(CancellationToken ct)
        {
            return _client.Get<Dictionary<string, AuthMount>>($"{UriPathBase}/auth", ct);
        }

        public Task EnableAuth(string path, string authType, string description)
        {
            return EnableAuth(path, authType, description, CancellationToken.None);
        }

        public Task EnableAuth(string path, string authType, string description, CancellationToken ct)
        {
            var request = new EnableAuthRequest
            {
                Type = authType,
                Description = description
            };
            return _client.PostVoid($"{UriPathBase}/auth/{path}", request, ct);
        }

        public Task DisableAuth(string path)
        {
            return DisableAuth(path, CancellationToken.None);
        }

        public Task DisableAuth(string path, CancellationToken ct)
        {
            return _client.DeleteVoid($"{UriPathBase}/auth/{path}", ct);
        }

        internal class EnableAuthRequest
        {
            [JsonProperty("type")]
            public string Type { get; set; }

            [JsonProperty("description")]
            public string Description { get; set; }
        }
    }
}
