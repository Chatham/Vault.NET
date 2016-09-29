using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace Vault.Models.Secret.Pki
{
    public class SignVerbatimRequest
    {
        [JsonProperty("csr")]
        public string Csr { get; set; }

        [JsonProperty("ttl")]
        public string Ttl { get; set; }

        [JsonProperty("format")]
        public CertificateFormat? Format { get; set; }
    }
}
