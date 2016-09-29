using Newtonsoft.Json;

namespace Vault.Models.Secret.Pki
{
    public class RevokeRequest
    {
        [JsonProperty("serial_number")]
        public string SerialNumber { get; set; }
    }
}
