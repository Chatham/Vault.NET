using System;
using System.Threading;
using System.Threading.Tasks;
using Vault.Models;

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
        Task<Secret> Read(string path, TimeSpan wrapTTL);
        Task<Secret> Read(string path, TimeSpan wrapTTL, CancellationToken ct);
        Task<Secret<TData>> Unwrap<TData>(string unwrappingToken);
        Task<Secret<TData>> Unwrap<TData>(string unwrappingToken, CancellationToken ct);
        Task Write<TParameters>(string path, TParameters data);
        Task Write<TParameters>(string path, TParameters data, CancellationToken ct);
        Task<Secret<TData>> Write<TParameters, TData>(string path, TParameters data);
        Task<Secret<TData>> Write<TParameters, TData>(string path, TParameters data, CancellationToken ct);
    }
}