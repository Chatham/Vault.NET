using System.Threading.Tasks;
using Xunit;

namespace Vault.Tests.Endpoint.Sys
{
    public class LeaderTests
    {
        [Fact]
        public async Task Leader_Call_ReturnsLeader()
        {
            using (var server = new VaultTestServer())
            {
                var client = server.TestClient();

                var leader = await client.Sys.Leader();
                
                Assert.NotNull(leader);
                Assert.False(leader.IsSelf);
                Assert.False(leader.HaEnables);
                Assert.NotNull(leader.LeaderAddress);
            }
        }
    }
}
