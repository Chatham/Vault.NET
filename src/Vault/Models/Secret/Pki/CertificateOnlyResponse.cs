using Newtonsoft.Json;

namespace Vault.Models.Secret.Pki
{
    public class CertificateOnlyResponse
    {
        [JsonProperty("certificate")]
        public string Certificate { get; set; }
    }
}
