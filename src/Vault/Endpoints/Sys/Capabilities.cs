using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Vault.Endpoints.Sys
{
    public partial class SysEndpoint
    {
        public Task<List<string>> CapabilitiesSelf(string path)
        {
            return CapabilitiesSelf(path, CancellationToken.None);
        }

        public async Task<List<string>> CapabilitiesSelf(string path, CancellationToken ct)
        {
            var request = new CapabilitiesRequest
            {
                Path = path
            };
            var response = await _client.Post<CapabilitiesRequest, CapabilitiesResponse>(
                $"{UriPathBase}/capabilities-self", request, ct).ConfigureAwait(false);
            return response.Capabilities;
        }

        public Task<List<string>> Capabilities(string token, string path)
        {
            return Capabilities(token, path, CancellationToken.None);
        }

        public async Task<List<string>> Capabilities(string token, string path, CancellationToken ct)
        {
            var request = new CapabilitiesRequest
            {
                Token = token,
                Path = path
            };
            var response = await _client.Post<CapabilitiesRequest, CapabilitiesResponse>(
                $"{UriPathBase}/capabilities", request, ct).ConfigureAwait(false);
            return response.Capabilities;
        }

        internal class CapabilitiesRequest
        {
            [JsonProperty("token")]
            public string Token { get; set; }

            [JsonProperty("path")]
            public string Path { get; set; }
        }

        internal class CapabilitiesResponse
        {
            [JsonProperty("capabilities")]
            public List<string> Capabilities { get; set; }
        }
    }
}
