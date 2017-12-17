using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Vault.Endpoints.Sys
{
    public class Audit
    {
        [JsonProperty("path")]
        public string Path { get; set; }

        [JsonProperty("type")]
        public string Type { get; set; }

        [JsonProperty("description")]
        public string Description { get; set; }

        [JsonProperty("options")]
        public Dictionary<string, string> Options { get; set; }
    }

    public partial class SysEndpoint
    {
        public async Task<string> AuditHash(string path, string input, CancellationToken ct = default(CancellationToken))
        {
            var request = new AuditHashRequest
            {
                Input = input
            };
            var hashData = await _client.Put<AuditHashRequest, AuditHashResponse>($"{UriPathBase}/audit-hash/{path}",
                request, ct);
            return hashData.Hash;
        }

        public Task<Dictionary<string, Audit>> ListAudit(CancellationToken ct = default(CancellationToken))
        {
            return _client.Get<Dictionary<string, Audit>>($"${UriPathBase}/audit", ct);
        }

        public Task EnableAudit(string path, string auditType, string description, Dictionary<string, string> options,
            CancellationToken ct = default(CancellationToken))
        {
            var request = new EnableAuditRequest
            {
                AuditType = auditType,
                Description = description,
                Options = options
            };
            return _client.PutVoid($"{UriPathBase}/audit/{path}", request, ct);
        }

        public Task DisableAudit(string path, CancellationToken ct = default(CancellationToken))
        {
            return _client.DeleteVoid($"{UriPathBase}/audit/{path}", ct);
        }

        internal class AuditHashRequest
        {
            [JsonProperty("input")]
            public string Input { get; set; }
        }

        internal class AuditHashResponse
        {
            [JsonProperty("hash")]
            public string Hash { get; set; }
        }

        internal class EnableAuditRequest
        {
            [JsonProperty("audit_type")]
            public string AuditType { get; set; }

            [JsonProperty("description")]
            public string Description { get; set; }

            [JsonProperty("options")]
            public Dictionary<string, string> Options { get; set; }
        }
    }
}
