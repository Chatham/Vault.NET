using Newtonsoft.Json;

namespace Vault.Models.Auth.UserPass
{
    public class LoginRequest
    {
        [JsonProperty("password")]
        public string Password { get; set; }
    }
}
