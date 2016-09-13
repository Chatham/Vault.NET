using System.Collections.Generic;

namespace Vault.Endpoints
{
    public class Secret<TData>
    {
        public string RequestId { get; set; }
        public string LeaseId { get; set; }
        public bool Renewable { get; set; }
        public int LeaseDuration { get; set; }
        public TData Data { get; set; }
        public List<string> Warnings { get; set; }
        public SecretWrapInfo WrapInfo { get; set; }
        public SecretAuth Auth { get; set; }
    }

    public class SecretWrapInfo
    {
        public string Token { get; set; }
        public int Ttl { get; set; }
        public string CreationTime { get; set; }
        public string WrappedAccessor { get; set; }
    }

    public class SecretAuth
    {
        public string ClientToken { get; set; }
        public string Accessor { get; set; }
        public List<string> Policies { get; set; }
        public Dictionary<string, string> Metadata { get; set; }
        public int LeaseDuration { get; set; }
        public bool Renewable { get; set; }  
    }
}

