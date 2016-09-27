using Newtonsoft.Json;

namespace Vault.Models.Secret.Pki
{
    public class RotateResponse
    {
        [JsonProperty("success")]
        public bool Success { get; set; }
    }
}
