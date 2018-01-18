using Newtonsoft.Json;
using System;

namespace Vault.Models.Secret.Transit
{
    public class KeyData
    {
        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("public_key")]
        public string PublicKey { get; set; }

        [JsonProperty("creation_time")]
        public DateTime CreationTime { get; set; }
    }
}
