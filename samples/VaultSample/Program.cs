using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Vault;

namespace VaultSample
{
    public class Program
    {

        public static void Main(string[] args)
        {
            Task.Run(async () =>
                {
                    var v = new VaultClient();

                    //await v.Secret.Write("secret/blah", new Dictionary<string, object> { { "abc", "123" } });
                    //var z = await v.Secret.Read<Dictionary<string, object>>("secret/blah");
                    //foreach (var entry in z.Data)
                    //{
                    //    Console.WriteLine($"{entry.Key}:{entry.Value}");
                    //}

                    //var mounts = await v.Sys.ListMounts();
                    //foreach (var m in mounts.Data)
                    //{
                    //    Console.WriteLine(m.Value.Type);
                    //}

                    await v.Sys.PutPolicy("abc", "path \"*\" {policy = \"read\"}");

                    var policies = await v.Sys.ListPolicies();
                    foreach (var p in policies)
                    {
                        Console.WriteLine(p);
                    }

                    Console.Read();
                }
            ).Wait();
        }
    }
}
