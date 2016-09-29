using System.Collections.Generic;
using Newtonsoft.Json;
using Vault.Util;

namespace Vault.Models.Auth.Token
{
    public class CreateRequest
    {
        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("policies")]
        public List<string> Policies { get; set; }

        [JsonProperty("meta")]
        public Dictionary<string, string> Meta { get; set; }

        [JsonProperty("no_parent")]
        public bool? NoParent { get; set; }

        [JsonProperty("no_default_policy")]
        public bool? NoDefaultPolicy { get; set; }

        [JsonProperty("renewable")]
        public bool? Renewable { get; set; }

        [JsonProperty("ttl")]
        public string Ttl { get; set; }

        [JsonProperty("explicit_max_ttl")]
        public string ExplicitMaxTtl { get; set; }

        [JsonProperty("display_name")]
        public string DisplayName { get; set; }

        [JsonProperty("num_uses")]
        public int? NumUses { get; set; }
    }
}
