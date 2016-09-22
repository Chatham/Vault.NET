using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Newtonsoft.Json;

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
        public Task<Secret<Dictionary<string, MountInfo>>> ListMounts()
        {
            return ListMounts(CancellationToken.None);
        }

        public Task<Secret<Dictionary<string, MountInfo>>> ListMounts(CancellationToken ct)
        {
            return _client.Get< Secret<Dictionary<string, MountInfo>>>($"{UriPathBase}/mounts", ct);
        }

        public Task Mount(string path, MountInfo mountInfo)
        {
            return Mount(path, mountInfo, CancellationToken.None);
        }

        public Task Mount(string path, MountInfo mountInfo, CancellationToken ct)
        {
            return _client.PostVoid($"{UriPathBase}/mounts/{path}", mountInfo, ct);
        }

        public Task Unmount(string path)
        {
            return Unmount(path, CancellationToken.None);
        }

        public Task Unmount(string path, CancellationToken ct)
        {
            return _client.DeleteVoid($"{UriPathBase}/mounts/{path}", ct);
        }

        public Task Remount(string from, string to)
        {
            return Remount(from, to, CancellationToken.None);
        }

        public Task Remount(string from, string to, CancellationToken ct)
        {
            var request = new RemountRequest
            {
                From = from,
                To = to
            };
            return _client.PutVoid($"{UriPathBase}/remount", request, ct);
        }

        public Task TuneMount(string path, MountConfig mountConfig)
        {
            return TuneMount(path, mountConfig, CancellationToken.None);
        }

        public Task TuneMount(string path, MountConfig mountConfig, CancellationToken ct)
        {
            return _client.PostVoid($"{UriPathBase}/mounts/{path}/tune", mountConfig, ct);
        }

        public Task<MountConfig> MountConfig(string path)
        {
            return MountConfig(path, CancellationToken.None);
        }

        public Task<MountConfig> MountConfig(string path, CancellationToken ct)
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
