using System.Collections.Generic;
using Newtonsoft.Json;
using Vault.Util;

namespace Vault.Models.Secret.Pki
{
    public class GenerateRequest
    {
        [JsonProperty("common_name")]
        public string CommonName { get; set; }

        [JsonProperty("alt_names")]
        private string _altNames
        {
            get { return StringUtil.ListToCsvString(AltNames); }
            set { AltNames = StringUtil.CsvStringToList(value); }
        }

        [JsonIgnore]
        public List<string> AltNames { get; set; }

        [JsonProperty("ttl")]
        public string Ttl { get; set; }

        [JsonProperty("format")]
        public CertificateFormat? Format { get; set; }

        [JsonProperty("key_type")]
        public PrivateKeyType? KeyType { get; set; }

        [JsonProperty("key_bits")]
        public int? KeyBits { get; set; }

        [JsonProperty("exclude_cn_from_sans")]
        public bool? ExcludeCnFromSans { get; set; }

    }
}
