using System.Threading.Tasks;
using Xunit;

namespace Vault.Tests.Sys
{
    public class SealTests
    {
        [Fact]
        public async Task Seal_ReturnsStatus()
        {
            using (var server = new VaultTestServer())
            {
                var client = server.TestClient();

                var status = await client.Sys.SealStatus();
                
                Assert.NotNull(status);
                Assert.False(status.Sealed);
            }
        }
    }
}
