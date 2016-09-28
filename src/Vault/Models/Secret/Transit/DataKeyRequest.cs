using Newtonsoft.Json;

namespace Vault.Models.Secret.Transit
{
    public class DataKeyRequest
    {
        [JsonProperty("context")]
        public string Context { get; set; }

        [JsonProperty("nonce")]
        public string Nonce { get; set; }

        [JsonProperty("bits")]
        public int? Bits { get; set; }
    }
}
