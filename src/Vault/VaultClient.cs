using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.WebUtilities;
using Vault.Endpoints;
using Vault.Endpoints.Sys;
using Microsoft.Extensions.Options;
using System.Collections.Specialized;
using System.Linq;

namespace Vault
{
    public class VaultClient : IVaultClient
    {
        private readonly IVaultHttpClient _httpClient;

        private readonly Lazy<ISysEndpoint> _sys;
        public ISysEndpoint Sys => _sys.Value;

        private readonly Lazy<IEndpoint> _secret;
        public IEndpoint Secret => _secret.Value;

        private readonly Lazy<IEndpoint> _auth;
        public IEndpoint Auth => _auth.Value;

        private const string DefaultAddress = "https://127.0.0.1:8200";
        public Uri Address { get; set; }

        public string Token { get; set; }

        public VaultClient() : this(new Uri(DefaultAddress), null) { }

        public VaultClient(Uri address, string token) : this(new VaultHttpClient(), address, token) { }

        public VaultClient(IVaultHttpClient httpClient, IOptions<VaultConfiguration> options)
            : this(httpClient, new Uri(options.Value.Address), options.Value.Token) { }

        public VaultClient(IVaultHttpClient httpClient, Uri address, string token)
        {
            _httpClient = httpClient;
            Token = token;
            Address = address;

            _sys = new Lazy<ISysEndpoint>(() => new SysEndpoint(this), LazyThreadSafetyMode.PublicationOnly);
            _secret = new Lazy<IEndpoint>(() => new Endpoint(this), LazyThreadSafetyMode.PublicationOnly);
            _auth = new Lazy<IEndpoint>(() => new Endpoint(this, "auth"), LazyThreadSafetyMode.PublicationOnly);
        }

        internal Task<T> Get<T>(string path, CancellationToken ct)
        {
            return Get<T>(path, Token, TimeSpan.Zero, ct);
        }

        internal Task<T> Get<T>(string path, TimeSpan wrapTtl, CancellationToken ct)
        {
            return Get<T>(path, Token, wrapTtl, ct);
        }

        internal Task<T> Get<T>(string path, string token, CancellationToken ct)
        {
            return _httpClient.Get<T>(BuildVaultUri(path), token, TimeSpan.Zero, ct);
        }

        internal Task<T> Get<T>(string path, string token, TimeSpan wrapTtl, CancellationToken ct)
        {
            return _httpClient.Get<T>(BuildVaultUri(path), token, wrapTtl, ct);
        }

        internal Task<T> List<T>(string path, TimeSpan wrapTtl, CancellationToken ct)
        {
            return _httpClient.Get<T>(BuildVaultUri(path, new NameValueCollection { { "list", "true" } }),
                Token, wrapTtl, ct);
        }

        internal Task<TO> Post<TI, TO>(string path, TI content, CancellationToken ct)
        {
            return _httpClient.Post<TI, TO>(BuildVaultUri(path), content, Token, TimeSpan.Zero, ct);
        }

        internal Task<TO> Post<TI, TO>(string path, TI content, TimeSpan wrapTtl, CancellationToken ct)
        {
            return _httpClient.Post<TI, TO>(BuildVaultUri(path), content, Token, wrapTtl, ct);
        }

        internal Task PostVoid<T>(string path, T content, CancellationToken ct)
        {
            return _httpClient.PostVoid(BuildVaultUri(path), content, Token, ct);
        }

        internal Task PutVoid(string path, CancellationToken ct)
        {
            return _httpClient.PutVoid(BuildVaultUri(path), Token, ct);
        }

        internal Task PutVoid<T>(string path, T content, CancellationToken ct)
        {
            return _httpClient.PutVoid(BuildVaultUri(path), content, Token, ct);
        }

        internal Task<TO> Put<TO>(string path, TimeSpan wrapTtl, CancellationToken ct)
        {
            return _httpClient.Put<TO>(BuildVaultUri(path), Token, wrapTtl, ct);
        }

        internal Task<TO> Put<TI, TO>(string path, TI content, CancellationToken ct)
        {
            return _httpClient.Put<TI, TO>(BuildVaultUri(path), content, Token, TimeSpan.Zero, ct);
        }

        internal Task<TO> Put<TI, TO>(string path, TI content, TimeSpan wrapTtl, CancellationToken ct)
        {
            return _httpClient.Put<TI, TO>(BuildVaultUri(path), content, Token, wrapTtl, ct);
        }

        internal Task DeleteVoid(string path, CancellationToken ct)
        {
            return _httpClient.DeleteVoid(BuildVaultUri(path), Token, ct);
        }

        private Uri BuildVaultUri(string path, NameValueCollection parameters = null)
        {
            var uriBuilder = new UriBuilder(Address)
            {
                Path = path
            };

            if (parameters == null) return uriBuilder.Uri;

            var dict = parameters.AllKeys.ToDictionary(t => t, t => parameters[t]);
            uriBuilder.Query = QueryHelpers.AddQueryString(string.Empty, dict);
            return uriBuilder.Uri;
        }
    }
}
