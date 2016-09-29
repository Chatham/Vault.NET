using Newtonsoft.Json;

namespace Vault.Models.Secret.Pki
{
    public class RootSignIntermediateRequest : SignRequest
    {
        [JsonProperty("use_csr_values")]
        public bool? UseCsrValues { get; set; }
    }
}
