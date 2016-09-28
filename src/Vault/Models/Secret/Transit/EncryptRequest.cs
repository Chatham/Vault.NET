using Newtonsoft.Json;

namespace Vault.Models.Secret.Transit
{
    public class EncryptRequest
    {
        [JsonProperty("plaintext")]
        public string PlainText { get; set; }

        [JsonProperty("context")]
        public string Context { get; set; }

        [JsonProperty("nonce")]
        public string Nonce { get; set; }
    }
}
