
using System.Threading;
using System.Threading.Tasks;

namespace Vault.Endpoints.Sys
{
    public class LeaderResponse
    {
        public bool HaEnables { get; set; }
        public bool IsSelf { get; set; }
        public string LeaderAddress { get; set; }
    }

    public partial class SysEndpoint
    {
        public Task<LeaderResponse> Leader()
        {
            return Leader(CancellationToken.None);
        }

        public Task<LeaderResponse> Leader(CancellationToken ct)
        {
            return _client.Get<LeaderResponse>($"{UriPathBase}/leader", ct);
        }
    }
}
