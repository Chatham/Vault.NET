using Newtonsoft.Json;

namespace Vault.Models.Secret
{
    public class Secret<TData> : SecretInfo
    {
        [JsonProperty("data")]
        public TData Data { get; set; }
    }
}

