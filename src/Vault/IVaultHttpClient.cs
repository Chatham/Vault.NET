using System;
using System.Threading;
using System.Threading.Tasks;

namespace Vault
{
    public interface IVaultHttpClient
    {
        Task<T> Get<T>(Uri uri,  string vaultToken, TimeSpan wrapTTL, CancellationToken ct);
        Task<TO> Post<TI, TO>(Uri uri, TI content, string vaultToken, CancellationToken ct);
        Task<TO> Put<TI, TO>(Uri uri, TI content, string vaultToken, CancellationToken ct);
        Task DeleteVoid(Uri uri, string vaultToken, CancellationToken ct);
    }
}