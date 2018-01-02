using System;
using System.Threading;
using System.Threading.Tasks;
using Vault.Models;

namespace Vault.Endpoints
{
    public class Endpoint : IEndpoint
    {
        private readonly VaultClient _client;
        private readonly string _uriBasePath;

        private const string UriRootPath = "/v1";

        public Endpoint(VaultClient client, string basePath = null)
        {
            _client = client;

            var path = basePath != null ? $"/{basePath}" : "";
            _uriBasePath = $"{UriRootPath}{path}";
        }

        public Task<VaultResponse<TData>> Read<TData>(string path, CancellationToken ct = default(CancellationToken))
        {
            return _client.Get<VaultResponse<TData>>($"{_uriBasePath}/{path}", TimeSpan.Zero, ct);
        }

        public Task<WrappedVaultResponse> Read(string path, TimeSpan wrapTtl = default(TimeSpan), CancellationToken ct = default(CancellationToken))
        {
            return _client.Get<WrappedVaultResponse>($"{_uriBasePath}/{path}", wrapTtl, ct);
        }

        public Task<VaultResponse<ListResponse>> List(string path, CancellationToken ct = default(CancellationToken))
        {
            return _client.List<VaultResponse<ListResponse>>($"{_uriBasePath}/{path}", TimeSpan.Zero, ct);
        }

        public Task<WrappedVaultResponse> List(string path, TimeSpan wrapTtl, CancellationToken ct = default(CancellationToken))
        {
            return _client.List<WrappedVaultResponse>($"{_uriBasePath}/{path}", TimeSpan.Zero, ct);
        }

        public Task Write<TParameters>(string path, TParameters data, CancellationToken ct = default(CancellationToken))
        {
            return _client.PutVoid($"{_uriBasePath}/{path}", data, ct);
        }

        public Task<VaultResponse<TData>> Write<TData>(string path, CancellationToken ct = default(CancellationToken))
        {
            return _client.Put<VaultResponse<TData>>($"{_uriBasePath}/{path}", TimeSpan.Zero, ct);
        }

        public Task<WrappedVaultResponse> Write(string path, TimeSpan wrapTtl, CancellationToken ct = default(CancellationToken))
        {
            return _client.Put<WrappedVaultResponse>($"{_uriBasePath}/{path}", wrapTtl, ct);
        }

        public Task<VaultResponse<TData>> Write<TParameters, TData>(string path, TParameters data, CancellationToken ct = default(CancellationToken))
        {
            return _client.Put<TParameters, VaultResponse<TData>>($"{_uriBasePath}/{path}", data, TimeSpan.Zero, ct);
        }

        public Task<WrappedVaultResponse> Write<TParameters>(string path, TParameters data, TimeSpan wrapTtl, CancellationToken ct = default(CancellationToken))
        {
            return _client.Put<TParameters, WrappedVaultResponse>($"{_uriBasePath}/{path}", data, wrapTtl, ct);
        }

        public Task Delete(string path, CancellationToken ct = default(CancellationToken))
        {
            return _client.DeleteVoid($"{_uriBasePath}/{path}", ct);
        }

        // Including with IEndpoint interface even though it just proxies to the ISysEndpoint
        public Task<VaultResponse<TData>> Unwrap<TData>(string wrappingToken, CancellationToken ct = default(CancellationToken))
        {
            return _client.Sys.Unwrap<VaultResponse<TData>>(wrappingToken, ct);
        }
    }
}
