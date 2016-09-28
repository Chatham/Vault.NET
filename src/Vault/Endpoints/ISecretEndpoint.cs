using System;
using System.Threading;
using System.Threading.Tasks;
using Vault.Models.Secret;

namespace Vault.Endpoints
{
    public interface ISecretEndpoint
    {
        Task Delete(string path);
        Task Delete(string path, CancellationToken ct);
        Task<Secret<TData>> List<TData>(string path);
        Task<Secret<TData>> List<TData>(string path, CancellationToken ct);
        Task<Secret<TData>> Read<TData>(string path);
        Task<Secret<TData>> Read<TData>(string path, CancellationToken ct);
        Task<WrappedSecret> Read(string path, TimeSpan wrapTtl);
        Task<WrappedSecret> Read(string path, TimeSpan wrapTtl, CancellationToken ct);
        Task<Secret<TData>> Unwrap<TData>(string unwrappingToken);
        Task<Secret<TData>> Unwrap<TData>(string unwrappingToken, CancellationToken ct);
        Task Write<TParameters>(string path, TParameters data);
        Task Write<TParameters>(string path, TParameters data, CancellationToken ct);
        Task<Secret<TData>> Write<TParameters, TData>(string path, TParameters data);
        Task<Secret<TData>> Write<TParameters, TData>(string path, TParameters data, CancellationToken ct);
    }
}