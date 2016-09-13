using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Vault.Endpoints.Sys
{
    public class Audit
    {
        public string Path { get; set; }
        public string Type { get; set; }
        public string Description { get; set; }
        public Dictionary<string, string> Options { get; set; }
    }

    public partial class SysEndpoint
    {
        public Task<string> AuditHash(string path, string input)
        {
            return AuditHash(path, input, CancellationToken.None);
        }

        public async Task<string> AuditHash(string path, string input, CancellationToken ct)
        {
            var request = new AuditHashRequest
            {
                Input = input
            };
            var hashData = await _client.Put<AuditHashRequest, AuditHashResponse>($"{UriPathBase}/audit-hash/{path}",
                request, ct);
            return hashData.Hash;
        }

        public Task<Dictionary<string, Audit>> ListAudit()
        {
            return ListAudit(CancellationToken.None);
        }

        public Task<Dictionary<string, Audit>> ListAudit(CancellationToken ct)
        {
            return _client.Get<Dictionary<string, Audit>>($"${UriPathBase}/audit", ct);
        }

        public Task EnableAudit(string path, string auditType, string description, Dictionary<string, string> options)
        {
            return EnableAudit(path, auditType, description, options, CancellationToken.None);
        }

        public Task EnableAudit(string path, string auditType, string description, Dictionary<string, string> options,
            CancellationToken ct)
        {
            var request = new EnableAuditRequest
            {
                AuditType = auditType,
                Description = description,
                Options = options
            };
            return _client.PutVoid($"{UriPathBase}/audit/{path}", request, ct);
        }

        public Task DisableAudit(string path)
        {
            return DisableAudit(path, CancellationToken.None);
        }

        public Task DisableAudit(string path, CancellationToken ct)
        {
            return _client.DeleteVoid($"{UriPathBase}/audit/{path}", ct);
        }

        internal class AuditHashRequest
        {
            public string Input { get; set; }
        }

        private class AuditHashResponse
        {
            public string Hash { get; set; }
        }

        private class EnableAuditRequest
        {
            public string AuditType { get; set; }
            public string Description { get; set; }
            public Dictionary<string, string> Options { get; set; }
        }
    }
}
