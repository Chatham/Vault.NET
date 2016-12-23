using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Vault.Models;

namespace Vault.Endpoints.Sys
{
    public class MountInfo
    {
        [JsonProperty("type")]
        public string Type { get; set; }

        [JsonProperty("description")]
        public string Description { get; set; }

        [JsonProperty("config")]
        public MountConfig Config { get; set; }
    }

    public class MountConfig
    {
        [JsonProperty("default_lease_ttl")]
        public string DefaultLeaseTtl { get; set; }

        [JsonProperty("max_lease_ttl")]
        public string MaxLeaseTtl { get; set; }
    }

    public partial class SysEndpoint
    {
        public Task<VaultResponse<Dictionary<string, MountInfo>>> ListMounts(CancellationToken ct = default(CancellationToken))
        {
            return _client.Get<VaultResponse<Dictionary<string, MountInfo>>>($"{UriPathBase}/mounts", ct);
        }

        public Task Mount(string path, MountInfo mountInfo, CancellationToken ct = default(CancellationToken))
        {
            return _client.PostVoid($"{UriPathBase}/mounts/{path}", mountInfo, ct);
        }

        public Task Unmount(string path, CancellationToken ct = default(CancellationToken))
        {
            return _client.DeleteVoid($"{UriPathBase}/mounts/{path}", ct);
        }

        public Task Remount(string from, string to, CancellationToken ct = default(CancellationToken))
        {
            var request = new RemountRequest
            {
                From = from,
                To = to
            };
            return _client.PutVoid($"{UriPathBase}/remount", request, ct);
        }

        public Task TuneMount(string path, MountConfig mountConfig, CancellationToken ct = default(CancellationToken))
        {
            return _client.PostVoid($"{UriPathBase}/mounts/{path}/tune", mountConfig, ct);
        }

        public Task<MountConfig> MountConfig(string path, CancellationToken ct = default(CancellationToken))
        {
            return _client.Get<MountConfig>($"{UriPathBase}/mounts/{path}/tune", ct);
        }

        internal class RemountRequest
        {
            [JsonProperty("from")]
            public string From { get; set; }

            [JsonProperty("to")]
            public string To { get; set; }
        }
    }
}
