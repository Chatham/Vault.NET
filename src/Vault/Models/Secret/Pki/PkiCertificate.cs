using System.Collections.Generic;
using Newtonsoft.Json;

namespace Vault.Models.Secret.Pki
{
    public class PkiCertificate
    {
        [JsonProperty("serial_number")]
        public string SerialNumber { get; set; }

        [JsonProperty("certificate")]
        public string Certificate { get; set; }

        [JsonProperty("issuing_ca")]
        public string IssuingCa { get; set; }

        [JsonProperty("ca_chain")]
        public List<string> CaChain { get; set; }
    }
}
