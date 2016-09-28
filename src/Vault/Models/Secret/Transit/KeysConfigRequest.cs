using Newtonsoft.Json;

namespace Vault.Models.Secret.Transit
{
    public class KeysConfigRequest
    {
        [JsonProperty("min_decryption_version")]
        public int? MinDecriptionVersion { get; set; }

        [JsonProperty("deletion_allowed")]
        public bool? DeletionAllowed { get; set; } 
    }
}
