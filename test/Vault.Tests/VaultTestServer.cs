using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net;
using System.Net.Sockets;
using System.Threading;

namespace Vault.Tests
{
    public class VaultTestServer : IDisposable
    {
        private Process _process;
        public string RootToken = Guid.NewGuid().ToString();
        public string ListenAddress = $"127.0.0.1:{ GetRandomUnusedPort() }";

        public VaultClient StartServer()
        {
            var vaultArgs = string.Join(" ", new List<string>
            {
                "server",
                "-dev",
                $"-dev-root-token-id={RootToken}",
                $"-dev-listen-address={ListenAddress}"
            });

            _process = new Process
            {
                StartInfo = new ProcessStartInfo("vault", vaultArgs),
            };
            _process.StartInfo.RedirectStandardOutput = true;
            _process.StartInfo.RedirectStandardError = true;

            if (!_process.Start())
            {
                throw new Exception($"Process did not start successfully: {_process.StandardError}");
            }

            Thread.Sleep(1000);

            if (_process.HasExited)
            {
                throw new Exception($"Process could not be started: {_process.StandardError}");
            }

            var config = new VaultClientConfiguration
            {
                Address = new UriBuilder(ListenAddress).Uri,
                Token = RootToken
            };

            return new VaultClient(config);
        }

        private static int GetRandomUnusedPort()
        {
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
                    _process.Kill();
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
