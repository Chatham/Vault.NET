using Newtonsoft.Json;

namespace Vault.Models.Auth.Token
{
    public class RenewRequest
    {
        [JsonProperty("increment")]
        public string Increment { get; set; }
    }
}
