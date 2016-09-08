using System;
using System.Threading;
using System.Threading.Tasks;

namespace Vault
{
    public interface IVaultHttpClient
    {
        Task<T> Get<T>(Uri uri, CancellationToken ct);
        Task<TO> Post<TI, TO>(Uri uri, TI content, CancellationToken ct);
        Task<TO> Put<TI, TO>(Uri uri, TI content, CancellationToken ct);
        Task Delete(Uri uri, CancellationToken ct);
    }
}