using Newtonsoft.Json;

namespace Vault.Models.Secret.Pki
{
    public class ConfigCrl
    {
        [JsonProperty("expiry")]
        public string Expiry { get; set; }
    }
}
