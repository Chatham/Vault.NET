using NSubstitute;
using System;
using System.Threading;
using System.Threading.Tasks;
using Vault.Models;
using Xunit;

namespace Vault.Tests
{
    public class VaultClientTests
    {
        [Fact]
        public async Task List_BuildVaultUrl()
        {
            var httpClient = Substitute.For<IVaultHttpClient>();
            var client = new VaultClient(httpClient, new Uri("https://example.com"), "token");

            await client.Secret.List("secret/foo");

            await httpClient.Received().Get<VaultResponse<ListResponse>>(
                new Uri("https://example.com/v1/secret/foo?list=true"),
                Arg.Any<string>(), 
                Arg.Any<TimeSpan>(), 
                Arg.Any<CancellationToken>());
        }
    }
}
