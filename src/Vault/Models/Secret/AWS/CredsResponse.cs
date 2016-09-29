using Newtonsoft.Json;

namespace Vault.Models.Secret.AWS
{
    public class CredsResponse
    {
        [JsonProperty("access_key")]
        public string AccessKey { get; set; }

        [JsonProperty("secret_key")]
        public string SecretKey { get; set; }

        [JsonProperty("secret_token")]
        public string SecretToken { get; set; }
    }
}
