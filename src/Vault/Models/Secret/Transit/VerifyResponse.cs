using Newtonsoft.Json;

namespace Vault.Models.Secret.Transit
{
    public class VerifyResponse
    {
        [JsonProperty("valid")]
        public bool Valid { get; set; }
    }
}
