using System.Collections.Generic;
using Newtonsoft.Json;

namespace Vault.Models
{
    public class NoData : object { }

    public class VaultResponse<TData> : VaultResponseInfo
    {
        [JsonProperty("data")]
        public TData Data { get; set; }

        [JsonProperty("auth")]
        public SecretAuth Auth { get; set; }
    }

    public class SecretAuth
    {
        [JsonProperty("client_token")]
        public string ClientToken { get; set; }

        [JsonProperty("accessor")]
        public string Accessor { get; set; }

        [JsonProperty("policies")]
        public List<string> Policies { get; set; }

        [JsonProperty("metadata")]
        public Dictionary<string, string> Metadata { get; set; }

        [JsonProperty("lease_duration")]
        public int LeaseDuration { get; set; }

        [JsonProperty("renewable")]
        public bool Renewable { get; set; }
    }
}

