using System.Collections.Generic;
using Newtonsoft.Json;

namespace Vault.Models.Auth.Token
{
    public class RolesResponse
    {
        [JsonProperty("allowed_policies")]
        public List<string> AllowedPolicies { get; set; }

        [JsonProperty("disallowed_policies")]
        public List<string> DisallowedPolicies { get; set; }

        [JsonProperty("explicit_max_ttl")]
        public int? ExcplicitMaxTtl { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("orphan")]
        public bool? Orphan { get; set; }

        [JsonProperty("path_suffix")]
        public string PathSuffix { get; set; }

        [JsonProperty("period")]
        public int? Period { get; set; }

        [JsonProperty("renewable")]
        public bool? Renewable { get; set; }
    }
}
