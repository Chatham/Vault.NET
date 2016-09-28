using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Vault.Models.Secret.Consul
{
    public class ConfigAccessRequest
    {
        [JsonProperty("address")]
        public string Address { get; set; }

        [JsonProperty("scheme")]
        public ConsulScheme? Scheme { get; set; }
        
        [JsonProperty("token")]
        public string Token { get; set; } 
    }

    public enum ConsulScheme
    {
        [EnumMember(Value = "http")]
        Http,
        
        [EnumMember(Value = "https")]
        Https
    }
}
