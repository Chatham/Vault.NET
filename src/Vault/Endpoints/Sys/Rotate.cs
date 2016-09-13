using System.Threading;
using System.Threading.Tasks;

namespace Vault.Endpoints.Sys
{
    public class KeyStatus
    {
        public int Term { get; set; }
        public string InstallTime { get; set; }
    }

    public partial class SysEndpoint
    {
        public Task Rotate()
        {
            return Rotate(CancellationToken.None);
        }

        public Task Rotate(CancellationToken ct)
        {
            return _client.PutVoid($"{UriPathBase}/rotate", ct);
        }

        public Task<KeyStatus> KeyStatus()
        {
            return KeyStatus(CancellationToken.None);
        }

        public Task<KeyStatus> KeyStatus(CancellationToken ct)
        {
            return _client.Get<KeyStatus>($"{UriPathBase}/key-status", ct);
        }
    }
}
