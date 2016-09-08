using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Vault.Endpoints.Sys
{
    public interface ISysEndpoint : ISysInit, ISysLeader, ISysGenerateRoot, ISysMounts
    {
    }

    public interface ISysMounts
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
    }

    public interface ISysLeader
    {
        Task<LeaderResponse> Leader();
        Task<LeaderResponse> Leader(CancellationToken ct);
    }

    public interface ISysGenerateRoot
    {
        Task<GenerateRootStatusResponse> GenerateRootStatus();
        Task<GenerateRootStatusResponse> GenerateRootStatus(CancellationToken ct);
        Task<GenerateRootStatusResponse> GenerateRootInit(string otp, string pgpKey);
        Task<GenerateRootStatusResponse> GenerateRootInit(string otp, string pgpKey, CancellationToken ct);
        Task GenerateRootCancel();
        Task GenerateRootCancel(CancellationToken ct);
        Task<GenerateRootStatusResponse> GenerateRootUpdate(string shard, string nonce);
        Task<GenerateRootStatusResponse> GenerateRootUpdate(string shard, string nonce, CancellationToken ct);
    }

    public interface ISysInit
    {
        Task<bool> InitStatus();
        Task<bool> InitStatus(CancellationToken ct);
        Task<InitResponse> Init(InitRequest request);
        Task<InitResponse> Init(InitRequest request, CancellationToken ct);
    }
}
