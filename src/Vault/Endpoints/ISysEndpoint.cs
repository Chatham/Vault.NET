using System.Threading.Tasks;

namespace Vault.Endpoints
{
    public interface ISysEndpoint
    {
        Task<Sys.SysInitResponse> ReadInit();
    }
}
