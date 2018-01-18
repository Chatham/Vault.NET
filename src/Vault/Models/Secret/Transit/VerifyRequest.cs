using Newtonsoft.Json;

namespace Vault.Models.Secret.Transit
{
    public class VerifyRequest
    {
        [JsonProperty("input")]
        public string Input { get; set; }

        [JsonProperty("signature")]
        public string Signature { get; set; }

        [JsonProperty("hmac")]
        public string Hmac { get; set; }

        [JsonProperty("context")]
        public string Context { get; set; }

        [JsonProperty("prehashed")]
        public bool? Prehashed { get; set; }
    }
}
