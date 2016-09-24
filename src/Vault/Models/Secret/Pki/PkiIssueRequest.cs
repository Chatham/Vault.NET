using Newtonsoft.Json;

namespace Vault.Models.Secret.Pki
{
    public class PkiIssueRequest
    {
        [JsonProperty("common_name")]
        public string CommonName { get; set; }

        [JsonProperty("alt_names")]
        public string AltNames { get; set; }

        [JsonProperty("ip_sans")]
        public string IpSans { get; set; }

        [JsonProperty("ttl")]
        public int Ttl { get; set; }

        [JsonProperty("format")]
        public string Format { get; set; }

        [JsonProperty("exclude_cn_from_sans")]
        public bool ExcludeCnFromSans { get; set; }
    }
}
