using System.Threading;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Vault.Endpoints.Sys
{
    public class KeyStatus
    {
        [JsonProperty("term")]
        public int Term { get; set; }

        [JsonProperty("install_time")]
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
