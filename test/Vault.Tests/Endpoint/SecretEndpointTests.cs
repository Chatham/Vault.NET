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
        public async Task Read_NonExistentSecret_ReturnsNullData()
        {
            using (var server = new VaultTestServer())
            {
                var client = server.TestClient();

                var mountPoint = Guid.NewGuid().ToString();
                await client.Sys.Mount(mountPoint, new MountInfo {Type = "generic"});

                var secret = await client.Secret.Read<Dictionary<string, string>>($"{mountPoint}/bogus/token");

                Assert.NotNull(secret);
                Assert.Null(secret.Data);
            }
        }

        [Fact]
        public async Task ReadWrite_SecretExists_ReturnsSecretData()
        {
            using (var server = new VaultTestServer())
            {
                var client = server.TestClient();
                var secretPath = "secret/data";

                var expected = new Dictionary<string, string> {{"abc", "123"}};

                var mountPoint = Guid.NewGuid().ToString();
                await client.Sys.Mount(mountPoint, new MountInfo { Type = "generic" });
                await client.Secret.Write($"{mountPoint}/{secretPath}", expected);

                var secret = await client.Secret.Read<Dictionary<string, string>>($"{mountPoint}/{secretPath}");

                Assert.NotNull(secret);
                Assert.NotNull(secret.Data);
                Assert.Equal(expected, secret.Data);
            }
        }

        [Fact]
        public async Task List_NoSecret_ReturnsNullData()
        {
            using (var server = new VaultTestServer())
            {
                var client = server.TestClient();
                var secretPath = "secret/data";

                var expected = new Dictionary<string, string> { { "abc", "123" } };

                var mountPoint = Guid.NewGuid().ToString();
                await client.Sys.Mount(mountPoint, new MountInfo { Type = "generic" });
                await client.Secret.Write($"{mountPoint}/{secretPath}", expected);

                var secret = await client.Secret.Read<Dictionary<string, string>>($"{mountPoint}/{secretPath}");

                Assert.NotNull(secret);
                Assert.NotNull(secret.Data);
                Assert.Equal(expected, secret.Data);
            }
        }

    }
}
