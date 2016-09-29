using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using Vault.Util;

namespace Vault.Models.Secret.Pki
{
    public class SignRequest
    {
        [JsonProperty("csr")]
        public string Csr { get; set; }

        [JsonProperty("common_name")]
        public string CommonName { get; set; }

        [JsonProperty("alt_names")]
        private string _altNames => StringUtil.ListToCsvString(AltNames);

        [JsonIgnore]
        public List<string> AltNames { get; set; }

        [JsonProperty("ip_sans")]
        private string _ipSans => StringUtil.ListToCsvString(IpSans);

        [JsonIgnore]
        public List<string> IpSans { get; set; }

        [JsonProperty("ttl")]
        public string Ttl { get; set; }

        [JsonProperty("format")]
        public CertificateFormat? Format { get; set; }

        [JsonProperty("exclude_cn_from_sans")]
        public bool? ExcludeCnFromSans { get; set; }
    }
}
