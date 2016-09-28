using System.Collections.Generic;
using Newtonsoft.Json;
using Vault.Util;

namespace Vault.Models.Auth.UserPass
{
    public class UsersRequest
    {
        [JsonProperty("username")]
        public string Username { get; set; }

        [JsonProperty("password")]
        public string Password { get; set; }

        [JsonProperty("policies")]
        private string _policies => StringUtil.ListToCsvString(Policies);

        [JsonIgnore]
        public List<string> Policies { get; set; }

        [JsonProperty("ttl")]
        public string Ttl { get; set; }

        [JsonProperty("max_ttl")]
        public string MaxTtl { get; set; }
    }
}
