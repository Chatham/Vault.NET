using Newtonsoft.Json;

namespace Vault.Models.Secret.AWS
{
    public class RolesRequest
    {
        [JsonProperty("policy")]
        public string Policy { get; set; }

        [JsonProperty("arn")]
        public string Arn { get; set; }
    }
}
