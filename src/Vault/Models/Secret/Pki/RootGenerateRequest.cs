using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;

namespace Vault.Models.Secret.Pki
{
    public class RootGenerateRequest
    {
        [JsonProperty("common_name")]
        public string CommonName { get; set; }

        [JsonProperty("alt_names", NullValueHandling = NullValueHandling.Ignore)]
        private string _altNames;

        [JsonIgnore]
        public List<string> AltNames
        {
            get { return _altNames.Split(',').ToList(); }
            set { _altNames = string.Join(",", value); }
        }

        [JsonProperty("ttl", DefaultValueHandling = DefaultValueHandling.Ignore)]
        public string Ttl { get; set; }

        [JsonProperty("format", DefaultValueHandling = DefaultValueHandling.Ignore)]
        public CertificateType Format { get; set; }

        [JsonProperty("key_type", DefaultValueHandling = DefaultValueHandling.Ignore)]
        public PrivateKeyType KeyType { get; set; }

        [JsonProperty("key_bits", DefaultValueHandling = DefaultValueHandling.Ignore)]
        public int KeyBits { get; set; }

        [JsonProperty("max_path_length", DefaultValueHandling = DefaultValueHandling.Ignore)]
        public int MaxPathLenth { get; set; }

        [JsonProperty("exclude_cn_from_sans", DefaultValueHandling = DefaultValueHandling.Ignore)]
        public bool ExcludeCnFromSans { get; set; }
    }
}
