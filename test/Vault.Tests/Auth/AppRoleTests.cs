using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using Vault.Models;
using Vault.Models.Auth.AppRole;
using Xunit;
using Xunit.Sdk;

namespace Vault.Tests.Auth
{
    public class AppRoleTests
    {
        [Fact]
        public async Task AuthAppRole_MountAndCreateUser_RetrieveUserSuccessfully()
        {
            using (var server = new VaultTestServer())
            {
                var client = server.TestClient();

                var mountPoint = Guid.NewGuid().ToString();
                await client.Sys.EnableAuth(mountPoint, "approle", "AppRole Mount");

                var rolename = Guid.NewGuid().ToString();
                var roleRequest = new RoleRequest
                {
                    Policies = new List<string>{"test", "test2"}
                };
                await client.Auth.Write($"{mountPoint}/role/{rolename}", roleRequest);

                var roleIdResponse = await client.Auth.Read<RoleIdResponse>($"{mountPoint}/role/{rolename}/role-id");
                var secretIdResponse = await client.Auth.Write<SecretIdResponse>($"{mountPoint}/role/{rolename}/secret-id");

                var loginRequest = new LoginRequest
                {
                    RoleId = roleIdResponse.Data.RoleId,
                    SecretId = secretIdResponse.Data.SecretId
                };
                var loginResponse =
                    await client.Auth.Write<LoginRequest, NoData>($"{mountPoint}/login", loginRequest);

                var expectedPolicies = new List<string>(roleRequest.Policies);
                expectedPolicies.Add("default");
                expectedPolicies.Sort();

                loginResponse.Auth.Policies.Sort();

                Assert.NotNull(loginResponse.Auth);
                Assert.Equal(expectedPolicies, loginResponse.Auth.Policies);
                Assert.NotNull(loginResponse.Auth.ClientToken);
            }
        }
    }
}
