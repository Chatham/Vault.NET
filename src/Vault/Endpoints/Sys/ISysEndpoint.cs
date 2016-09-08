using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Vault.Endpoints.Sys
{
    public interface ISysEndpoint
    {
        Task<Secret<Dictionary<string, MountInfo>>> ListMounts();
        Task<Secret<Dictionary<string, MountInfo>>> ListMounts(CancellationToken ct);
        Task Mount(string path, MountInfo mountInfo);
        Task Mount(string path, MountInfo mountInfo, CancellationToken ct);
        Task Unmount(string path);
        Task Unmount(string path, CancellationToken ct);
        Task Remount(string from, string to);
        Task Remount(string from, string to, CancellationToken ct);
        Task TuneMount(string path, MountConfig mountConfig);
        Task TuneMount(string path, MountConfig mountConfig, CancellationToken ct);
        Task<MountConfig> MountConfig(string path);
        Task<MountConfig> MountConfig(string path, CancellationToken ct);

        Task<LeaderResponse> Leader();
        Task<LeaderResponse> Leader(CancellationToken ct);

        Task<GenerateRootStatusResponse> GenerateRootStatus();
        Task<GenerateRootStatusResponse> GenerateRootStatus(CancellationToken ct);
        Task<GenerateRootStatusResponse> GenerateRootInit(string otp, string pgpKey);
        Task<GenerateRootStatusResponse> GenerateRootInit(string otp, string pgpKey, CancellationToken ct);
        Task GenerateRootCancel();
        Task GenerateRootCancel(CancellationToken ct);
        Task<GenerateRootStatusResponse> GenerateRootUpdate(string shard, string nonce);
        Task<GenerateRootStatusResponse> GenerateRootUpdate(string shard, string nonce, CancellationToken ct);

        Task<bool> InitStatus();
        Task<bool> InitStatus(CancellationToken ct);
        Task<InitResponse> Init(InitRequest request);
        Task<InitResponse> Init(InitRequest request, CancellationToken ct);

        Task<SealStatusResponse> SealStatus();
        Task<SealStatusResponse> SealStatus(CancellationToken ct);
        Task Seal();
        Task Seal(CancellationToken ct);
        Task<SealStatusResponse> Unseal(string shard);
        Task<SealStatusResponse> Unseal(string shard, CancellationToken ct);
        Task<SealStatusResponse> ResetUnsealProcess();
        Task<SealStatusResponse> ResetUnsealProcess(CancellationToken ct);

    }
}
