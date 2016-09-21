using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Vault.Endpoints.Sys;
using Xunit;

namespace Vault.Tests.Endpoint
{
    public class SecretEndpointTests
    {
        [Fact]
        public async Task Read_NoData_Throws404Exception()
        {
            using (var server = new VaultTestServer())
            {
                var client = server.StartServer();

                var mountPoint = Guid.NewGuid().ToString();
                await client.Sys.Mount(mountPoint, new MountInfo {Type = "generic"});

                Exception actual = null;
                try
                {
                    await client.Secret.Read<Dictionary<string, string>>($"{mountPoint}/bogus/token");
                }
                catch (Exception ex)
                {
                    actual = ex;
                }

                Assert.NotNull(actual);
                Assert.Equal(typeof(System.Net.Http.HttpRequestException), actual.GetType());
                Assert.Contains("404", actual.Message);

            }
        }
    }
}
