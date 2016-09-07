using System.Threading;
using System.Threading.Tasks;

namespace Vault.Endpoints
{
    public class Sys : ISysEndpoint
    {
        private readonly VaultClient _client;
        private const string UriPathBase = "/v1/sys";

        public Sys(VaultClient client)
        {
            _client = client;
        }

        public Task<GetInitResponse> GetInit()
        {
            return GetInit(CancellationToken.None);
        }

        public Task<GetInitResponse> GetInit(CancellationToken ct)
        {
            return _client.Get<GetInitResponse>($"{UriPathBase}/init", null, ct);
        }

        public Task<PutInitResponse> PutInit(PutInitRequest request)
        {
            return PutInit(request, CancellationToken.None);
        }

        public Task<PutInitResponse> PutInit(PutInitRequest request, CancellationToken ct)
        {
            return _client.Put<PutInitRequest, PutInitResponse>($"{UriPathBase}/init", null, request, ct);
        }
    }
}