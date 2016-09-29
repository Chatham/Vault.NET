using Newtonsoft.Json;

namespace Vault.Models.Secret.Pki
{
    public class TidyRequest
    {
        [JsonProperty("tidy_cert_store")]
        public bool? TidyCertStore { get; set; }

        [JsonProperty("tidy_revocation_list")]
        public bool? TidyRevocationList { get; set; }

        [JsonProperty("safety_buffer")]
        public string SafetyBuffer { get; set; }
    }
}
