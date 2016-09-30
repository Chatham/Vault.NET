using System.Collections.Generic;
using Newtonsoft.Json;

namespace Vault.Models
{
    public class VaultResponseInfo
    {
        [JsonProperty("request_id")]
        public string RequestId { get; set; }

        [JsonProperty("lease_id")]
        public string LeaseId { get; set; }

        [JsonProperty("renewable")]
        public bool Renewable { get; set; }

        [JsonProperty("lease_duration")]
        public int LeaseDuration { get; set; }

        [JsonProperty("warnings")]
        public List<string> Warnings { get; set; }
    }
}
