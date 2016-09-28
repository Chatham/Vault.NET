using System;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using System.Text;
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

                var rootCaConfig = new RootGenerateRequest
                {
                    CommonName = "Vault Testing Root Certificate Authority",
                    Ttl = "87600h"
                };
                await client.Secret.Write($"{mountPoint}/root/generate/internal", rootCaConfig);

                var roleName = Guid.NewGuid().ToString();
                var role = new RolesRequest
                {
                    AllowAnyDomain = true,
                    EnforceHostnames = false,
                    MaxTtl = "1h"
                };
                await client.Secret.Write($"{mountPoint}/roles/{roleName}", role);
                
                var certRequest = new IssueRequest
                {
                    CommonName = "Test Cert"
                };
                var cert =
                    await
                        client.Secret.Write<IssueRequest, IssueResponse>($"{mountPoint}/issue/{roleName}",
                            certRequest);

                Assert.NotNull(cert.Data);
                Assert.NotNull(cert.Data.Certificate);
                Assert.NotNull(cert.Data.PrivateKey);
            }
        }

        [Fact]
        public async Task SecretPki_SetUpRootCA_CanIssueCertificatesWithAltNames()
        {
            using (var server = new VaultTestServer())
            {
                var client = server.TestClient();

                var mountPoint = Guid.NewGuid().ToString();
                await client.Sys.Mount(mountPoint, new MountInfo { Type = "pki" });

                var mountConfig = new MountConfig
                {
                    MaxLeaseTtl = "87600h"
                };
                await client.Sys.TuneMount(mountPoint, mountConfig);

                var rootCaConfig = new RootGenerateRequest
                {
                    CommonName = "Vault Testing Root Certificate Authority",
                    Ttl = "87600h"
                };
                await client.Secret.Write($"{mountPoint}/root/generate/internal", rootCaConfig);

                var roleName = Guid.NewGuid().ToString();
                var role = new RolesRequest
                {
                    AllowAnyDomain = true,
                    EnforceHostnames = false,
                    MaxTtl = "1h"
                };
                await client.Secret.Write($"{mountPoint}/roles/{roleName}", role);

                var commonName = Guid.NewGuid().ToString();
                var certRequest = new IssueRequest
                {
                    CommonName = commonName,
                    AltNames = new List<string> { "example.com", "test.example.com"},
                    Format = CertificateFormat.Der
                };
                var cert =
                    await
                        client.Secret.Write<IssueRequest, IssueResponse>($"{mountPoint}/issue/{roleName}",
                            certRequest);

                Assert.NotNull(cert.Data);
                Assert.NotNull(cert.Data.Certificate);
                Assert.NotNull(cert.Data.PrivateKey);

                var x509Cert = new X509Certificate2(Encoding.UTF8.GetBytes(cert.Data.Certificate));
                Assert.Equal($"CN={commonName}", x509Cert.SubjectName.Name);
            }
        }

    }
}
