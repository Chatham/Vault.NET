using System;
using System.Threading;
using System.Threading.Tasks;
using Vault.Endpoints;
using Vault.Endpoints.Sys;
using Microsoft.Extensions.Options;
using System.Collections.Generic;
using System.Linq;
using System.Net;

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

        public Uri Address { get; set; }

        private string _token;
        public string Token
        {
            set { _token = value; }
        }

        public VaultClient() : this(VaultOptions.Default) { }

        public VaultClient(IOptions<VaultOptions> options)
            : this(new Uri(options.Value.Address), options.Value.Token) { }

        public VaultClient(Uri address, string token = null)
            : this(new VaultHttpClient(), address, token) { }

        public VaultClient(IVaultHttpClient httpClient, Uri address, string token = null)
        {
            _httpClient = httpClient;
            _token = token;
            Address = address;

            _sys = new Lazy<ISysEndpoint>(() => new SysEndpoint(this), LazyThreadSafetyMode.PublicationOnly);
            _secret = new Lazy<IEndpoint>(() => new Endpoint(this), LazyThreadSafetyMode.PublicationOnly);
            _auth = new Lazy<IEndpoint>(() => new Endpoint(this, "auth"), LazyThreadSafetyMode.PublicationOnly);
        }

        internal Task<T> Get<T>(string path, CancellationToken ct)
        {
            return Get<T>(path, TimeSpan.Zero, ct);
        }

        internal Task<byte[]> GetRaw(string path, CancellationToken ct)
        {
            return _httpClient.GetRaw(BuildVaultUri(path), _token, ct);
        }

        internal Task<T> Get<T>(string path, TimeSpan wrapTtl, CancellationToken ct)
        {
            return _httpClient.Get<T>(BuildVaultUri(path), _token, wrapTtl, ct);
        }

        internal Task<T> List<T>(string path, TimeSpan wrapTtl, CancellationToken ct)
        {
            var parameters = new List<KeyValuePair<string, string>> { new KeyValuePair<string, string>("list", "true") };
            return _httpClient.Get<T>(BuildVaultUri(path, parameters),
                _token, wrapTtl, ct);
        }

        internal Task<TO> Post<TI, TO>(string path, TI content, CancellationToken ct)
        {
            return _httpClient.Post<TI, TO>(BuildVaultUri(path), content, _token, TimeSpan.Zero, ct);
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

        internal Task<TO> Put<TO>(string path, TimeSpan wrapTtl, CancellationToken ct)
        {
            return _httpClient.Put<TO>(BuildVaultUri(path), _token, wrapTtl, ct);
        }

        internal Task<TO> Put<TI, TO>(string path, TI content, CancellationToken ct)
        {
            return _httpClient.Put<TI, TO>(BuildVaultUri(path), content, _token, TimeSpan.Zero, ct);
        }

        internal Task<TO> Put<TI, TO>(string path, TI content, TimeSpan wrapTtl, CancellationToken ct)
        {
            return _httpClient.Put<TI, TO>(BuildVaultUri(path), content, _token, wrapTtl, ct);
        }

        internal Task DeleteVoid(string path, CancellationToken ct)
        {
            return _httpClient.DeleteVoid(BuildVaultUri(path), _token, ct);
        }

        private Uri BuildVaultUri(string path, IEnumerable<KeyValuePair<string, string>> parameters = null)
        {
            var uriBuilder = new UriBuilder(Address)
            {
                Path = path
            };

            if (parameters == null) return uriBuilder.Uri;

            var query = string.Join("&", parameters.Select(x => $"{WebUtility.UrlEncode(x.Key.ToString())}={WebUtility.UrlEncode(x.Value.ToString())}"));
            uriBuilder.Query = query;

            return uriBuilder.Uri;
        }
    }

    public class VaultRequestException : Exception
    {
        public HttpStatusCode StatusCode { get; set; }
        public VaultRequestException() { }
        public VaultRequestException(string message, HttpStatusCode statusCode) : base(message) { StatusCode = statusCode; }
        public VaultRequestException(string message, HttpStatusCode statusCode, Exception inner) : base(message, inner) { StatusCode = statusCode; }
    }
}
