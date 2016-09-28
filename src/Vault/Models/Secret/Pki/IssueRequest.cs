using System.Collections.Generic;
using Newtonsoft.Json;
using Vault.Util;

namespace Vault.Models.Secret.Pki
{
    public class IssueRequest
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

        [JsonProperty("ip_sans")]
        private string _ipSans
        {
            get { return StringUtil.ListToCsvString(IpSans); }
            set { IpSans = StringUtil.CsvStringToList(value); }
        }

        [JsonIgnore]
        public List<string> IpSans;

        [JsonProperty("ttl")]
        public int? Ttl { get; set; }

        [JsonProperty("format")]
        public CertificateFormat? Format { get; set; }

        [JsonProperty("exclude_cn_from_sans")]
        public bool? ExcludeCnFromSans { get; set; }
    }
}
