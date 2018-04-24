# Vault.NET Local Certificate

I have defined a new property "CertPah" that is populated before the definition of the httpcliet.

In case it is empty, the behavior remains unchanged.

If it is different from empty, then the certificate is taken locally and contributes to Vault API calls.

```csharp   
public static async Task<Dictionary<string, string>> VaultAsync(string secretPath)
{
    VaultOptions.Default.CertPath = new DirectoryInfo(
        Path.GetFullPath(Path.Combine(Environment.CurrentDirectory, @"..\..\" + "AppData\\cert.crt"))
    ).ToString();

    var vaultClient = new VaultClient();
    vaultClient.Address = new System.Uri("https://vault.personal.domain.com:8200");
    vaultClient.Token = "token";

    var secret = await vaultClient.Secret.Read<Dictionary<string, string>>(secretPath);

    return secret.Data;
}    
```    
