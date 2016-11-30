using System;
using System.Threading;
using System.Threading.Tasks;

namespace Vault
{
    public interface IVaultHttpClient
    {
        Task<T> Get<T>(Uri uri, string vaultToken, CancellationToken ct);
        Task<TO> Post<TI, TO>(Uri uri, TI content, string vaultToken, CancellationToken ct);
        Task PostVoid<T>(Uri uri, T content, string vaultToken, CancellationToken ct);
        Task<TO> Put<TI, TO>(Uri uri, TI content, string vaultToken, CancellationToken ct);
        Task<T> Put<T>(Uri uri, string vaultToken, CancellationToken ct);
        Task PutVoid(Uri uri, string vaultToken, CancellationToken ct);
        Task PutVoid<T>(Uri uri, T content, string vaultToken, CancellationToken ct);
        Task DeleteVoid(Uri uri, string vaultToken, CancellationToken ct);
        Task<T> Delete<T>(Uri uri, string vaultToken, CancellationToken ct);
    }
}