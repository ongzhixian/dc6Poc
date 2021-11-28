# Country API


```cmd
dotnet new web -n Dn6Poc.CountryApi
dotnet sln .\dn6Poc.sln add .\Dn6Poc.CountryApi\
dotnet user-secrets init --project .\Dn6Poc.CountryApi\

dotnet add .\Dn6Poc.CountryApi\ package Serilog
dotnet add .\Dn6Poc.CountryApi\ package Serilog.Sinks.Console
dotnet add .\Dn6Poc.CountryApi\ package Serilog.Sinks.File
dotnet add .\Dn6Poc.CountryApi\ package Serilog.Extensions.Hosting
dotnet add .\Dn6Poc.CountryApi\ package Serilog.Settings.Configuration
dotnet add .\Dn6Poc.CountryApi\ package Serilog.Formatting.Compact
dotnet add .\Dn6Poc.CountryApi\ package Swashbuckle.AspNetCore
```

dotnet build .\Dn6Poc.CountryApi\
dotnet run --project .\Dn6Poc.CountryApi\
