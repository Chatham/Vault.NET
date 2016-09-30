using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Vault.Models;
using Vault.Models.Auth.UserPass;
using Xunit;

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
                var loginResponse = await client.Auth.Write<LoginRequest, VaultResponseWithoutData>($"{mountPoint}/login/{username}", loginRequest);

                Assert.Equal(username, loginResponse.Auth.Metadata["username"]);
                Assert.Equal(usersRequest.Policies, loginResponse.Auth.Policies);
            }
        }
    }
}
