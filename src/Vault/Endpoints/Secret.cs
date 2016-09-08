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
        public dynamic WrappedInformation { get; set; }
        public List<string> Warnings { get; set; }
    }
}

