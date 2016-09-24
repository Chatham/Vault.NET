using System.Collections.Generic;
using Newtonsoft.Json;

namespace Vault.Models.Secret.Pki
{
    public class PkiConfigUrls
    {
        [JsonProperty("issuing_certificates")]
        public List<string> IssuingCertificates { get; set; }

        [JsonProperty("crl_distribution_points")]
        public List<string> CrlDistributionPoints { get; set; }

        [JsonProperty("oscp_servers")]
        public List<string> OscpServers { get; set; }
    }
}
