using Newtonsoft.Json;

namespace Vault.Models.Secret.Pki
{
    public class PkiConfigCaRequest
    {
        [JsonProperty("pem_bundle")]
        public string PemBundle { get; set; }
    }
}
