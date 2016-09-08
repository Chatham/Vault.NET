using System;
using Vault;

namespace VaultSample
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var httpClient = new VaultHttpClient();

            var v = new VaultClient(httpClient, VaultClientConfiguration.Default);

            foreach (var m in v.Sys.ListMounts().Result.Data)
            {
                Console.WriteLine(m.Value.Type);
            }
            Console.Read();
        }
    }
}
