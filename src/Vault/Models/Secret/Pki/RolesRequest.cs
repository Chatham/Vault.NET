using System.Collections.Generic;
using Newtonsoft.Json;
using Vault.Util;

namespace Vault.Models.Secret.Pki
{
    public class RolesRequest
    {
        [JsonProperty("ttl")]
        public string Ttl { get; set; }

        [JsonProperty("max_ttl")]
        public string MaxTtl { get; set; }

        [JsonProperty("allow_localhost")]
        public bool? AllowLocalhost { get; set; }

        [JsonProperty("allowed_domains")]
        public List<string> AllowedDomains { get; set; }

        [JsonProperty("allow_bare_domains")]
        public bool? AllowBareDomains { get; set; }

        [JsonProperty("allow_subdomains")]
        public bool? AllowSubdomains { get; set; }

        [JsonProperty("allow_any_name")]
        public bool? AllowAnyDomain { get; set; }

        [JsonProperty("enforce_hostnames")]
        public bool? EnforceHostnames { get; set; }

        [JsonProperty("allow_ip_sans")]
        public bool? AllowIpSans { get; set; }

        [JsonProperty("server_flag")]
        public bool? ServerFlag { get; set; }

        [JsonProperty("client_flag")]
        public bool? ClientFlag { get; set; }

        [JsonProperty("code_signing_flag")]
        public bool? CodeSigningFlag { get; set; }

        [JsonProperty("email_protection_flag")]
        public bool? EmailProtectionFlag { get; set; }

        [JsonProperty("key_type")]
        public PrivateKeyType? KeyType { get; set; }

        [JsonProperty("key_bits")]
        public int? KeyBits { get; set; }

        [JsonProperty("key_usage")]
        public List<string> KeyUsage { get; set; }

        [JsonProperty("use_csr_common_name")]
        public bool? UseCsrCommonName { get; set; }
    }
}
