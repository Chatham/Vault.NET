using Newtonsoft.Json;

namespace Vault.Models.Secret.Pki
{
    public class PkiConfigCrl
    {
        [JsonProperty("expiry")]
        public string Expiry { get; set; }
    }
}
