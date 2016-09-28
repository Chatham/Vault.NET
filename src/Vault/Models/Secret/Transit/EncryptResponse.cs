using Newtonsoft.Json;

namespace Vault.Models.Secret.Transit
{
    public class EncryptResponse
    {
        [JsonProperty("ciphertext")]
        public string CipherText { get; set; }
    }
}
