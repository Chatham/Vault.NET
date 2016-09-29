using System.Collections.Generic;
using Newtonsoft.Json;

namespace Vault.Models.Auth.Token
{
    public class LookupAccessorResponse
    {
        [JsonProperty("creation_time")]
        public int CreationTime { get; set; }

        [JsonProperty("creation_ttl")]
        public int CreationTtl { get; set; }

        [JsonProperty("display_name")]
        public string DisplayName { get; set; }

        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("meta")]
        public Dictionary<string, string> Meta { get; set; }

        [JsonProperty("num_uses")]
        public int NumUses { get; set; }

        [JsonProperty("orphan")]
        public bool Orphan { get; set; }

        [JsonProperty("path")]
        public string Path { get; set; }

        [JsonProperty("policies")]
        public List<string> Policies { get; set; }

        [JsonProperty("ttl")]
        public int Ttl { get; set; }
    }
}
