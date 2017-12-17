using System.Threading;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Vault.Endpoints.Sys
{
    public class LeaderResponse
    {
        [JsonProperty("ha_enabled")]
        public bool HaEnables { get; set; }

        [JsonProperty("is_self")]
        public bool IsSelf { get; set; }

        [JsonProperty("leader_address")]
        public string LeaderAddress { get; set; }
    }

    public partial class SysEndpoint
    {
        public Task<LeaderResponse> Leader(CancellationToken ct = default(CancellationToken))
        {
            return _client.Get<LeaderResponse>($"{UriPathBase}/leader", ct);
        }
    }
}
