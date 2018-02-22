# Vault.NET [![Build status](https://ci.appveyor.com/api/projects/status/784hg5j70vcnumeb/branch/master?svg=true)](https://ci.appveyor.com/project/chatham/vault-net/branch/master)

* Vault API: v0.9.1
* .NET Standard 1.3 (.NET: >= 4.6, .NET Core: >= 1.0.0)
* .NET 4.5
* Nuget: Vault [![NuGet](https://img.shields.io/nuget/v/Vault.svg)](https://www.nuget.org/packages/Vault/)

Vault.NET is an .NET API client for the interacting with [Vault](https://www.vaultproject.io/).  This is a port of the go api client and provides generic methods for interacting with the paths in Vault.  

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

Many request/response objects are provided in this package to support different backends.  This is in no way an exhaustive list of all the objects.  Since the models are the things that are going to most likely change between versions of vault, it may make sense to make your own to service your needs.  These may get split into a seperate Nuget package in the future.

## Testing

Since most of the operation of this library are just building requests and passing them to the vault API and the vault team provides an easy to use local development server, each test runs against its own vault server.  This means that tests require the vault binary available to spin up the vault server instance.  The test suite will first look for the environment variable `VAULT_BIN` and if not found will fall back to use the `vault` binary in the `$PATH`.

Downloads for vault can be found [here](https://www.vaultproject.io/downloads.html).

## Versioning

This library will follow the version of vault that it was developed against.  Since most core operations of vault maintain backwards compatibility, this library can be used against many older and newer versions of vault.  If features are added or bugs are fixed, a new point release will be created (ex. 0.6.4 -> 0.6.4.1).  If there is some functionality that breaks on a newer version of vault, please submit a pull request.
