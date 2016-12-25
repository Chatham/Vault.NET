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

```
var secret = await vaultClient.Secret.Read<Dictionary<string, string>>("secret/foo");
Console.WriteLine(secret.Data["zip"]);

// zap
```

### PKI
```
var certRequest = new IssueRequest
{
    CommonName = "Test Cert"
};
var cert = await client.Secret.Write<IssueRequest, IssueResponse>($"{mountPoint}/issue/{roleName}", certRequest);
Console.WriteLine(secret.Data.Certificate);

// -----BEGIN CERTIFICATE-----
// MII...
```

## Models

Many request/response objects are provided in this package to support different backends.  This is in no way an exhaustive list of all the objects.  
