using Newtonsoft.Json;

namespace Vault.Models.Secret.AWS
{
    public class ConfigRootRequest
    {
        [JsonProperty("access_key")]
        public string AccessKey { get; set; }

        [JsonProperty("secret_key")]
        public string SecretKey { get; set; }

        [JsonProperty("region")]
        public string Region { get; set; }
    }
}
