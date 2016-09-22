using System;
using System.Net;

namespace Vault
{
    public class Exceptions
    {
        public class VaultRequestException : Exception
        {
            public HttpStatusCode StatusCode { get; set; }
            public VaultRequestException() { }
            public VaultRequestException(string message, HttpStatusCode statusCode) : base(message) { StatusCode = statusCode; }
            public VaultRequestException(string message, HttpStatusCode statusCode, Exception inner) : base(message, inner) { StatusCode = statusCode; }
        }
    }
}
