using Newtonsoft.Json;

namespace Vault.Models.Secret.Pki
{
    public class PkiCertResponse
    {
        [JsonProperty("certificate")]
        public string Certificate { get; set; }
    }
}
