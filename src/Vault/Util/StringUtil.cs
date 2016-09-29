using System.Collections.Generic;

namespace Vault.Util
{
    public class StringUtil
    {
        public static string ListToCsvString(List<string> input)
        {
            return input == null ? null : string.Join(",", input);
        }
    }
}
