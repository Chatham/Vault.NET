using Newtonsoft.Json;

namespace Vault.Models.Secret.Pki
{
    public class RootGenerateRequest : GenerateRequest
    {
        [JsonProperty("max_path_length")]
        public int? MaxPathLenth { get; set; }
    }
}
