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
        private static readonly HttpClient HttpClient = new HttpClient();

        public VaultHttpClient()
        {
            HttpClient.DefaultRequestHeaders.Accept
                .Add(new MediaTypeWithQualityHeaderValue("application/json"));
        }

        public Task<T> Get<T>(Uri uri, string vaultToken, TimeSpan wrapTtl, CancellationToken ct)
        {
            return HttpRequest<T>(HttpMethod.Get, uri, null, vaultToken, wrapTtl, ct);
        }

        public async Task PostVoid<T>(Uri uri, T content, string vaultToken, CancellationToken ct)
        {
            var httpContent = await JsonSerialize(content, ct).ConfigureAwait(false);
            await HttpRequestVoid(HttpMethod.Post, uri, httpContent, vaultToken, ct).ConfigureAwait(false);
        }

        public async Task<TO> Post<TI, TO>(Uri uri, TI content, string vaultToken, TimeSpan wrapTtl, CancellationToken ct)
        {
            var httpContent = await JsonSerialize(content, ct).ConfigureAwait(false);
            return await HttpRequest<TO>(HttpMethod.Post, uri, httpContent, vaultToken, wrapTtl, ct).ConfigureAwait(false);
        }

        public Task PutVoid(Uri uri, string vaultToken, CancellationToken ct)
        {
            return HttpRequestVoid(HttpMethod.Put, uri, null, vaultToken, ct);
        }

        public async Task PutVoid<T>(Uri uri, T content, string vaultToken, CancellationToken ct)
        {
            var httpContent = await JsonSerialize(content, ct).ConfigureAwait(false);
            await HttpRequestVoid(HttpMethod.Put, uri, httpContent, vaultToken, ct).ConfigureAwait(false);
        }

        public Task<T> Put<T>(Uri uri, string vaultToken, TimeSpan wrapTtl, CancellationToken ct)
        {
            return HttpRequest<T>(HttpMethod.Put, uri, null, vaultToken, wrapTtl, ct);
        }

        public async Task<TO> Put<TI, TO>(Uri uri, TI content, string vaultToken, TimeSpan wrapTtl, CancellationToken ct)
        {
            var httpContent = await JsonSerialize(content, ct).ConfigureAwait(false);
            return await HttpRequest<TO>(HttpMethod.Put, uri, httpContent, vaultToken, wrapTtl, ct).ConfigureAwait(false);
        }

        public Task DeleteVoid(Uri uri, string vaultToken, CancellationToken ct)
        {
            return HttpRequestVoid(HttpMethod.Delete, uri, null, vaultToken,ct);
        }

        public Task<T> Delete<T>(Uri uri, string vaultToken, CancellationToken ct)
        {
            return HttpRequest<T>(HttpMethod.Delete, uri, null, vaultToken, TimeSpan.Zero, ct);
        }

        private static Task<HttpResponseMessage> HttpSendRequest(HttpMethod method, Uri uri, string body, string vaultToken, TimeSpan wrapTtl, CancellationToken ct)
        {
            var requestMessage = new HttpRequestMessage(method, uri);

            if (vaultToken != null)
            {
                requestMessage.Headers.Add("X-Vault-Token", vaultToken);
            }
            if (wrapTtl != TimeSpan.Zero)
            {
                requestMessage.Headers.Add("X-Vault-Wrap-TTL", $"{(int)wrapTtl.TotalSeconds}");
            }
            if (body != null)
            {
                requestMessage.Content = new StringContent(body, Encoding.UTF8, "application/json");
            }

            return HttpClient.SendAsync(requestMessage, ct);
        }

        private static async Task HttpRequestVoid(HttpMethod method, Uri uri, string body, string vaultToken,CancellationToken ct)
        {
            using (var r = await HttpSendRequest(method, uri, body, vaultToken, TimeSpan.Zero, ct))
            {
                if (r.StatusCode != HttpStatusCode.NotFound && !r.IsSuccessStatusCode)
                {
                    throw new VaultRequestException($"Unexpected response, status code {r.StatusCode}", r.StatusCode);
                }

                await r.Content.ReadAsStringAsync().ConfigureAwait(false);
            }
        }

        private static async Task<T> HttpRequest<T>(HttpMethod method, Uri uri, string body, string vaultToken, TimeSpan wrapTtl, CancellationToken ct)
        {
            using (var r = await HttpSendRequest(method, uri, body, vaultToken, wrapTtl, ct))
            {
                if (r.StatusCode != HttpStatusCode.NotFound && !r.IsSuccessStatusCode)
                {
                    throw new VaultRequestException($"Unexpected response, status code {r.StatusCode}", r.StatusCode);
                }

                var content = await r.Content.ReadAsStringAsync().ConfigureAwait(false);
                return await JsonDeserialize<T>(content, ct).ConfigureAwait(false);
            }
        }

        private static Task<string> JsonSerialize<T>(T content, CancellationToken ct)
        {
            return Task.Run(() => JsonConvert.SerializeObject(content, VaultJsonSerializerSettings()), ct);
        }

        private static Task<T> JsonDeserialize<T>(string result, CancellationToken ct)
        {
            return Task.Run(() => JsonConvert.DeserializeObject<T>(result, VaultJsonSerializerSettings()), ct);
        }

        private static JsonSerializerSettings VaultJsonSerializerSettings()
        {
            return new JsonSerializerSettings
            {
                DefaultValueHandling = DefaultValueHandling.Ignore
            };
        }
    }
}
