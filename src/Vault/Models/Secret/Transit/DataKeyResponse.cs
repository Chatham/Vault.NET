using Newtonsoft.Json;

namespace Vault.Models.Secret.Transit
{
    public class DataKeyResponse
    {
        [JsonProperty("ciphertext")]
        public string CipherText { get; set; }

        [JsonProperty("plaintext")]
        public string PlainText { get; set; }
    }
}
