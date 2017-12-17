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
                    AltNames = new List<string> { "example.com", "test.example.com" },
                    Format = CertificateFormat.Der
                };
                var cert =
                    await
                        client.Secret.Write<IssueRequest, IssueResponse>($"{mountPoint}/issue/{roleName}",
                            certRequest);

                Assert.NotNull(cert.Data);
                Assert.NotNull(cert.Data.Certificate);
                Assert.NotNull(cert.Data.PrivateKey);

                var x509Cert = new X509Certificate2(Convert.FromBase64String(cert.Data.Certificate));
                Assert.Equal($"CN={commonName}", x509Cert.SubjectName.Name);
            }
        }

        [Fact]
        public async Task SecretPki_BuildIntermediateCAChain_CanIssueCertificatesWithChain()
        {
            using (var server = new VaultTestServer())
            {
                var client = server.TestClient();

                await client.Sys.Mount("pki", new MountInfo { Type = "pki" });
                await client.Sys.Mount("pki1", new MountInfo { Type = "pki" });
                await client.Sys.Mount("pki2", new MountInfo { Type = "pki" });
                await client.Sys.Mount("pki3", new MountInfo { Type = "pki" });

                var mountConfig = new MountConfig
                {
                    MaxLeaseTtl = "87600h"
                };
                await client.Sys.TuneMount("pki", mountConfig);
                await client.Sys.TuneMount("pki1", mountConfig);
                await client.Sys.TuneMount("pki2", mountConfig);
                await client.Sys.TuneMount("pki3", mountConfig);

                // Root CA
                var rootCaConfig = new RootGenerateRequest
                {
                    CommonName = "Vault Testing Root Certificate Authority",
                    Ttl = "87600h"
                };
                await client.Secret.Write($"pki/root/generate/internal", rootCaConfig);

                // Intermediate CA
                var pki1CaConfig = new IntermediateGenerateRequest
                {
                    CommonName = "Vault Testing Intermediate CA"
                };
                var pki1Request =
                    await
                        client.Secret.Write<IntermediateGenerateRequest, IntermediateGenerateInternalResponse>(
                            "pki1/intermediate/generate/internal", pki1CaConfig);

                var pki1SignRequest = new RootSignIntermediateRequest
                {
                    Csr = pki1Request.Data.Csr,
                    Format = CertificateFormat.PemBundle,
                    Ttl = "87500h"
                };
                var pki1SignResponse =
                    await
                        client.Secret.Write<RootSignIntermediateRequest, RootSignIntermediateResponse>(
                            "pki/root/sign-intermediate", pki1SignRequest);

                var pki1SetSigned = new IntermediateSetSignedRequest
                {
                    Certificate = pki1SignResponse.Data.Certificate
                };
                await client.Secret.Write("pki1/intermediate/set-signed", pki1SetSigned);


                // PKI2 - Sub Intermediate CA
                var pki2CaConfig = new IntermediateGenerateRequest
                {
                    CommonName = "Vault Testing Sub Intermediate CA"
                };
                var pki2Request =
                    await
                        client.Secret.Write<IntermediateGenerateRequest, IntermediateGenerateInternalResponse>(
                            "pki2/intermediate/generate/internal", pki2CaConfig);

                var pki2SignRequest = new RootSignIntermediateRequest
                {
                    Csr = pki2Request.Data.Csr,
                    Format = CertificateFormat.PemBundle,
                    Ttl = "87400h"
                };
                var pki2SignResponse =
                    await
                        client.Secret.Write<RootSignIntermediateRequest, RootSignIntermediateResponse>(
                            "pki1/root/sign-intermediate", pki2SignRequest);

                var pki2SetSigned = new IntermediateSetSignedRequest
                {
                    Certificate = pki2SignResponse.Data.Certificate
                };
                await client.Secret.Write("pki2/intermediate/set-signed", pki2SetSigned);

                var roleName = Guid.NewGuid().ToString();
                var role = new RolesRequest
                {
                    AllowAnyDomain = true,
                    EnforceHostnames = false,
                    MaxTtl = "1h"
                };
                await client.Secret.Write($"pki2/roles/{roleName}", role);

                var commonName = Guid.NewGuid().ToString();
                var certRequest = new IssueRequest
                {
                    CommonName = commonName,
                    AltNames = new List<string> { "example.com", "test.example.com" },
                    Format = CertificateFormat.Der
                };
                var cert =
                    await
                        client.Secret.Write<IssueRequest, IssueResponse>($"pki2/issue/{roleName}",
                            certRequest);

                Assert.NotNull(cert.Data);
                Assert.NotNull(cert.Data.Certificate);
                Assert.NotNull(cert.Data.PrivateKey);
                Assert.Equal(2, cert.Data.CaChain.Count);
            }
        }
    }
}
