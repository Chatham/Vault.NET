using System.Collections.Specialized;
using System.Threading;
using System.Threading.Tasks;

namespace Vault.Endpoints
{
    public class Logical
    {
        private readonly VaultClient _client;
        private const string UriBasePath = "/v1";
        private const string WrappedResponseLocation = "cubbyhole/response";

        public Logical(VaultClient client)
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

        public Task<Secret<TData>> List<TData>(string path)
        {
            return List<TData>(path, CancellationToken.None);
        }

        public Task<Secret<TData>> List<TData>(string path, CancellationToken ct)
        {
            var queryParams = new NameValueCollection {{"list", "true"}};
            return _client.Get<Secret<TData>>($"{UriBasePath}/{path}", ct, queryParams);
        }

        public Task Write<T>(string path, T data)
        {
            return Write(path, data, CancellationToken.None);
        }

        public Task Write<T>(string path, T data, CancellationToken ct)
        {
            return _client.PutVoid(path, data, ct);
        }

        public Task Delete(string path)
        {
            return Delete(path, CancellationToken.None);
        }

        public Task Delete(string path, CancellationToken ct)
        {
            return _client.DeleteVoid(path, ct);
        }

        public Task<Secret<TData>> Unwrap<TData>(string unwrappingToken)
        {
            return Read<TData>(WrappedResponseLocation, unwrappingToken, CancellationToken.None);
        }

        public Task<Secret<TData>> Unwrap<TData>(string unwrappingToken, CancellationToken ct)
        {
            return Read<TData>(WrappedResponseLocation, unwrappingToken, ct);
        }
    }
}
