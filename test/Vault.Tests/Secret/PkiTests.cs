using System;
using System.Threading.Tasks;
using Vault.Endpoints.Sys;
using Vault.Models.Secret.Pki;
using Xunit;

namespace Vault.Tests.Secret
{
    public class PkiTests
    {
        [Fact]
        public async Task SecretPki_SetUpRootCA_CanIssueCertificates()
        {
            using (var server = new VaultTestServer())
            {
                var client = server.TestClient();

                var mountPoint = Guid.NewGuid().ToString();
                await client.Sys.Mount(mountPoint, new MountInfo {Type = "pki"});

                var mountConfig = new MountConfig
                {
                    MaxLeaseTtl = "87600h"
                };
                await client.Sys.TuneMount(mountPoint, mountConfig);

                var rootCaConfig = new PkiRootGenerateRequest
                {
                    CommonName = "Vault Testing Root Certificate Authority",
                    Ttl = "87600h"
                };
                await client.Secret.Write($"{mountPoint}/root/generate/internal", rootCaConfig);

                var roleName = Guid.NewGuid().ToString();
                var role = new PkiRoleRequest
                {
                    AllowAnyDomain = true,
                    EnforceHostnames = false,
                    MaxTtl = "1h"
                };
                await client.Secret.Write($"{mountPoint}/roles/{roleName}", role);

                var certRequest = new PkiIssueRequest
                {
                    CommonName = "Test Cert"
                };
                var cert =
                    await
                        client.Secret.Write<PkiIssueRequest, PkiIssue>($"{mountPoint}/issue/{roleName}",
                            certRequest);

                Assert.NotNull(cert.Data);
                Assert.NotNull(cert.Data.Certificate);
                Assert.NotNull(cert.Data.PrivateKey);
            }
        }
    }
}
