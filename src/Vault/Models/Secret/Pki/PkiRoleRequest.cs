using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;

namespace Vault.Models.Secret.Pki
{
    public class PkiRoleRequest
    {
        [JsonProperty("ttl", NullValueHandling = NullValueHandling.Ignore)]
        public string Ttl { get; set; }

        [JsonProperty("max_ttl", NullValueHandling = NullValueHandling.Ignore)]
        public string MaxTtl { get; set; }

        [JsonProperty("allow_localhost")]
        public bool AllowLocalhost { get; set; } = true;

        [JsonProperty("allowed_domains", NullValueHandling = NullValueHandling.Ignore)]
        private string _allowedDomains;

        [JsonIgnore]
        public List<string> AllowedDomains
        {
            get { return _allowedDomains.Split(',').ToList(); }
            set { _allowedDomains = string.Join(",", value); }
        }

        [JsonProperty("allow_bare_domains")]
        public bool AllowBareDomains { get; set; }

        [JsonProperty("allow_subdomains")]
        public bool AllowSubdomains { get; set; }

        [JsonProperty("allow_any_name")]
        public bool AllowAnyDomain { get; set; }

        [JsonProperty("enforce_hostnames")]
        public bool EnforceHostnames { get; set; } = true;

        [JsonProperty("allow_ip_sans")]
        public bool AllowIpSans { get; set; } = true;

        [JsonProperty("server_flag")]
        public bool ServerFlag { get; set; } = true;

        [JsonProperty("client_flag")]
        public bool ClientFlag { get; set; } = true;

        [JsonProperty("code_signing_flag")]
        public bool CodeSigningFlag { get; set; }

        [JsonProperty("email_protection_flag")]
        public bool EmailProtectionFlag { get; set; }

        [JsonProperty("key_type")]
        public PrivateKeyType KeyType { get; set; }

        [JsonProperty("key_bits")]
        public int KeyBits { get; set; } = 2048;

        [JsonProperty("key_usage", NullValueHandling = NullValueHandling.Ignore)]
        private string _keyUsage;

        [JsonIgnore]
        public List<string> KeyUsage
        {
            get { return _keyUsage.Split(',').ToList();  }
            set { _keyUsage = string.Join(",", value); }
        }

        [JsonProperty("use_csr_common_name")]
        public bool UseCsrCommonName { get; set; }
    }
}
