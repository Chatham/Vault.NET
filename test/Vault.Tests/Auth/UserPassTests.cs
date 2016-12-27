using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using Vault.Models;
using Vault.Models.Auth.UserPass;
using Xunit;
using Xunit.Sdk;

namespace Vault.Tests.Auth
{
    public class UserPassTests
    {
        [Fact]
        public async Task AuthUserPass_MountAndCreateUser_RetrieveUserSuccessfully()
        {
            using (var server = new VaultTestServer())
            {
                var client = server.TestClient();

                var mountPoint = Guid.NewGuid().ToString();
                await client.Sys.EnableAuth(mountPoint, "userpass", "Userpass Mount");

                var username = Guid.NewGuid().ToString();

                var usersRequest = new UsersRequest
                {
                    Password = Guid.NewGuid().ToString(),
                    Policies = new List<string> { "default" },
                    Ttl = "1h",
                    MaxTtl = "2h"
                };
                await client.Auth.Write($"{mountPoint}/users/{username}", usersRequest);

                var loginRequest = new LoginRequest
                {
                    Password = usersRequest.Password
                };
                var loginResponse = await client.Auth.Write<LoginRequest, NoData>($"{mountPoint}/login/{username}", loginRequest);

                Assert.Equal(username, loginResponse.Auth.Metadata["username"]);
                Assert.Equal(usersRequest.Policies, loginResponse.Auth.Policies);
                Assert.NotNull(loginResponse.Auth.ClientToken);

                var usersResponse = await client.Auth.Read<UsersResponse>($"{mountPoint}/users/{username}");
                Assert.Equal(usersRequest.Policies, usersResponse.Data.Policies);

                client.Token = loginResponse.Auth.ClientToken;
                try
                {
                    var ex = await Assert.ThrowsAsync<VaultRequestException>(() => 
                        client.Auth.Read<UsersResponse>($"{mountPoint}/users/{username}"));
                    Assert.Equal(ex.StatusCode, HttpStatusCode.Forbidden);
                }
                catch (AssertActualExpectedException exception)
                {
                    Assert.Equal("(No exception was thrown)", exception.Actual);
                }

                // Login with new unauthenticated client
                var newClient = new VaultClient(new UriBuilder(server.ListenAddress).Uri);
                var newLoginResponse = await newClient.Auth.Write<LoginRequest, NoData>($"{mountPoint}/login/{username}", loginRequest);
                Assert.Equal(username, newLoginResponse.Auth.Metadata["username"]);
                Assert.Equal(usersRequest.Policies, newLoginResponse.Auth.Policies);
                Assert.NotNull(newLoginResponse.Auth.ClientToken);
            }
        }
    }
}
