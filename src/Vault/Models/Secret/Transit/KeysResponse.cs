using System.Collections.Generic;
using Newtonsoft.Json;

namespace Vault.Models.Secret.Transit
{
    public class KeysResponse
    {
        [JsonProperty("cipher_mode")]
        public string CipherMode { get; set; }

        [JsonProperty("deletion_allowed")]
        public bool DeletionAllowed { get; set; }

        [JsonProperty("derived")]
        public bool Derived { get; set; }

        [JsonProperty("keys")]
        public Dictionary<string, int> Keys { get; set; }

        [JsonProperty("min_decryption_version")]
        public int MinDecriptionVersion { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }
    }
}
