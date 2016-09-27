using System.Collections.Generic;
using Newtonsoft.Json;

namespace Vault.Models.Secret
{
    public class ListInfo
    {
        [JsonProperty("keys")]
        public List<string> Keys { get; set; }
    }
}
