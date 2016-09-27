using Newtonsoft.Json;

namespace Vault.Models.Secret.Pki
{
    public class PkiCertificateOnlyResponse
    {
        [JsonProperty("certificate")]
        public string Certificate { get; set; }
    }
}
