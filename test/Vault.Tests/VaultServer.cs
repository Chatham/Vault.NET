using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net;
using System.Net.Sockets;

namespace Vault.Tests
{
    public class VaultServer : IDisposable
    {
        private Process _process;
        public string RootToken = Guid.NewGuid().ToString();
        public string ListenAddress = $"127.0.0.1:{GetRandomUnusedPort()}";

        public void StartTestServer()
        {
            var vaultArgs = string.Join(" ", new List<string>
            {
                "server",
                "-dev",
                $"-dev-root-token_id={RootToken}",
                $"-dev-listen-address={ListenAddress}"
            });

            var processInfo = new ProcessStartInfo("vault", vaultArgs);
            _process = Process.Start(processInfo);
        }

        private static int GetRandomUnusedPort()
        {
            return 5000;
            var listener = new TcpListener(IPAddress.Any, 0);
            listener.Start();
            var port = ((IPEndPoint)listener.LocalEndpoint).Port;
            listener.Stop();
            return port;
        }

        private bool _disposedValue;

        protected virtual void Dispose(bool disposing)
        {
            if (!_disposedValue)
            {
                if (disposing)
                {
                    _process.Dispose();
                }

                _disposedValue = true;
            }
        }

        public void Dispose()
        {
            Dispose(true);
        }
    }
}
