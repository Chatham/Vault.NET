using System.Collections.Generic;
using Newtonsoft.Json;

namespace Vault.Endpoints
{
    public class Secret : Secret<object> { }

    public class Secret<TData>
    {
        [JsonProperty("request_id")]
        public string RequestId { get; set; }

        [JsonProperty("lease_id")]
        public string LeaseId { get; set; }

        [JsonProperty("renewable")]
        public bool Renewable { get; set; }

        [JsonProperty("lease_duration")]
        public int LeaseDuration { get; set; }

        [JsonProperty("data")]
        public TData Data { get; set; }

        [JsonProperty("warnings")]
        public List<string> Warnings { get; set; }

        [JsonProperty("wrap_info")]
        public SecretWrapInfo WrapInfo { get; set; }

        [JsonProperty("auth")]
        public SecretAuth Auth { get; set; }
    }

    public class SecretWrapInfo
    {
        [JsonProperty("token")]
        public string Token { get; set; }

        [JsonProperty("ttl")]
        public int Ttl { get; set; }

        [JsonProperty("creation_time")]
        public string CreationTime { get; set; }

        [JsonProperty("wrapped_accessor")]
        public string WrappedAccessor { get; set; }
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

