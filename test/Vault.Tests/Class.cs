using Xunit;
using System.Threading.Tasks;

namespace Vault.Tests
{
    public class Class
    {
        [Fact]
        public async Task TestServer()
        {
            using (var server = new VaultTestServer())
            {
                var client = server.StartServer();
                var mounts = await client.Sys.ListMounts();

                var str = "";
                foreach (var m in mounts.Data)
                {
                    str += m.Key;
                }
            }
        }
    }
}
