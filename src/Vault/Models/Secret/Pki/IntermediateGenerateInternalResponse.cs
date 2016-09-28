using Newtonsoft.Json;

namespace Vault.Models.Secret.Pki
{
    public class IntermediateGenerateInternalResponse
    {
        [JsonProperty("csr")]
        public string Csr { get; set; }
    }
}
