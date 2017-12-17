using Microsoft.Extensions.Options;

namespace Vault
{
    public class VaultOptions : IOptions<VaultOptions>
    {
        public static VaultOptions Default = new VaultOptions();

        public string Address { get; set; } = "https://localhost:8200";
        public string Token { get; set; }

        VaultOptions IOptions<VaultOptions>.Value => this;
    }
}
