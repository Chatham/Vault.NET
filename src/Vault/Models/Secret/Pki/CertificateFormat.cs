using System.Runtime.Serialization;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Vault.Models.Secret.Pki
{
    [JsonConverter(typeof(StringEnumConverter))]
    public enum CertificateFormat
    {
        [EnumMember(Value = "pem")]
        Pem,

        [EnumMember(Value = "der")]
        Der,

        [EnumMember(Value = "pem_bundle")]
        PemBundle
    }
}
