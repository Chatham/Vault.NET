using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Vault;
using Vault.Endpoints.Secret;

namespace VaultSample
{
    public class Program
    {

        public static void Main(string[] args)
        {
            Task.Run(async () =>
                {
                    var v = new VaultClient();

                    await v.Secret.Generic.Write("secret/blah", new Dictionary<string, object> { { "abc", "123" } });
                    var z = await v.Secret.Generic.Read("secret/blah");
                    foreach (var entry in z.Data)
                    {
                        Console.WriteLine($"{entry.Key}:{entry.Value}");
                    }

                    //foreach (var m in v.Sys.ListMounts().Result.Data)
                    //{
                    //    Console.WriteLine(m.Value.Type);
                    //}
                    Console.Read();
                }
            ).Wait();
        }
    }
}
