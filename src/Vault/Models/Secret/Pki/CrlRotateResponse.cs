using Newtonsoft.Json;

namespace Vault.Models.Secret.Pki
{
    public class CrlRotateResponse
    {
        [JsonProperty("success")]
        public bool Success { get; set; }
    }
}
