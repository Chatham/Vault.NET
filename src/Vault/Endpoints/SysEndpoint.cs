using System;
using System.Threading;
using System.Threading.Tasks;

namespace Vault.Endpoints
{
    public class SysEndpoint : ISysEndpoint
    {
        private readonly VaultClient _client;
        private const string UriPathBase = "/v1/sys";

        public SysEndpoint(VaultClient client)
        {
            _client = client;
        }

        public Task<bool> InitStatus()
        {
            return InitStatus(CancellationToken.None);
        }

        public async Task<bool> InitStatus(CancellationToken ct)
        {
            var result = await _client.Get<InitStatusResponse>($"{UriPathBase}/init", ct);
            return result.Initialized;
        }

        public Task<InitResponse> Init(InitRequest request)
        {
            return Init(request, CancellationToken.None);
        }

        public Task<InitResponse> Init(InitRequest request, CancellationToken ct)
        {
            return _client.Put<InitRequest, InitResponse>($"{UriPathBase}/init", request, ct);
        }

        public Task<GenerateRootStatusResponse> GenerateRootStatus()
        {
            return GenerateRootStatus(CancellationToken.None);
        }

        public Task<GenerateRootStatusResponse> GenerateRootStatus(CancellationToken ct)
        {
            return _client.Get<GenerateRootStatusResponse>($"{UriPathBase}/generate-root/attempt", ct);
        }

        public Task<GenerateRootStatusResponse> GenerateRootInit(GenerateRootInitRequest request)
        {
            return GenerateRootInit(request, CancellationToken.None);
        }

        public Task<GenerateRootStatusResponse> GenerateRootInit(GenerateRootInitRequest request, CancellationToken ct)
        {
            return
                _client.Put<GenerateRootInitRequest, GenerateRootStatusResponse>(
                    $"{UriPathBase}/generate-root/attempt", request, CancellationToken.None);
        }
    }
}
