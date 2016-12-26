# Vault.NET

* Vault API: v0.6.2
* .NET: >= 4.6 - .NET Core: >= 1.0.0

Vault.NET is an .NET API client for the interacting with [Vault](https://www.vaultproject.io/).  This is a port of the go api client and provides generic methods for interacting with the virtual paths in Vault.  

## Example

```csharp
using Vault;

var vaultClient = new VaultClient();
vaultClient.Token = "XXXXXX";
```

### Generic Secret

```csharp
var data = new Dictionary<string, string>
{
    {"zip", "zap"}
};
await vaultClient.Secret.Write("secret/foo", data);

var secret = await vaultClient.Secret.Read<Dictionary<string, string>>("secret/foo");
Console.WriteLine(secret.Data["zip"]);

// zap
```

### PKI

```csharp
using Vault.Models.Secret.Pki;

var testRole = new RolesRequest
{
    AllowAnyDomain = true,
    EnforceHostnames = false,
    MaxTtl = "1h"
};
await vaultClient.Secret.Write("pki/roles/test", testRole);

var certRequest = new IssueRequest
{
    CommonName = "Test Cert"
};
var cert = await vaultClient.Secret.Write<IssueRequest, IssueResponse>("pki/issue/test", certRequest);
Console.WriteLine(secret.Data.Certificate);

// -----BEGIN CERTIFICATE-----
// MII...
```

### Username/Password Authentication

```csharp
using Vault.Models.Auth.UserPass;

await vaultClient.Sys.EnableAuth("userpass", "userpass", "Userpass Mount");

var usersRequest = new UsersRequest
{
    Password = "password",
    Policies = new List<string> { "default" },
    Ttl = "1h",
    MaxTtl = "2h"
};
await vaultClient.Auth.Write("userpass/users/username", usersRequest);

var loginRequest = new LoginRequest
{
    Password = "password"
};
var loginResponse = await vaultClient.Auth.Write<LoginRequest, NoData>("userpass/login/username", loginRequest);

// Set client token to authenticated token
vaultClient.Token = loginResponse.Auth.ClientToken;

// Proceed with authenticated requests
```

## Models

Many request/response objects are provided in this package to support different backends.  This is in no way an exhaustive list of all the objects.