using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Vault.Util;

namespace Vault.Models.Auth.AppRole
{
    public class RoleRequest
    {
        [JsonProperty("role_name")]
        public string RoleName { get; set; }

        [JsonProperty("bind_secret_id")]
        public bool? BindSecretId { get; set; }

        [JsonProperty("bound_cidr_list")]
        private string _boundCidrList
        {
            get { return StringUtil.ListToCsvString(BoundCidrList); }
            set { BoundCidrList = StringUtil.CsvStringToList(value); }
        }

        [JsonIgnore]
        public List<string> BoundCidrList { get; set; }

        [JsonProperty("policies")]
        private string _policies
        {
            get { return StringUtil.ListToCsvString(Policies); }
            set { Policies = StringUtil.CsvStringToList(value); }
        }

        [JsonIgnore]
        public List<string> Policies { get; set; }

        [JsonProperty("secret_id_num_uses")]
        public int? SecretIdNumUses { get; set; }

        [JsonProperty("secret_id_ttl")]
        public int? SecretIdTtl { get; set; }

        [JsonProperty("token_ttl")]
        public int? TokenTtl { get; set; }

        [JsonProperty("token_max_ttl")]
        public int? TokenMaxTtl { get; set; }

        [JsonProperty("period")]
        public int? Period { get; set; }
    }
}
