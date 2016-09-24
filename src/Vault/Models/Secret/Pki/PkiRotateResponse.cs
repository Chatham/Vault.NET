using Newtonsoft.Json;

namespace Vault.Models.Secret.Pki
{
    public class PkiRotateResponse
    {
        [JsonProperty("success")]
        public bool Success { get; set; }
    }
}
