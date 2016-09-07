using System.Threading;
using System.Threading.Tasks;

namespace Vault.Endpoints
{
    public interface ISysEndpoint
    {
        Task<SysInitResponse> ReadInit();
        Task<SysInitResponse> ReadInit(CancellationToken ct);
    }

    public class SysInitResponse
    {
        public bool Initialized { get; set; }
    }
}
