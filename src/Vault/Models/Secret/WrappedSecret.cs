using Newtonsoft.Json;

namespace Vault.Models.Secret
{
    public class WrappedSecret : SecretInfo
    {
        [JsonProperty("wrap_info")]
        public SecretWrapInfo WrapInfo { get; set; }
    }

    public class SecretWrapInfo
    {
        [JsonProperty("token")]
        public string Token { get; set; }

        [JsonProperty("ttl")]
        public int Ttl { get; set; }

        [JsonProperty("creation_time")]
        public string CreationTime { get; set; }

        [JsonProperty("wrapped_accessor")]
        public string WrappedAccessor { get; set; }
    }
}
