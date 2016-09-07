using System;

namespace Vault
{
    public class VaultClientConfiguration
    {
        public static readonly VaultClientConfiguration Default = new VaultClientConfiguration
        {
            Address = new Uri("http://localhost:8200")
        };

        public Uri Address { get; set; }
        public string Token { get; set; }
    }
}
