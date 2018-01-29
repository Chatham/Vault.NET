using Newtonsoft.Json;

namespace Vault.Models.Secret.Transit
{
    public class SignRequest
    {
        [JsonProperty("input")]
        public string Input { get; set; }

        [JsonProperty("context")]
        public string Context { get; set; }

        [JsonProperty("prehashed")]
        public bool? Prehashed { get; set; }

        [JsonProperty("key_version")]
        public int? KeyVersion { get; set; }
    }
}
