using System.Threading;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Vault.Models;

namespace Vault.Endpoints.Sys
{
    public class WrappingLookupResponse
    {
        [JsonProperty("creation_time")]
        public string CreationTime { get; set; }

        [JsonProperty("creation_ttl")]
        public int CreationTtl { get; set; }
    }

    public partial class SysEndpoint
    {
        public Task<WrappingLookupResponse> WrapLookup(string token, CancellationToken ct = default(CancellationToken))
        {
            var request = new WrappingTokenRequest
            {
                Token = token
            };

            return _client.Post<WrappingTokenRequest, WrappingLookupResponse>($"{UriPathBase}/wrapping/lookup", request, ct);
        }

        public Task<SecretWrapInfo> Rewrap(string token, CancellationToken ct = default(CancellationToken))
        {
            var request = new WrappingTokenRequest
            {
                Token = token
            };

            return _client.Post<WrappingTokenRequest, SecretWrapInfo>($"{UriPathBase}/wrapping/rewrap", request, ct);
        }

        public Task<T> Unwrap<T>(string token, CancellationToken ct = default(CancellationToken))
        {
            var request = new WrappingTokenRequest
            {
                Token = token
            };

            return _client.Post<WrappingTokenRequest, T>($"{UriPathBase}/wrapping/unwrap", request, ct);
        }

        public Task<SecretWrapInfo> Wrap<T>(T content, CancellationToken ct = default(CancellationToken))
        {
            return _client.Post<T, SecretWrapInfo>($"{UriPathBase}/wrapping/wrap", content, ct);
        }
    }

    internal class WrappingTokenRequest
    {
        [JsonProperty("token")]
        public string Token { get; set; }
    }
}
