using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Vault.Endpoints.Sys;
using Xunit;

namespace Vault.Tests.Secret
{
    public class KVv2Tests
    {
        [Fact]
        public async Task KVV2Read_NonExistentSecret_ReturnsNullData()
        {
            using (var server = new VaultTestServer())
            {
                var client = server.TestClient();

                var mountPoint = Guid.NewGuid().ToString();
                await client.Sys.Mount(mountPoint, new MountInfo 
                {
                    Type = "kv",
                    Options = new Dictionary<string, string>{{"version", "2"}}
                });

                var secret = await client.Secret.Read<Dictionary<string, string>>($"{mountPoint}/data/bogus/token");

                Assert.NotNull(secret);
                Assert.Null(secret.Data);
                Assert.Null(secret.Warnings);
            }
        }

        [Fact]
        public async Task GenericReadWrite_SecretExists_ReturnsSecretData()
        {
            using (var server = new VaultTestServer())
            {
                var client = server.TestClient();
                var secretPath = "secret/data";

                var expected = new Dictionary<string, Dictionary<string, string>> { 
                    {"data", new Dictionary<string, string>{{"abc", "123"}}}
                };

                var mountPoint = Guid.NewGuid().ToString();
                await client.Sys.Mount(mountPoint, new MountInfo 
                {
                    Type = "kv",
                    Options = new Dictionary<string, string>{{"version", "2"}}
                });
                await client.Secret.Write($"{mountPoint}/data/{secretPath}", expected);

                var secret = await client.Secret.Read<Dictionary<string, Dictionary<string, string>>>($"{mountPoint}/data/{secretPath}");

                Assert.NotNull(secret);
                Assert.NotNull(secret.Data);
                Assert.Null(secret.Warnings);
                Assert.Equal(expected["data"], secret.Data["data"]);
            }
        }

    //     [Fact]
    //     public async Task GenericWrapRead_SecretExists_ReturnsSecretWrappedInformation()
    //     {
    //         using (var server = new VaultTestServer())
    //         {
    //             var client = server.TestClient();
    //             var secretPath = "secret/data";

    //             var expected = new Dictionary<string, string> { { "abc", "123" } };

    //             var mountPoint = Guid.NewGuid().ToString();
    //             await client.Sys.Mount(mountPoint, new MountInfo { Type = "generic" });
    //             await client.Secret.Write($"{mountPoint}/{secretPath}", expected);

    //             var secret = await client.Secret.Read($"{mountPoint}/{secretPath}", TimeSpan.FromSeconds(120));

    //             Assert.NotNull(secret); 
    //             Assert.NotNull(secret.WrapInfo);

    //             var unwrappedSecret = await client.Secret.Unwrap<Dictionary<string, string>>(secret.WrapInfo.Token);
    //             Assert.NotNull(unwrappedSecret);
    //             Assert.NotNull(unwrappedSecret.Data);
    //             Assert.Equal(expected, unwrappedSecret.Data);
    //         }
    //     }

    //     [Fact]
    //     public async Task GenericList_NoSecret_ReturnsNullData()
    //     {
    //         using (var server = new VaultTestServer())
    //         {
    //             var client = server.TestClient();
    //             var secretPath = "secret/data";

    //             var mountPoint = Guid.NewGuid().ToString();
    //             await client.Sys.Mount(mountPoint, new MountInfo { Type = "generic" });

    //             var secret = await client.Secret.List($"{mountPoint}/{secretPath}");

    //             Assert.NotNull(secret);
    //             Assert.Null(secret.Data);
    //         }
    //     }

    //     [Fact]
    //     public async Task GenericList_OneSecret_ReturnsListOfSecretKeys()
    //     {
    //         using (var server = new VaultTestServer())
    //         {
    //             var client = server.TestClient();
    //             var secretPath = "secret/data";

    //             var data = new Dictionary<string, string> { { "abc", "123" } };

    //             var mountPoint = Guid.NewGuid().ToString();
    //             await client.Sys.Mount(mountPoint, new MountInfo { Type = "generic" });
    //             await client.Secret.Write($"{mountPoint}/{secretPath}", data);
    //             await client.Secret.Write($"{mountPoint}/{secretPath}/subdata", data);

    //             var secret = await client.Secret.List($"{mountPoint}/secret/");

    //             var expected = new List<string>
    //             {
    //                 "data",
    //                 "data/"
    //             };

    //             Assert.NotNull(secret);
    //             Assert.Equal(expected, secret.Data.Keys);
    //         }
    //     }

    //     [Fact]
    //     public async Task GenericDelete_SecretRemoved_ReturnsNullSecretData()
    //     {
    //         using (var server = new VaultTestServer())
    //         {
    //             var client = server.TestClient();
    //             var secretPath = "secret/data";

    //             var expected = new Dictionary<string, string> { { "abc", "123" } };

    //             var mountPoint = Guid.NewGuid().ToString();
    //             await client.Sys.Mount(mountPoint, new MountInfo { Type = "generic" });
    //             await client.Secret.Write($"{mountPoint}/{secretPath}", expected);

    //             var secret = await client.Secret.Read<Dictionary<string, string>>($"{mountPoint}/{secretPath}");

    //             Assert.NotNull(secret);
    //             Assert.NotNull(secret.Data);
    //             Assert.Equal(expected, secret.Data);

    //             await client.Secret.Delete($"{mountPoint}/{secretPath}");

    //             secret = await client.Secret.Read<Dictionary<string, string>>($"{mountPoint}/{secretPath}");
                
    //             Assert.NotNull(secret);
    //             Assert.Null(secret.Data);
    //         }
    //     }
    }
}
