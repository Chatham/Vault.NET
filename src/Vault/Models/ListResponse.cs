using System.Collections.Generic;
using Newtonsoft.Json;

namespace Vault.Models
{
    public class ListResponse
    {
        [JsonProperty("keys")]
        public List<string> Keys { get; set; }
    }
}
