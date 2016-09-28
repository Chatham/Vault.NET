using Newtonsoft.Json;

namespace Vault.Models.Secret.Pki
{
    public class RevokeResponse
    {
        [JsonProperty("revocation_time")]
        public int RevocationTime { get; set; }
    }
}
