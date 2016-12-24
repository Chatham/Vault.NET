using Newtonsoft.Json;

namespace Vault.Models.Auth.AppRole
{
    public class SecretIdResponse
    {
        [JsonProperty("secret_id")]
        public string SecretId { get; set; }

        [JsonProperty("secret_id_accessor")]
        public string SecretIdAccessor { get; set; }
    }
}
