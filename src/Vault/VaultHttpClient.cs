using System;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Vault
{
    public class VaultHttpClient : IVaultHttpClient
    {
        private static HttpClient HttpClientInitialization()
        {
            HttpClient httpClient = new HttpClient();
            
            #if NET45
                if (!string.IsNullOrEmpty(Vault.VaultOptions.Default.CertPath))
                {
                    WebRequestHandler requestHandler = new WebRequestHandler();
                    requestHandler.ClientCertificateOptions = ClientCertificateOption.Manual;
                    requestHandler.ClientCertificates.Add(new X509Certificate2(Vault.VaultOptions.Default.CertPath));
                    httpClient = new HttpClient(requestHandler);
                }
                else
                    httpClient = new HttpClient();
            #else
                if (!string.IsNullOrEmpty(Vault.VaultOptions.Default.CertPath))
                {
                    var handler = new HttpClientHandler();            
                    handler.ServerCertificateCustomValidationCallback = (request, cert, chain, errors) =>
                    {
                        const SslPolicyErrors unforgivableErrors =
                            SslPolicyErrors.RemoteCertificateNotAvailable |
                            SslPolicyErrors.RemoteCertificateNameMismatch;

                        if ((errors & unforgivableErrors) != 0)
                        {
                            return false;
                        }

                        X509Certificate2 remoteRoot = chain.ChainElements[chain.ChainElements.Count - 1].Certificate;
                        return new X509Certificate2(Vault.VaultOptions.Default.CertPath).RawData.SequenceEqual(remoteRoot.RawData);
                    };
                    httpClient = new HttpClient(handler);
                }
                else
                {
                    httpClient = new HttpClient();
                }
            #endif
            return httpClient;
        }

        private static readonly HttpClient HttpClient = HttpClientInitialization();

        public VaultHttpClient()
        {
            HttpClient.DefaultRequestHeaders.Accept
                .Add(new MediaTypeWithQualityHeaderValue("application/json"));
        }

        public Task<T> Get<T>(Uri uri, string vaultToken, TimeSpan wrapTtl, CancellationToken ct)
        {
            return HttpRequest<T>(HttpMethod.Get, uri, null, vaultToken, wrapTtl, ct);
        }

        public Task<byte[]> GetRaw(Uri uri, string vaultToken, CancellationToken ct)
        {
            return HttpRequestRaw(HttpMethod.Get, uri, null, vaultToken, ct);
        }

        public Task PostVoid<T>(Uri uri, T content, string vaultToken, CancellationToken ct)
        {
            var httpContent = JsonSerialize(content);
            return HttpRequestVoid(HttpMethod.Post, uri, httpContent, vaultToken, ct);
        }

        public Task<TO> Post<TI, TO>(Uri uri, TI content, string vaultToken, TimeSpan wrapTtl, CancellationToken ct)
        {
            var httpContent = JsonSerialize(content);
            return HttpRequest<TO>(HttpMethod.Post, uri, httpContent, vaultToken, wrapTtl, ct);
        }

        public Task PutVoid(Uri uri, string vaultToken, CancellationToken ct)
        {
            return HttpRequestVoid(HttpMethod.Put, uri, null, vaultToken, ct);
        }

        public Task PutVoid<T>(Uri uri, T content, string vaultToken, CancellationToken ct)
        {
            var httpContent = JsonSerialize(content);
            return HttpRequestVoid(HttpMethod.Put, uri, httpContent, vaultToken, ct);
        }

        public Task<T> Put<T>(Uri uri, string vaultToken, TimeSpan wrapTtl, CancellationToken ct)
        {
            return HttpRequest<T>(HttpMethod.Put, uri, null, vaultToken, wrapTtl, ct);
        }

        public Task<TO> Put<TI, TO>(Uri uri, TI content, string vaultToken, TimeSpan wrapTtl, CancellationToken ct)
        {
            var httpContent = JsonSerialize(content);
            return HttpRequest<TO>(HttpMethod.Put, uri, httpContent, vaultToken, wrapTtl, ct);
        }

        public Task DeleteVoid(Uri uri, string vaultToken, CancellationToken ct)
        {
            return HttpRequestVoid(HttpMethod.Delete, uri, null, vaultToken, ct);
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

        private static async Task HttpRequestVoid(HttpMethod method, Uri uri, string body, string vaultToken, CancellationToken ct)
        {
            await HttpRequest(method, uri, body, vaultToken, TimeSpan.Zero, ct).ConfigureAwait(false);
        }

        private static async Task<T> HttpRequest<T>(HttpMethod method, Uri uri, string body, string vaultToken, TimeSpan wrapTtl, CancellationToken ct)
        {
            return JsonDeserialize<T>(await HttpRequest(method, uri, body, vaultToken, wrapTtl, ct).ConfigureAwait(false));
        }

        private static async Task<string> HttpRequest(HttpMethod method, Uri uri, string body, string vaultToken, TimeSpan wrapTtl, CancellationToken ct)
        {
            using (var r = await HttpSendRequest(method, uri, body, vaultToken, wrapTtl, ct).ConfigureAwait(false))
            {
                if (r.StatusCode != HttpStatusCode.NotFound) {
                    if (!r.IsSuccessStatusCode)
                    {
                        throw new VaultRequestException($"Unexpected response, status code {r.StatusCode}", r.StatusCode);
                    }
                    if (r.Content.Headers.ContentType.MediaType != "application/json") {
                        throw new VaultRequestException($"Unexpected content media type {r.Content.Headers.ContentType.MediaType}", HttpStatusCode.InternalServerError);
                    }
                }

                return await r.Content.ReadAsStringAsync().ConfigureAwait(false);
            }
        }

        private static async Task<byte[]> HttpRequestRaw(HttpMethod method, Uri uri, string body, string vaultToken, CancellationToken ct)
        {
            using (var r = await HttpSendRequest(method, uri, body, vaultToken, TimeSpan.Zero, ct).ConfigureAwait(false))
            {
                if (r.StatusCode != HttpStatusCode.NotFound) {
                    if (!r.IsSuccessStatusCode)
                    {
                        throw new VaultRequestException($"Unexpected response, status code {r.StatusCode}", r.StatusCode);
                    }
                    if (r.Content.Headers.ContentType.MediaType == "application/json") {
                        throw new VaultRequestException($"Unexpected content media type {r.Content.Headers.ContentType.MediaType}", HttpStatusCode.InternalServerError);
                    }
                }

                return await r.Content.ReadAsByteArrayAsync().ConfigureAwait(false);
            }
        }

        private static string JsonSerialize<T>(T content)
        {
            return JsonConvert.SerializeObject(content, VaultJsonSerializerSettings());
        }

        private static T JsonDeserialize<T>(string result)
        {
            return JsonConvert.DeserializeObject<T>(result, VaultJsonSerializerSettings());
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
