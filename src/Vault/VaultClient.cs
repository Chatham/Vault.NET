using System;
using System.Collections.Specialized;
using System.Threading;
using System.Threading.Tasks;
using Vault.Endpoints;

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

        internal Task<T> Get<T>(string path, NameValueCollection parameters, CancellationToken ct)
        {
            return _httpClient.Get<T>(BuildVaultUri(path, parameters), ct);
        }

        internal Task<TO> Put<TI, TO>(string path, NameValueCollection parameters, TI content, CancellationToken ct)
        {
            return _httpClient.Put<TI, TO>(BuildVaultUri(path, parameters), content, ct);
        }

        private Uri BuildVaultUri(string path, NameValueCollection parameters)
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

        private Sys _sys;
        public ISysEndpoint Sys
        {
            get
            {
                if (_sys == null)
                {
                    lock (_lock)
                    {
                        if (_sys == null)
                        {
                            _sys = new Sys(this);
                        }
                    }
                }
                return _sys;
            }
        }
    }
}
