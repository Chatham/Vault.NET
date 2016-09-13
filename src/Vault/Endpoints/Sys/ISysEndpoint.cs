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

        Task<Secret<object>> Renew(string leaseId, int increment);
        Task<Secret<object>> Renew(string leaseId, int increment, CancellationToken ct);
        Task Revoke(string id);
        Task Revoke(string id, CancellationToken ct);
        Task RevokeForce(string id);
        Task RevokeForce(string id, CancellationToken ct);
        Task RevokePrefix(string id);
        Task RevokePrefix(string id, CancellationToken ct);

        Task<List<string>> Capabilities(string token, string path);
        Task<List<string>> Capabilities(string token, string path, CancellationToken ct);
        Task<List<string>> CapabilitiesSelf(string path);
        Task<List<string>> CapabilitiesSelf(string path, CancellationToken ct);

        Task Rotate();
        Task Rotate(CancellationToken ct);
        Task<KeyStatus> KeyStatus();
        Task<KeyStatus> KeyStatus(CancellationToken ct);

        Task<string> AuditHash(string path, string input);
        Task<string> AuditHash(string path, string input, CancellationToken ct);
        Task<Dictionary<string, Audit>> ListAudit();
        Task<Dictionary<string, Audit>> ListAudit(CancellationToken ct);
        Task EnableAudit(string path, string auditType, string description, Dictionary<string, string> option);
        Task EnableAudit(string path, string auditType, string description, Dictionary<string, string> options, CancellationToken ct);
        Task DisableAudit(string path);
        Task DisableAudit(string path, CancellationToken ct);

        Task<List<string>> ListPolicies();
        Task<List<string>> ListPolicies(CancellationToken ct);
        Task<string> GetPolicy(string name);
        Task<string> GetPolicy(string name, CancellationToken ct);
        Task PutPolicy(string name, string rules);
        Task PutPolicy(string name, string rules, CancellationToken ct);
        Task DeletePolicy(string name);
        Task DeletePolicy(string name, CancellationToken ct);

        Task StepDown();
        Task StepDown(CancellationToken ct);
    }
}
