using System;
using System.Threading;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Vault.Endpoints
{
    public class SecretEndpoint : ISecretEndpoint
    {
        private readonly VaultClient _client;
        private const string UriBasePath = "/v1";
        private const string WrappedResponseLocation = "cubbyhole/response";

        public SecretEndpoint(VaultClient client)
        {
            _client = client;
        }

        public Task<Secret<TData>> Read<TData>(string path)
        {
            return Read<TData>(path, CancellationToken.None);
        }

        public Task<Secret<TData>> Read<TData>(string path, CancellationToken ct)
        {
            return _client.Get<Secret<TData>>($"{UriBasePath}/{path}", ct);
        }

        private Task<Secret<TData>> Read<TData>(string path, string token, CancellationToken ct)
        {
            return _client.Get<Secret<TData>>($"{UriBasePath}/{path}", token, ct);
        }

        public Task<Secret> Read(string path, TimeSpan wrapTTL)
        {
            return Read(path, wrapTTL, CancellationToken.None);
        }

        public Task<Secret> Read(string path, TimeSpan wrapTTL, CancellationToken ct)
        {
            return _client.Get<Secret>($"{UriBasePath}/{path}", wrapTTL, ct);
        }
 
        public Task<Secret<TData>> List<TData>(string path)
        {
            return List<TData>(path, CancellationToken.None);
        }

        public Task<Secret<TData>> List<TData>(string path, CancellationToken ct)
        {
            return _client.List<Secret<TData>>($"{UriBasePath}/{path}", ct);
        }

        public Task Write<TData>(string path, TData data)
        {
            return Write(path, data, CancellationToken.None);
        }

        public Task Write<TData>(string path, TData data, CancellationToken ct)
        {
            return _client.PutVoid($"{UriBasePath}/{path}", data, ct);
        }

        public Task Delete(string path)
        {
            return Delete(path, CancellationToken.None);
        }

        public Task Delete(string path, CancellationToken ct)
        {
            return _client.DeleteVoid($"{UriBasePath}/{path}", ct);
        }

        public Task<Secret<TData>> Unwrap<TData>(string unwrappingToken)
        {
            return Unwrap<TData>(unwrappingToken, CancellationToken.None);
        }

        public async Task<Secret<TData>> Unwrap<TData>(string unwrappingToken, CancellationToken ct)
        {
            var wrappedSecret = await Read<WrappedSecret<TData>>(WrappedResponseLocation, unwrappingToken, ct).ConfigureAwait(false);
            return await Task.Run(() => JsonConvert.DeserializeObject<Secret<TData>>(wrappedSecret.Data.Response), ct).ConfigureAwait(false); ;
        }

        internal class WrappedSecret<TData>
        {
            public string Response { get; set; }
        }
    }
}
