using Newtonsoft.Json;

namespace Vault.Models.Auth.Token
{
    public class LookupRequest
    {
        [JsonProperty("token")]
        public string Token { get; set; }
    }
}
