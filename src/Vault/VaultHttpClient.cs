using System;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Vault
{
    public class VaultHttpClient : IVaultHttpClient
    {
        private readonly HttpClient _httpClient;

        public VaultHttpClient()
        {
            _httpClient = new HttpClient();
            _httpClient.DefaultRequestHeaders.Accept
                .Add(new MediaTypeWithQualityHeaderValue("application/json"));
        }

        public Task<T> Get<T>(Uri uri, string vaultToken, TimeSpan wrapTTL, CancellationToken ct)
        {
            return HttpRequest<T>(HttpMethod.Get, uri, null, vaultToken, wrapTTL, ct);
        }

        public async Task PostVoid<T>(Uri uri, T content, string vaultToken, CancellationToken ct)
        {
            var httpContent = await JsonSerialize(content, ct).ConfigureAwait(false);
            await HttpRequestVoid(HttpMethod.Post, uri, httpContent, vaultToken, ct).ConfigureAwait(false);
        }

        public async Task<TO> Post<TI, TO>(Uri uri, TI content, string vaultToken, CancellationToken ct)
        {
            var httpContent = await JsonSerialize(content, ct).ConfigureAwait(false);
            return await HttpRequest<TO>(HttpMethod.Post, uri, httpContent, vaultToken, ct).ConfigureAwait(false);
        }

        public async Task PutVoid(Uri uri, string vaultToken, CancellationToken ct)
        {
            await HttpRequestVoid(HttpMethod.Put, uri, null, vaultToken, ct).ConfigureAwait(false);
        }

        public async Task PutVoid<T>(Uri uri, T content, string vaultToken, CancellationToken ct)
        {
            var httpContent = await JsonSerialize(content, ct).ConfigureAwait(false);
            await HttpRequestVoid(HttpMethod.Put, uri, httpContent, vaultToken, ct).ConfigureAwait(false);
        }

        public async Task<TO> Put<TI, TO>(Uri uri, TI content, string vaultToken, CancellationToken ct)
        {
            var httpContent = await JsonSerialize(content, ct).ConfigureAwait(false);
            return await HttpRequest<TO>(HttpMethod.Put, uri, httpContent, vaultToken, ct).ConfigureAwait(false);
        }

        public async Task DeleteVoid(Uri uri, string vaultToken, CancellationToken ct)
        {
            await HttpRequestVoid(HttpMethod.Delete, uri, null, vaultToken, ct).ConfigureAwait(false);
        }

        public async Task<T> Delete<T>(Uri uri, string vaultToken, CancellationToken ct)
        {
            return await HttpRequest<T>(HttpMethod.Delete, uri, null, vaultToken, ct).ConfigureAwait(false);
        }

        private Task<HttpResponseMessage> HttpSendRequest(HttpMethod method, Uri uri, string body, string vaultToken, TimeSpan wrapTTL, CancellationToken ct)
        {
            var requestMessage = new HttpRequestMessage(method, uri);

            if (vaultToken != null)
            {
                requestMessage.Headers.Add("X-Vault-Token", vaultToken);
            }
            if (wrapTTL != TimeSpan.Zero)
            {
                requestMessage.Headers.Add("X-Vault-Wrap-TTL", $"{(int)wrapTTL.TotalSeconds}");
            }
            if (body != null)
            {
                requestMessage.Content = new StringContent(body, Encoding.UTF8, "application/json");
            }

            return _httpClient.SendAsync(requestMessage, ct);
        }

        private async Task HttpRequestVoid(HttpMethod method, Uri uri, string body, string vaultToken, CancellationToken ct)
        {
            using (var r = await HttpSendRequest(method, uri, body, vaultToken, TimeSpan.Zero, ct))
            {
                if (r.StatusCode != HttpStatusCode.NotFound && !r.IsSuccessStatusCode)
                {
                    throw new Exceptions.VaultRequestException($"Unexpected response, status code {r.StatusCode}", r.StatusCode);
                }

                await r.Content.ReadAsStringAsync().ConfigureAwait(false);
            }
        }

        private async Task<T> HttpRequest<T>(HttpMethod method, Uri uri, string body, string vaultToken, CancellationToken ct)
        {
            return await HttpRequest<T>(method, uri, body, vaultToken, TimeSpan.Zero, ct).ConfigureAwait(false);
        }

        private async Task<T> HttpRequest<T>(HttpMethod method, Uri uri, string body, string vaultToken, TimeSpan wrapTTL, CancellationToken ct)
        {
            using (var r = await HttpSendRequest(method, uri, body, vaultToken, wrapTTL, ct))
            {
                if (r.StatusCode != HttpStatusCode.NotFound && !r.IsSuccessStatusCode)
                {
                    throw new Exceptions.VaultRequestException($"Unexpected response, status code {r.StatusCode}", r.StatusCode);
                }

                var content = await r.Content.ReadAsStringAsync().ConfigureAwait(false);
                return await JsonDeserialize<T>(content, ct).ConfigureAwait(false);
            }
        }

        private static Task<string> JsonSerialize<T>(T content, CancellationToken ct)
        {
            return Task.Run(() => JsonConvert.SerializeObject(content), ct);
        }

        private static Task<T> JsonDeserialize<T>(string result, CancellationToken ct)
        {
            return Task.Run(() => JsonConvert.DeserializeObject<T>(result), ct);
        }
    }
}
