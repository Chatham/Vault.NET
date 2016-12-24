using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Vault.Models.Auth.AppRole
{
    public class RoleIdRequest
    {
        [JsonProperty("role_id")]
        public string RoleId { get; set; }
    }
}
