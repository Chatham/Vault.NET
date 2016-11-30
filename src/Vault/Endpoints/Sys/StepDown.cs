using System.Threading;
using System.Threading.Tasks;

namespace Vault.Endpoints.Sys
{
    public partial class SysEndpoint
    {
        public Task StepDown(CancellationToken ct = default(CancellationToken))
        {
            return _client.PutVoid($"{UriPathBase}/step-down", ct);
        }
    }
}
