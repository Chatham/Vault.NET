using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using Vault.Util;

namespace Vault.Models.Auth.AppRole
{
    public class SecretIdLookupResponse
    {

        [JsonProperty("cidr_list")]
        private string _cidrList
        {
            get { return StringUtil.ListToCsvString(CidrList); }
            set { CidrList = StringUtil.CsvStringToList(value); }
        }

        [JsonIgnore]
        public List<string> CidrList { get; set; }

        [JsonProperty("creation_time")]
        public DateTimeOffset CreationTime { get; set; }

        [JsonProperty("expiration_time")]
        public DateTimeOffset ExperationTime { get; set; }

        [JsonProperty("last_updated_time")]
        public DateTimeOffset LastUpdatedTime { get; set; }

        [JsonProperty("metadata")]
        public Dictionary<string, string> Metadata { get; set; }


        [JsonProperty("secret_id_accessor")]
        public string SecretIdAccessor { get; set; }

        [JsonProperty("secret_id_num_uses")]
        public int? SecretIdNumUses { get; set; }

        [JsonProperty("secret_id_ttl")]
        public int? SecretIdTtl { get; set; }
    }
}
