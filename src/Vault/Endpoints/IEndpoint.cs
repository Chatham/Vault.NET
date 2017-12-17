using System;
using System.Threading;
using System.Threading.Tasks;
using Vault.Models;

namespace Vault.Endpoints
{
    public interface IEndpoint
    {
        Task Delete(string path, CancellationToken ct = default(CancellationToken));
        Task<VaultResponse<ListResponse>> List(string path, CancellationToken ct = default(CancellationToken));
        Task<VaultResponse<TData>> Read<TData>(string path, CancellationToken ct = default(CancellationToken));
        Task Write<TParameters>(string path, TParameters data, CancellationToken ct = default(CancellationToken));
        Task<VaultResponse<TData>> Write<TData>(string path, CancellationToken ct = default(CancellationToken));
        Task<VaultResponse<TData>> Write<TParameters, TData>(string path, TParameters data, CancellationToken ct = default(CancellationToken));
        Task<VaultResponse<TData>> Unwrap<TData>(string wrappingToken, CancellationToken ct = default(CancellationToken));

        // Wrapped requests
        Task<WrappedVaultResponse> Read(string path, TimeSpan wrapTtl, CancellationToken ct = default(CancellationToken));
        Task<WrappedVaultResponse> List(string path, TimeSpan wrapTtl, CancellationToken ct = default(CancellationToken));
        Task<WrappedVaultResponse> Write(string path, TimeSpan wrapTtl, CancellationToken ct = default(CancellationToken));
        Task<WrappedVaultResponse> Write<TParameters>(string path, TParameters data, TimeSpan wrapTtl, CancellationToken ct = default(CancellationToken));
    }
}