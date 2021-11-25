# dotnet user-secrets CLI

## Storage location

The values are stored in a JSON file in the local machine's user profile folder:

 `%APPDATA%\Microsoft\UserSecrets\<user_secrets_id>\secrets.json`
 
 --OR (in MacOS/Linux)--
 
 `~/.microsoft/usersecrets/<user_secrets_id>/secrets.json`


## Enabling secret management

To enable secrets management, you need to initialize the tool first:

`dotnet user-secrets init --project .\Dn6Poc.TravalApi\`

The user-secrets tool is project specific.
The above command only defines a GUID to store the secrets and add it to `csproj` file as <UserSecretsId>.

```xml:csproj
  <PropertyGroup>
    <TargetFramework>net6.0</TargetFramework>
    <Nullable>enable</Nullable>
    <ImplicitUsings>enable</ImplicitUsings>
    <UserSecretsId>94235bd0-be44-4377-aa54-03b663764d6b</UserSecretsId>
  </PropertyGroup>
```

At this stage, the `secrets.json` is not created yet.
In fact, if you only started to use `user-secrets`, you will not find the `UserSecrets` folder mentioned above.
The tool will create it after you start adding secrets.


## Add a secret

`dotnet user-secrets set <some-key> <some-value> --project .\Dn6Poc.TravalApi\`

`dotnet user-secrets set "Movies:ServiceApiKey" "12345" --project .\Dn6Poc.TravalApi\`

`dotnet user-secrets set "Movies__ServiceApiKey2" "x2345" --project .\Dn6Poc.TravalApi\`

`dotnet user-secrets set ConnectionStrings:SafeTravel "Data Source=.;Database=SafeTravel;Trusted_Connection=True;" --project .\Dn6Poc.TravalApi\`

"Server=(localdb)\\MSSQLLocalDB;Database=SafeTravel;Trusted_Connection=True;MultipleActiveResultSets=true"

`dotnet user-secrets set ConnectionStrings:SafeTravel "Server=(localdb)\MSSQLLocalDB;Database=SafeTravel;Trusted_Connection=True;MultipleActiveResultSets=true" --project .\Dn6Poc.TravalApi\`


### Adding multiple secrets

`type .\input.json | dotnet user-secrets set --project .\Dn6Poc.TravalApi\`

`input.json` file may look like:

```json
{
	"AppName" : "MyApp",
	"Version" : "1.0.0"
}
```

### Note
The JSON in the `secrets.json` file is flattened whenever modifications are made (either via `dotnet user-secrets remove` or `dotnet user-secrets set`).
For example, if we have the following JSON in the `secrets.json` file:

```json:before remove of Movies:ConnectionString
{
  "Movies": {
    "ConnectionString": "Server=(localdb)\\mssqllocaldb;Database=Movie-1;Trusted_Connection=True;MultipleActiveResultSets=true",
    "ServiceApiKey": "12345"
  }
}
```

And we remove the `ConnectionString` secret, the JSON in the `secrets.json` file will be flattened to:

```json:after remove of Movies:ConnectionString
{
  "Movies:ServiceApiKey": "12345"
}
```


## Listing secrets

`dotnet user-secrets --project .\Dn6Poc.TravalApi\ list`

## Remove secrets

Remove a secret:

`dotnet user-secrets --project .\Dn6Poc.TravalApi\ remove Movies:ConnectionString`

To remove all secrets:

`dotnet user-secrets clear --project .\Dn6Poc.TravalApi\`


## Reference secrets in code

The user secrets configuration provider registers the appropriate configuration source with the .NET Configuration API.

```cs
var builder = WebApplication.CreateBuilder(args);
var movieApiKey = builder.Configuration["Movies:ServiceApiKey"];
```

WebApplication.CreateBuilder initializes a new instance of the WebApplicationBuilder class with preconfigured defaults. 
The initialized WebApplicationBuilder (builder) provides default configuration and calls AddUserSecrets when the EnvironmentName is Development:


```cs
public class IndexModel : PageModel
{
    private readonly IConfiguration _config;
    public IndexModel(IConfiguration config)
    {
        _config = config;
    }

    public void OnGet()
    {
        var moviesApiKey = _config["Movies:ServiceApiKey"];
    }
}
```

Aggregating secrets to an POCO: 

Assuming a `secrets.json` file with the following content:

```json:secrets.json
{
  "Movies:ConnectionString": "Server=(localdb)\\mssqllocaldb;Database=Movie-1;Trusted_Connection=True;MultipleActiveResultSets=true",
  "Movies:ServiceApiKey": "12345"
}
```

We can define an object that has the same structure as the secrets:

```cs
public class MovieSettings
{
    public string ConnectionString { get; set; }
    public string ServiceApiKey { get; set; }
}
```

We can then read the contents and access the secrets like so:

```cs
var moviesConfig = Configuration.GetSection("Movies").Get<MovieSettings>();
_moviesApiKey = moviesConfig.ServiceApiKey;
```


## Referencing a secret when using `dotnet ef scaffold`

Assuming a `secrets.json` file:
```cmd
{
  "ConnectionStrings": {
	"SafeTravel": "Data Source=.;Database=SafeTravel;Trusted_Connection=True;",
    "BloggingDatabase": "Server=(localdb)\\mssqllocaldb;Database=EFGetStarted.ConsoleApp.NewDb;Trusted_Connection=True;"
  },
  "Version": "1.0.0",
  "AppName": "MyApp"
}
```

We can use use the following command to scaffold:

`dotnet ef dbcontext scaffold Name=ConnectionStrings:SafeTravel Microsoft.EntityFrameworkCore.SqlServer -o Models --project .\Dn6Poc.TravalApi\`


## Reference

https://docs.microsoft.com/en-us/aspnet/core/security/app-secrets?view=aspnetcore-6.0&tabs=windows#secret-manager
