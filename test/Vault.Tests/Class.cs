using System;
using Xunit;
using Vault;
using System.Threading.Tasks;

namespace Vault.Tests
{
    public class Class
    {
        [Fact]
        public async Task TestServer()
        {
            using (var server = new VaultServer())
            {
                server.StartTestServer();

                var config = new VaultClientConfiguration
                {
                    Address = new UriBuilder(server.ListenAddress).Uri,
                    Token = server.RootToken
                };

                var client = new VaultClient(config);

                Console.WriteLine(await client.Sys.InitStatus());

            }
        }
    }
}
