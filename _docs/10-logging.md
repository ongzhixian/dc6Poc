# Logging

Adding Serilog logging

## Nuget packages

```
dotnet add .\Dn6Poc.TravalApi\ package Serilog
dotnet add .\Dn6Poc.TravalApi\ package Serilog.Sinks.Console
dotnet add .\Dn6Poc.TravalApi\ package Serilog.Sinks.File
dotnet add .\Dn6Poc.TravalApi\ package Serilog.Extensions.Hosting
dotnet add .\Dn6Poc.TravalApi\ package Serilog.Settings.Configuration
dotnet add .\Dn6Poc.TravalApi\ package Serilog.Formatting.Compact
```

```xml:reference only (From Rabbit project)
<PackageReference Include="Microsoft.Extensions.Configuration" Version="3.1.18" />
<PackageReference Include="Microsoft.Extensions.Configuration.CommandLine" Version="3.1.18" />
<PackageReference Include="Microsoft.Extensions.Configuration.EnvironmentVariables" Version="3.1.18" />
<PackageReference Include="Microsoft.Extensions.Configuration.Json" Version="3.1.18" />
<PackageReference Include="Microsoft.Extensions.DependencyInjection" Version="3.1.18" />
<PackageReference Include="Microsoft.Extensions.Hosting.WindowsServices" Version="3.1.18" />
<PackageReference Include="Microsoft.Extensions.Http" Version="3.1.18" />
<PackageReference Include="Microsoft.Extensions.Logging" Version="3.1.18" />
<PackageReference Include="Microsoft.Extensions.Logging.Console" Version="3.1.18" />
<PackageReference Include="Microsoft.Extensions.Logging.Debug" Version="3.1.18" />

<PackageReference Include="Serilog" Version="2.10.0" />
<PackageReference Include="Serilog.Sinks.Console" Version="4.0.0" />
<PackageReference Include="Serilog.Sinks.File" Version="5.0.0" />
<PackageReference Include="Serilog.Extensions.Hosting" Version="4.1.2" />
<PackageReference Include="Serilog.Formatting.Compact" Version="1.1.0" />

<PackageReference Include="Serilog.Extensions.Logging" Version="3.0.1" />
<PackageReference Include="Serilog.Settings.Configuration" Version="3.2.0" />
<PackageReference Include="System.Text.Json" Version="5.0.2" />
```