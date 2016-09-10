using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Vault.Endpoints.Secret
{
    public class LogicalEndpoint<TData>
    {
        private readonly VaultClient _client;
        private const string UriBasePath = "/v1";
        private const string WrappedResponseLocation = "cubbyhole/response";

        public LogicalEndpoint(VaultClient client)
        {
            _client = client;
        }

        public Task<Secret<TData>> Read(string path)
        {
            return Read(path, CancellationToken.None);
        }

        public Task<Secret<TData>> Read(string path, CancellationToken ct)
        {
            return _client.Get<Secret<TData>>($"{UriBasePath}/{path}", ct);
        }

        private Task<Secret<TData>> Read(string path, string token, CancellationToken ct)
        {
            return _client.Get<Secret<TData>>($"{UriBasePath}/{path}", token, ct);
        }

        public Task<Secret<List<TData>>> List(string path)
        {
            return List(path, CancellationToken.None);
        }

        public Task<Secret<List<TData>>> List(string path, CancellationToken ct)
        {
            return _client.List<Secret<List<TData>>>($"{UriBasePath}/{path}", ct);
        }

        public Task Write(string path, TData data)
        {
            return Write(path, data, CancellationToken.None);
        }

        public Task Write(string path, TData data, CancellationToken ct)
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

        public Task<Secret<TData>> Unwrap(string unwrappingToken)
        {
            return Unwrap(unwrappingToken, CancellationToken.None);
        }

        public Task<Secret<TData>> Unwrap(string unwrappingToken, CancellationToken ct)
        {
            return Read(WrappedResponseLocation, unwrappingToken, ct);
        }
    }
}
