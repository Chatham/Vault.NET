using Newtonsoft.Json;

namespace Vault.Models.Secret.AWS
{
    public class ConfigLeaseRequest
    {
        [JsonProperty("lease")]
        public string Lease { get; set; }

        [JsonProperty("lease_max")]
        public string LeaseMax { get; set; }
    }
}
