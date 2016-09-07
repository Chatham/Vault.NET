using System;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Vault
{
    public class VaultHttpClient : IVaultHttpClient
    {
        private readonly HttpClient _httpClient = new HttpClient();

        private async Task<string> Get(Uri uri, CancellationToken ct)
        {
            using (var r = await _httpClient.GetAsync(uri, ct).ConfigureAwait(false))
            {
                return await r.Content.ReadAsStringAsync().ConfigureAwait(false);
            }
        }

        public async Task<T> Get<T>(Uri uri, CancellationToken ct)
        {
            var result = await Get(uri, ct).ConfigureAwait(false);
            return await JsonDeserialize<T>(result, ct);
        }

        private async Task<string> Post(Uri uri, HttpContent content, CancellationToken ct)
        {
            using (var r = await _httpClient.PostAsync(uri, content, ct).ConfigureAwait(false))
            {
                return await r.Content.ReadAsStringAsync().ConfigureAwait(false);
            }
        }

        public async Task<TO> Post<TI, TO>(Uri uri, TI content, CancellationToken ct)
        {
            var httpContent = await JsonSerialize(content, ct).ConfigureAwait(false);
            var result = await Post(uri, new StringContent(httpContent), ct).ConfigureAwait(false);
            return await JsonDeserialize<TO>(result, ct).ConfigureAwait(false);
        }

        private async Task<string> Put(Uri uri, HttpContent content, CancellationToken ct)
        {
            using (var r = await _httpClient.PutAsync(uri, content, ct).ConfigureAwait(false))
            {
                return await r.Content.ReadAsStringAsync().ConfigureAwait(false);
            }
        }

        public async Task<TO> Put<TI, TO>(Uri uri, TI content, CancellationToken ct)
        {
            var httpContent = await JsonSerialize(content, ct).ConfigureAwait(false);
            var result = await Put(uri, new StringContent(httpContent), ct).ConfigureAwait(false);
            return await JsonDeserialize<TO>(result, ct).ConfigureAwait(false);
        }

        private async Task<string> Delete(Uri uri, CancellationToken ct)
        {
            using (var r = await _httpClient.DeleteAsync(uri, ct).ConfigureAwait(false))
            {
                return await r.Content.ReadAsStringAsync().ConfigureAwait(false);
            }
        }

        public async Task<T> Delete<T>(Uri uri, CancellationToken ct)
        {
            var result = await Delete(uri, ct).ConfigureAwait(false);
            return await JsonDeserialize<T>(result, ct).ConfigureAwait(false); ;
        }

        private static async Task<string> JsonSerialize<T>(T content, CancellationToken ct)
        {
            return await Task.Run(() => JsonConvert.SerializeObject(content), ct).ConfigureAwait(false);   
        }

        private static async Task<T> JsonDeserialize<T>(string result, CancellationToken ct)
        {
            return await Task.Run(() => JsonConvert.DeserializeObject<T>(result), ct).ConfigureAwait(false);
        }
    }
}
