using Newtonsoft.Json;
using System.Collections.Generic;

namespace Vault.Models.Secret.Transit
{
    public class ReadKeyResponse
    {
        [JsonProperty("type")]
        public string Type { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("keys")]
        public Dictionary<string, KeyData> Keys { get; set; }

        [JsonProperty("latest_version")]
        public int LatestVersion { get; set; }

        [JsonProperty("min_decryption_version")]
        public int MinDecriptionVersion { get; set; }

        [JsonProperty("min_encryption_version")]
        public int MinEncryptionVersion { get; set; }

        [JsonProperty("deletion_allowed")]
        public bool? DeletionAllowed { get; set; }

        [JsonProperty("derived")]
        public bool? Derived { get; set; }

        [JsonProperty("exportable")]
        public bool? Exportable { get; set; }

        [JsonProperty("allow_plaintext_backup")]
        public bool? AllowPlaintextBackup { get; set; }

        [JsonProperty("supports_encryption")]
        public bool? SupportsEncryption { get; set; }

        [JsonProperty("supports_decryption")]
        public bool? SupportsDecryption { get; set; }

        [JsonProperty("supports_derivation")]
        public bool? SupportsDerivation { get; set; }

        [JsonProperty("supports_signing")]
        public bool? SupportsSigning { get; set; }

        [JsonProperty("backup_info")]
        public InfoData BackupInfo { get; set; }

        [JsonProperty("restore_info")]
        public InfoData RestoreInfo { get; set; }

        [JsonProperty("convergent_encryption")]
        public bool? ConvergentEncryption { get; set; }

        [JsonProperty("convergent_encryption_version")]
        public int ConvergentEncryptionVersion { get; set; }

        [JsonProperty("kdf")]
        public string Kdf { get; set; }

        [JsonProperty("kdf_mode")]
        public string KdfMode { get; set; }
    }
}
