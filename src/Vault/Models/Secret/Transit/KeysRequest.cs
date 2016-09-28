using Newtonsoft.Json;

namespace Vault.Models.Secret.Transit
{
    public class KeysRequest
    {
        [JsonProperty("derived")]
        public bool? Derived { get; set; }

        [JsonProperty("convergent_encryption")]
        public bool? ConvergentEncryption { get; set; }
    }
}
