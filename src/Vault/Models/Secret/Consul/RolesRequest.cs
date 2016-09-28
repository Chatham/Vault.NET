using System.Runtime.Serialization;
using Newtonsoft.Json;

namespace Vault.Models.Secret.Consul
{
    public class RolesRequest
    {
        [JsonProperty("policy")]
        public string Policy { get; set; }

        [JsonProperty("token_type")]
        public TokenType? TokenType { get; set; }

        [JsonProperty("lease")]
        public string Lease { get; set; }
    }

    public enum TokenType
    {
        [EnumMember(Value = "client")]
        Client,

        [EnumMember(Value = "management")]
        Management
    }
}
