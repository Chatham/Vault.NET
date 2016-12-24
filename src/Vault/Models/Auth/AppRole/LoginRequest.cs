using Newtonsoft.Json;

namespace Vault.Models.Auth.AppRole
{
    public class LoginRequest
    {
        [JsonProperty("role_id")]
        public string RoleId { get; set; }

        [JsonProperty("secret_id")]
        public string SecretId { get; set; }
    }
}
