using System;
using System.Net.Http;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

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
            return await JsonDeserialize<T>(result, ct).ConfigureAwait(false);
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

        public async Task Delete(Uri uri, CancellationToken ct)
        {
            using (var r = await _httpClient.DeleteAsync(uri, ct).ConfigureAwait(false))
            {
                await r.Content.ReadAsStringAsync().ConfigureAwait(false);
            }
        }

        //private async Task<string> Delete(Uri uri, CancellationToken ct)
        //{
        //    using (var r = await _httpClient.DeleteAsync(uri, ct).ConfigureAwait(false))
        //    {
        //        return await r.Content.ReadAsStringAsync().ConfigureAwait(false);
        //    }
        //}

        //public async Task<T> Delete<T>(Uri uri, CancellationToken ct)
        //{
        //    var result = await Delete(uri, ct).ConfigureAwait(false);
        //    return await JsonDeserialize<T>(result, ct).ConfigureAwait(false);
        //}

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
