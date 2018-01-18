using Newtonsoft.Json;

namespace Vault.Models.Secret.Transit
{
    public class SignResponse
    {
        [JsonProperty("signature")]
        public string Signature { get; set; }

        [JsonProperty("public_key")]
        public string PublicKey { get; set; }
    }
}
