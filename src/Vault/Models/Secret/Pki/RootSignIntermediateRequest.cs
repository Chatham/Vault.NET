using Newtonsoft.Json;

namespace Vault.Models.Secret.Pki
{
    public class RootSignIntermediateRequest : RootGenerateRequest
    {
        [JsonProperty("csr")]
        public string Csr { get; set; }

        [JsonProperty("use_csr_values")]
        public bool? UseCsrValues { get; set; }
    }
}
