using Newtonsoft.Json;

namespace Vault.Models.Secret.Transit
{
    public class DecryptRequest
    {
        [JsonProperty("ciphertext")]
        public string CipherText { get; set; }

        [JsonProperty("context")]
        public string Context { get; set; }

        [JsonProperty("nonce")]
        public string Nonce { get; set; }
    }
}
