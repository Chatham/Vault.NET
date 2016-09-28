using Newtonsoft.Json;

namespace Vault.Models.Secret.Transit
{
    public class DecryptResponse
    {
        [JsonProperty("plaintext")]
        public string PlainText { get; set; }
    }
}
