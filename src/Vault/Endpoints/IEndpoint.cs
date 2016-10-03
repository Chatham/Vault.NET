using System;
using System.Threading;
using System.Threading.Tasks;
using Vault.Models;
using Vault.Models.Secret;

namespace Vault.Endpoints
{
    public interface IEndpoint
    {
        Task Delete(string path);
        Task Delete(string path, CancellationToken ct);
        Task<VaultResponse<TData>> List<TData>(string path);
        Task<VaultResponse<TData>> List<TData>(string path, CancellationToken ct);
        Task<VaultResponse<TData>> Read<TData>(string path);
        Task<VaultResponse<TData>> Read<TData>(string path, CancellationToken ct);
        Task<WrappedVaultResponse> Read(string path, TimeSpan wrapTtl);
        Task<WrappedVaultResponse> Read(string path, TimeSpan wrapTtl, CancellationToken ct);
        Task<VaultResponse<TData>> Unwrap<TData>(string unwrappingToken);
        Task<VaultResponse<TData>> Unwrap<TData>(string unwrappingToken, CancellationToken ct);
        Task Write<TParameters>(string path, TParameters data);
        Task Write<TParameters>(string path, TParameters data, CancellationToken ct);
        Task<VaultResponse<TData>> Write<TData>(string path);
        Task<VaultResponse<TData>> Write<TData>(string path, CancellationToken ct);
        Task<VaultResponse<TData>> Write<TParameters, TData>(string path, TParameters data);
        Task<VaultResponse<TData>> Write<TParameters, TData>(string path, TParameters data, CancellationToken ct);
    }
}