using System.Collections.Generic;
using Newtonsoft.Json;
using Vault.Util;

namespace Vault.Models.Auth.UserPass
{
    public class UsersRequest : UsersResponse
    {
        [JsonProperty("password")]
        public string Password { get; set; }
    }
}
