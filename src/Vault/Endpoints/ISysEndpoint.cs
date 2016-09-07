using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Vault.Endpoints
{
    public interface ISysEndpoint
    {
        Task<bool> InitStatus();
        Task<bool> InitStatus(CancellationToken ct);
        Task<InitResponse> Init(InitRequest request);
        Task<InitResponse> Init(InitRequest request, CancellationToken ct);

        Task<GenerateRootStatusResponse> GenerateRootStatus();
        Task<GenerateRootStatusResponse> GenerateRootStatus(CancellationToken ct);
        Task<GenerateRootStatusResponse> GenerateRootInit(GenerateRootInitRequest request);
        Task<GenerateRootStatusResponse> GenerateRootInit(GenerateRootInitRequest request, CancellationToken ct);
    }

    public class InitStatusResponse
    {
        public bool Initialized { get; set; }
    }

    public class InitRequest
    {
        public int SecretShares { get; set; }
        public int SecretThreshold { get; set; }
        public List<string> PgpKeys { get; set; }
    }

    public class InitResponse
    {
        public List<string> Keys { get; set; }
        public List<string> KeysBase64 { get; set; }
        public string RootToken { get; set; }
    }

    public class GenerateRootStatusResponse
    {
        public string Nonce { get; set; }
        public bool Started { get; set; }
        public int Progress { get; set; }
        public int Required { get; set; }
        public bool Complete { get; set; }
        public string EncodedRootToken { get; set; }
    }

    public class GenerateRootInitRequest
    {
        public string Otp { get; set; }
        public string PgpKey { get; set; }
    }
}
