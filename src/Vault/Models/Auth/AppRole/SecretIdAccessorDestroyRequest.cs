using System;
using Newtonsoft.Json;

namespace Vault.Models.Auth.AppRole
{
    public class SecretIdAccessorDestroyRequest
    {
        [JsonProperty("secret_id_accessor")]
        public string SecretIdAccessor { get; set; }
    }
}
