using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Vault.Endpoints.Secret
{
    public interface ILogicalEndpoint<TData>
    {
        Task Delete(string path);
        Task Delete(string path, CancellationToken ct);
        Task<Secret<List<TData>>> List(string path);
        Task<Secret<List<TData>>> List(string path, CancellationToken ct);
        Task<Secret<TData>> Read(string path);
        Task<Secret<TData>> Read(string path, CancellationToken ct);
        Task<Secret<TData>> Unwrap(string unwrappingToken);
        Task<Secret<TData>> Unwrap(string unwrappingToken, CancellationToken ct);
        Task Write(string path, TData data);
        Task Write(string path, TData data, CancellationToken ct);
    }
}