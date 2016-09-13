using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Vault.Endpoints.Sys
{
    public class AuthMount
    {
        public string Type { get; set; }
        public string Description { get; set; }
        public AuthConfigOptions Config { get; set; }
    }

    public class AuthConfigOptions
    { 
        public int DefaultLeaseTtl { get; set; }
        public int MaxLeaseTtl { get; set; }
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
            public string Type { get; set; }
            public string Description { get; set; }
        }
    }
}
