using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
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

        public VaultClient TestClient()
        {
            var vaultArgs = string.Join(" ", new List<string>
            {
                "server",
                "-dev",
                $"-dev-root-token-id={RootToken}",
                $"-dev-listen-address={ListenAddress}"
            });

            var vaultBin = Environment.GetEnvironmentVariable("VAULT_BIN") ?? "vault";
            var startInfo = new ProcessStartInfo(vaultBin, vaultArgs);
            startInfo.Environment["HOME"] = Directory.GetCurrentDirectory();
            _process = new Process
            {
                StartInfo = startInfo
            };
            _process.StartInfo.RedirectStandardOutput = true;
            _process.StartInfo.RedirectStandardError = true;

            if (!_process.Start())
            {
                throw new Exception($"Process did not start successfully: {_process.StandardError}");
            }

            Thread.Sleep(500);

            if (_process.HasExited)
            {
                throw new Exception($"Process could not be started: {_process.StandardError}");
            }

            return new VaultClient(new UriBuilder(ListenAddress).Uri, RootToken);
        }

        private static int GetRandomUnusedPort()
        {
            var listener = new TcpListener(IPAddress.Any, 0);
            listener.Start();
            var port = ((IPEndPoint)listener.LocalEndpoint).Port;
            listener.Stop();
            return port;
        }

        private bool _disposed;

        protected virtual void Dispose(bool disposing)
        {
            if (_disposed)
            {
                return;
            }

            if (disposing)
            {
                _process.Kill();
                _process.Dispose();
            }

            _disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
        }
    }
}
