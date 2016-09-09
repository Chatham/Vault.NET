using System;
using Vault;

namespace VaultSample
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var v = new VaultClient();
            foreach (var m in v.Sys.ListMounts().Result.Data)
            {
                Console.WriteLine(m.Value.Type);
            }
            Console.Read();
        }
    }
}
