using System;
using System.Collections.Specialized;
using System.Threading;
using System.Threading.Tasks;

namespace Vault
{
    public class VaultClient
    {
        private readonly VaultHttpClient _httpClient;
        private readonly VaultClientConfiguration _config;

        private readonly object _lock = new object();

        public VaultClient(VaultHttpClient httpClient, VaultClientConfiguration config)
        {
            _httpClient = httpClient;
            _config = config;
        }

        internal Task<T> Get<T>(string path, CancellationToken ct, NameValueCollection parameters = null)
        {
            return _httpClient.Get<T>(BuildVaultUri(path, parameters), _config.Token, ct);
        }

        internal Task<TO> Post<TI, TO>(string path, TI content, CancellationToken ct, NameValueCollection parameters = null)
        {
            return _httpClient.Post<TI, TO>(BuildVaultUri(path, parameters), content, _config.Token, ct);
        }

        internal Task PostVoid<T>(string path, T content, CancellationToken ct, NameValueCollection parameters = null)
        {
            return _httpClient.PostVoid(BuildVaultUri(path, parameters), content, _config.Token, ct);
        }

        internal Task PutVoid(string path, CancellationToken ct, NameValueCollection parameters = null)
        {
            return _httpClient.PutVoid(BuildVaultUri(path, parameters), _config.Token, ct);
        }

        internal Task PutVoid<T>(string path, T content, CancellationToken ct, NameValueCollection parameters = null)
        {
            return _httpClient.PutVoid(BuildVaultUri(path, parameters), content, _config.Token, ct);
        }

        internal Task<TO> Put<TI, TO>(string path, TI content, CancellationToken ct, NameValueCollection parameters = null)
        {
            return _httpClient.Put<TI, TO>(BuildVaultUri(path, parameters), content, _config.Token, ct);
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

            if (parameters != null)
            {
                uriBuilder.Query = parameters.ToString();
            }

            return uriBuilder.Uri;
        }

        private Endpoints.Sys.SysEndpoint _sys;
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
    }
}
