using System;
using System.Collections.Specialized;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.WebUtilities;
using System.Linq;
using Vault.Endpoints;
using Vault.Endpoints.Sys;

namespace Vault
{
    public class VaultClient : IVaultClient
    {
        private readonly VaultHttpClient _httpClient;
        private string _token;

        public Uri Address { get; set; }

        public string Token
        {
            set { _token = value; }
        }

        public VaultClient(Uri address, string token)
        {
            _httpClient = new VaultHttpClient();
            _token = token;
            Address = address;

            _sys = new Lazy<ISysEndpoint>(() => new SysEndpoint(this), LazyThreadSafetyMode.PublicationOnly);
            _secret = new Lazy<IEndpoint>(() => new Endpoint(this), LazyThreadSafetyMode.PublicationOnly);
            _auth = new Lazy<IEndpoint>(() => new Endpoint(this, "auth"), LazyThreadSafetyMode.PublicationOnly);
        }

        internal Task<T> Get<T>(string path, CancellationToken ct)
        {
            return Get<T>(path, _token, ct);
        }

        internal Task<T> Get<T>(string path, TimeSpan wrapTTL, CancellationToken ct)
        {
            return Get<T>(path, _token, wrapTTL, ct);
        }

        internal Task<T> Get<T>(string path, string token, CancellationToken ct)
        {
            return Get<T>(path, token, TimeSpan.Zero, ct);
        }

        private Task<T> Get<T>(string path, string token, TimeSpan wrapTTL, CancellationToken ct)
        {
            return _httpClient.Get<T>(BuildVaultUri(path), token, wrapTTL, ct);
        }

        internal Task<T> List<T>(string path, CancellationToken ct)
        {
            return List<T>(path, TimeSpan.Zero, ct);
        }

        internal Task<T> List<T>(string path, TimeSpan wrapTTL, CancellationToken ct)
        {
            return _httpClient.Get<T>(BuildVaultUri(path, new NameValueCollection { { "list", "true" } }),
                _token, wrapTTL, ct);
        }

        internal Task<TO> Post<TI, TO>(string path, TI content, CancellationToken ct)
        {
            return _httpClient.Post<TI, TO>(BuildVaultUri(path), content, _token, ct);
        }

        internal Task PostVoid<T>(string path, T content, CancellationToken ct)
        {
            return _httpClient.PostVoid(BuildVaultUri(path), content, _token, ct);
        }

        internal Task PutVoid(string path, CancellationToken ct)
        {
            return _httpClient.PutVoid(BuildVaultUri(path), _token, ct);
        }

        internal Task PutVoid<T>(string path, T content, CancellationToken ct)
        {
            return _httpClient.PutVoid(BuildVaultUri(path), content, _token, ct);
        }

        internal Task<TO> Put<TI, TO>(string path, TI content, CancellationToken ct)
        {
            return _httpClient.Put<TI, TO>(BuildVaultUri(path), content, _token, ct);
        }

        internal Task DeleteVoid(string path, CancellationToken ct)
        {
            return _httpClient.DeleteVoid(BuildVaultUri(path), _token, ct);
        }

        private Uri BuildVaultUri(string path, NameValueCollection parameters = null)
        {
            var uriBuilder = new UriBuilder(Address)
            {
                Path = path
            };

            if (parameters == null) return uriBuilder.Uri;

            var dict = parameters.AllKeys.ToDictionary(t => t, t => parameters[t]);
            uriBuilder.Query = QueryHelpers.AddQueryString(string.Empty, dict); ;

            return uriBuilder.Uri;
        }

        private readonly Lazy<ISysEndpoint> _sys;
        public ISysEndpoint Sys => _sys.Value;

        private readonly Lazy<IEndpoint> _secret;
        public IEndpoint Secret => _secret.Value;

        private readonly Lazy<IEndpoint> _auth;
        public IEndpoint Auth => _auth.Value;
    }
}
