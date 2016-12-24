using System.Collections.Generic;
using Newtonsoft.Json;
using Vault.Util;

namespace Vault.Models.Auth.UserPass
{
    public class UsersResponse
    {
        [JsonProperty("policies")]
        private string _policies
        {
            get { return StringUtil.ListToCsvString(Policies); }
            set { Policies = StringUtil.CsvStringToList(value); }
        }

        [JsonIgnore]
        public List<string> Policies { get; set; }

        [JsonProperty("ttl")]
        public string Ttl { get; set; }

        [JsonProperty("max_ttl")]
        public string MaxTtl { get; set; }
    }
}
