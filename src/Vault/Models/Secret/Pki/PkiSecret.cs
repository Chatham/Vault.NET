using System.Collections.Generic;
using Newtonsoft.Json;

namespace Vault.Models.Secret.Pki
{
    public class PkiSecret
    {
        [JsonProperty("serial_number")]
        public string SerialNumber { get; set; }

        [JsonProperty("private_key")]
        public string PrivateKey { get; set; }

        [JsonProperty("private_key_type")]
        public PrivateKeyType PrivateKeyType { get; set; }

        [JsonProperty("certificate")]
        public string Certificate { get; set; }

        [JsonProperty("issuing_ca")]
        public string IssuingCa { get; set; }

        [JsonProperty("ca_chain")]
        public List<string> CaChain { get; set; }
    }
}
