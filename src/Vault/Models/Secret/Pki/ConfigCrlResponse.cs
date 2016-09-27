using Newtonsoft.Json;

namespace Vault.Models.Secret.Pki
{
    public class ConfigCrlResponse
    {
        [JsonProperty("expiry")]
        public string Expiry { get; set; }
    }
}
