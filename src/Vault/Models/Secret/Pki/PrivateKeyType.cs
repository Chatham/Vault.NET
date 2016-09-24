using System.Runtime.Serialization;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Vault.Models.Secret.Pki
{
    [JsonConverter(typeof(StringEnumConverter))]
    public enum PrivateKeyType
    {
        [EnumMember(Value = "rsa")]
        Rsa,

        [EnumMember(Value = "ec")]
        Ec
    }
}
