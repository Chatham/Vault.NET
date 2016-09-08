using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

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
         
        public string Token { get; set; }

        public Task<T> Get<T>(Uri uri, string vaultToken, CancellationToken ct)
        {
            return HttpRequest<T>(HttpMethod.Get, uri, null, vaultToken, ct);
        }

        public async Task PostVoid<T>(Uri uri, T content, string vaultToken, CancellationToken ct)
        {
            var httpContent = await JsonSerialize(content, ct).ConfigureAwait(false);
            await HttpRequestVoid(HttpMethod.Post, uri, httpContent, vaultToken, ct);
        }

        public async Task<TO> Post<TI, TO>(Uri uri, TI content, string vaultToken, CancellationToken ct)
        {
            var httpContent = await JsonSerialize(content, ct).ConfigureAwait(false);
            return await HttpRequest<TO>(HttpMethod.Post, uri, httpContent, vaultToken, ct).ConfigureAwait(false);
        }

        public async Task PutVoid<T>(Uri uri, T content, string vaultToken, CancellationToken ct)
        {
            var httpContent = await JsonSerialize(content, ct).ConfigureAwait(false);
            await HttpRequestVoid(HttpMethod.Put, uri, httpContent, vaultToken, ct);
        }

        public async Task<TO> Put<TI, TO>(Uri uri, TI content, string vaultToken, CancellationToken ct)
        {
            var httpContent = await JsonSerialize(content, ct).ConfigureAwait(false);
            return await HttpRequest<TO>(HttpMethod.Put, uri, httpContent, vaultToken, ct).ConfigureAwait(false);
        }

        public async Task DeleteVoid(Uri uri, string vaultToken, CancellationToken ct)
        {
            await HttpRequestVoid(HttpMethod.Delete, uri, null, vaultToken, ct);
        }

        public async Task<T> Delete<T>(Uri uri, string vaultToken, CancellationToken ct)
        {
            return await HttpRequest<T>(HttpMethod.Delete, uri, null, vaultToken, ct).ConfigureAwait(false);
        }

        private Task<HttpResponseMessage> HttpSendRequest(HttpMethod method, Uri uri, string body, string vaultToken, CancellationToken ct)
        {
            var requestMessage = new HttpRequestMessage(method, uri);

            if (vaultToken != null)
            {
                requestMessage.Headers.Add("X-Vault-Token", vaultToken);
            }
            if (body != null)
            {
                requestMessage.Content = new StringContent(body, Encoding.UTF8, "application/json");
            }

            return _httpClient.SendAsync(requestMessage, ct);
        }

        private async Task HttpRequestVoid(HttpMethod method, Uri uri, string body, string vaultToken, CancellationToken ct)
        {
            using (var r = await HttpSendRequest(method, uri, body, vaultToken, ct))
            {
                await r.Content.ReadAsStringAsync();
            }
        }

        private async Task<T> HttpRequest<T>(HttpMethod method, Uri uri, string body, string vaultToken, CancellationToken ct)
        {
            using (var r = await HttpSendRequest(method, uri, body, vaultToken, ct))
            {
                var content = await r.Content.ReadAsStringAsync();
                return await JsonDeserialize<T>(content, ct).ConfigureAwait(false);
            }
        }

        private readonly JsonSerializerSettings _jsonSettings = new JsonSerializerSettings
        {
            ContractResolver = new PascalCaseToUnderscoreContractResolver()
        };

        private Task<string> JsonSerialize<T>(T content, CancellationToken ct)
        {
            return Task.Run(() => JsonConvert.SerializeObject(content, _jsonSettings), ct);
        }

        private Task<T> JsonDeserialize<T>(string result, CancellationToken ct)
        {
            return Task.Run(() => JsonConvert.DeserializeObject<T>(result, _jsonSettings), ct);
        }

        private class PascalCaseToUnderscoreContractResolver : DefaultContractResolver
        {
            // Extracted from http://humanizr.net/#underscore
            protected override string ResolvePropertyName(string propertyName)
            {
                return Regex.Replace(
                        Regex.Replace(
                            Regex.Replace(
                                propertyName,
                                @"([A-Z]+)([A-Z][a-z])",
                                "$1_$2"),
                            @"([a-z\d])([A-Z])",
                            "$1_$2"),
                        @"[-\s]",
                        "_")
                    .ToLower();
            }
        }
    }
}
