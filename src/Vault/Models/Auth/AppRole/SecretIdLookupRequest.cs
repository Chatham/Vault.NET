using System;
using Newtonsoft.Json;

namespace Vault.Models.Auth.AppRole
{
    public class SecretIdLookupRequest
    {
        [JsonProperty("secret_id")]
        public string SecretId { get; set; }
    }
}
