using System;
using System.Collections.Specialized;
using System.Threading;
using System.Threading.Tasks;

namespace Vault
{
    public class VaultClient
    {
        public readonly VaultClientConfiguration Config;

        private readonly VaultHttpClient _httpClient;
        private readonly object _lock = new object();

        public VaultClient(VaultHttpClient httpClient, VaultClientConfiguration config)
        {
            _httpClient = httpClient;

            Config = config;
        }

        internal Task<T> Get<T>(string path, CancellationToken ct, NameValueCollection parameters = null)
        {
            return _httpClient.Get<T>(BuildVaultUri(path, parameters), ct);
        }

        internal Task<TO> Put<TI, TO>(string path, TI content, CancellationToken ct, NameValueCollection parameters = null)
        {
            return _httpClient.Put<TI, TO>(BuildVaultUri(path, parameters), content, ct);
        }

        private Uri BuildVaultUri(string path, NameValueCollection parameters = null)
        {
            var uriBuilder = new UriBuilder(Config.Address)
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
