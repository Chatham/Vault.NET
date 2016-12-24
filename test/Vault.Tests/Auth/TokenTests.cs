using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Vault.Models;
using Vault.Models.Auth.Token;
using Xunit;

namespace Vault.Tests.Auth
{
    public class TokenTests
    {
        [Fact]
        public async Task AuthToken_Create_ReturnsToken()
        {
            using (var server = new VaultTestServer())
            {
                var client = server.TestClient();

                var tokenRequest = new CreateRequest();
                var result = await client.Auth.Write<CreateRequest, NoData>("token/create", tokenRequest);

                Assert.NotNull(result.Auth);
                Assert.NotNull(result.Auth.ClientToken);
            }
        }

        [Fact]
        public async Task AuthToken_CreateWithPolicies_ReturnsTokenWithPolicies()
        {
            using (var server = new VaultTestServer())
            {
                var client = server.TestClient();

                var policyName = Guid.NewGuid().ToString();
                var policy = "path \"*\" {\n" +
                                      "    policy = \"sudo\"\n" +
                                      "}\n";
                await client.Sys.PutPolicy(policyName, policy);

                var tokenRequest = new CreateRequest
                {
                    Policies = new List<string> { policyName }
                };
                var result = await client.Auth.Write<CreateRequest, NoData>("token/create", tokenRequest);

                Assert.NotNull(result.Auth);
                Assert.NotNull(result.Auth.ClientToken);
                Assert.Contains(policyName, result.Auth.Policies);
            }
        }
    }
}
