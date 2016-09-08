using System.Threading;
using System.Threading.Tasks;

namespace Vault.Endpoints.Sys
{
    public interface ISysEndpoint
    {
        Task<bool> InitStatus();
        Task<bool> InitStatus(CancellationToken ct);
        Task<InitResponse> Init(InitRequest request);
        Task<InitResponse> Init(InitRequest request, CancellationToken ct);

        Task<GenerateRootStatusResponse> GenerateRootStatus();
        Task<GenerateRootStatusResponse> GenerateRootStatus(CancellationToken ct);
        Task<GenerateRootStatusResponse> GenerateRootInit(string otp, string pgpKey);
        Task<GenerateRootStatusResponse> GenerateRootInit(string otp, string pgpKey, CancellationToken ct);
        Task GenerateRootCancel();
        Task GenerateRootCancel(CancellationToken ct);


    }
}
