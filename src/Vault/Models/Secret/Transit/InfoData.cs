using Newtonsoft.Json;
using System;

namespace Vault.Models.Secret.Transit
{
    public class InfoData
    {
        [JsonProperty("time")]
        public DateTime Time { get; set; }

        [JsonProperty("version")]
        public int Version { get; set; }
    }
}
