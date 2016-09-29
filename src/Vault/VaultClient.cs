using System;
using System.Collections.Specialized;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.WebUtilities;
using System.Linq;

namespace Vault
{
    public class VaultClient : IVaultClient
    {
        private readonly VaultHttpClient _httpClient;
        private readonly VaultClientConfiguration _config;

        private readonly object _lock = new object();

        public VaultClient() : this(VaultClientConfiguration.Default)
        {
        }

        public VaultClient(VaultClientConfiguration config)
        {
            _httpClient = new VaultHttpClient();
            _config = config;
        }

        internal Task<T> Get<T>(string path, CancellationToken ct)
        {
            return Get<T>(path, _config.Token, ct);
        }

        internal Task<T> Get<T>(string path, TimeSpan wrapTTL, CancellationToken ct)
        {
            return Get<T>(path, _config.Token, wrapTTL, ct);
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
                _config.Token, wrapTTL, ct);
        }

        internal Task<TO> Post<TI, TO>(string path, TI content, CancellationToken ct)
        {
            return _httpClient.Post<TI, TO>(BuildVaultUri(path), content, _config.Token, ct);
        }

        internal Task PostVoid<T>(string path, T content, CancellationToken ct)
        {
            return _httpClient.PostVoid(BuildVaultUri(path), content, _config.Token, ct);
        }

        internal Task PutVoid(string path, CancellationToken ct)
        {
            return _httpClient.PutVoid(BuildVaultUri(path), _config.Token, ct);
        }

        internal Task PutVoid<T>(string path, T content, CancellationToken ct)
        {
            return _httpClient.PutVoid(BuildVaultUri(path), content, _config.Token, ct);
        }

        internal Task<TO> Put<TI, TO>(string path, TI content, CancellationToken ct)
        {
            return _httpClient.Put<TI, TO>(BuildVaultUri(path), content, _config.Token, ct);
        }

        internal Task DeleteVoid(string path, CancellationToken ct)
        {
            return _httpClient.DeleteVoid(BuildVaultUri(path), _config.Token, ct);
        }

        private Uri BuildVaultUri(string path, NameValueCollection parameters = null)
        {
            var uriBuilder = new UriBuilder(_config.Address)
            {
                Path = path
            };

            if (parameters == null) return uriBuilder.Uri;

            var dict = parameters.AllKeys.ToDictionary(t => t, t => parameters[t]);
            uriBuilder.Query = QueryHelpers.AddQueryString(string.Empty, dict); ;

            return uriBuilder.Uri;
        }

        private Endpoints.Sys.ISysEndpoint _sys;
        public Endpoints.Sys.ISysEndpoint Sys
        {
            get
            {
                if (_sys == null)
                {
                    lock (_lock)
                    {
                        if (_sys == null)
                        {
                            _sys = new Endpoints.Sys.SysEndpoint(this);
                        }
                    }
                }
                return _sys;
            }
        }

        private Endpoints.IEndpoint _secret;
        public Endpoints.IEndpoint Secret
        {
            get
            {
                if (_secret == null)
                {
                    lock (_lock)
                    {
                        if (_secret == null)
                        {
                            _secret = new Endpoints.Endpoint(this);
                        }
                    }
                }
                return _secret;
            }
        }

        private Endpoints.IEndpoint _auth;
        public Endpoints.IEndpoint Auth
        {
            get
            {
                if (_auth == null)
                {
                    lock (_lock)
                    {
                        if (_auth == null)
                        {
                            _auth = new Endpoints.Endpoint(this, "auth");
                        }
                    }
                }
                return _auth;
            }
        }

    }
}
