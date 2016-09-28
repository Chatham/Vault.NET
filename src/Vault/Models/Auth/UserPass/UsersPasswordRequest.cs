using Newtonsoft.Json;

namespace Vault.Models.Auth.UserPass
{
    public class UsersPasswordRequest
    {
        [JsonProperty("password")]
        public string Password { get; set; }
    }
}
