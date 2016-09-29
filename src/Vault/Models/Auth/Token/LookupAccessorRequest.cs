using Newtonsoft.Json;

namespace Vault.Models.Auth.Token
{
    public class LookupAccessorRequest
    {
        [JsonProperty("accessor")]
        public string Accessor { get; set; }
    }
}
