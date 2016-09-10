using System.Threading;
using System.Threading.Tasks;

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

        public Task<Secret<TData>> List<TData>(string path)
        {
            return List<TData>(path, CancellationToken.None);
        }

        public Task<Secret<TData>> List<TData>(string path, CancellationToken ct)
        {
            return _client.List<Secret<TData>>($"{UriBasePath}/{path}", ct);
        }

        public Task Write<TParameters>(string path, TParameters parameters)
        {
            return Write(path, parameters, CancellationToken.None);
        }

        public Task Write<TParameters>(string path, TParameters parameters, CancellationToken ct)
        {
            return _client.PutVoid($"{UriBasePath}/{path}", parameters, ct);
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

        public Task<Secret<TData>> Unwrap<TData>(string unwrappingToken, CancellationToken ct)
        {
            return Read<TData>(WrappedResponseLocation, unwrappingToken, ct);
        }
    }
}
