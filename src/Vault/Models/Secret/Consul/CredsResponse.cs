using Newtonsoft.Json;

namespace Vault.Models.Secret.Consul
{
    public class CredsResponse
    {
        [JsonProperty("token")]
        public string Token { get; set; }
    }
}
