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


## Http Logging

HTTP logging here refers to the logging of request/response to the serving HTTP application.

```cs
public void ConfigureServices(IServiceCollection services)
{
    services.AddHttpLogging(logging =>
    {
        // Customize HTTP logging here.
        logging.LoggingFields = HttpLoggingFields.All;
        logging.RequestHeaders.Add("My-Request-Header");
        logging.ResponseHeaders.Add("My-Response-Header");
        logging.MediaTypeOptions.AddText("application/javascript");
        logging.RequestBodyLogLimit = 4096;
        logging.ResponseBodyLogLimit = 4096;
    });
}

...

app.UseHttpLogging();
```

Note also that by default, the log level for `Microsoft.AspNetCore` namespace in `appsettings.Development.json` is `Warning`.
You need to set it to at least `Information` to see the more common messages HTTP request/response.

```json
{
  "Logging": {
    "LogLevel": {
      "Default": "Information",
      "Microsoft.AspNetCore": "Warning",
      "Microsoft.Hosting": "Information",
      "System.Net.Http.HttpClient": "Trace"
    }
  }
}

```




There are 2 namespaces that the log messages commonly logged in: 

Microsoft.AspNetCore.Hosting.Diagnostics[1]
Microsoft.AspNetCore.HttpLogging.HttpLoggingMiddleware[1]

```log:example
info: Microsoft.AspNetCore.Hosting.Diagnostics[1]
      Request starting HTTP/2 GET https://localhost:7241/favicon.ico - -
info: Microsoft.AspNetCore.HttpLogging.HttpLoggingMiddleware[1]
      Request:
      Protocol: HTTP/2
      Method: GET
      Scheme: https
      ...
```




## HttpClient logging

Logs messages inccuring from using HttpClient.

The default logging messages (see below) are quite basic.
Most of the time, we want a more detailed logging.
That requires to hooking it up to a delegating handler.



The log category used for each client includes the name of the client. 
A client named MyNamedClient, for example, logs messages with a category of "System.Net.Http.HttpClient.MyNamedClient.LogicalHandler".

Messages suffixed with `LogicalHandler` occur outside the request handler pipeline. 
On the request, messages are logged before any other handlers in the pipeline have processed it.
On the response, messages are logged after any other pipeline handlers have received the response.

Logging also occurs inside the request handler pipeline. 
In the MyNamedClient example, those messages are logged with the log category "System.Net.Http.HttpClient.MyNamedClient.ClientHandler". 
For the request, this occurs after all other handlers have run and immediately before the request is sent. 
On the response, this logging includes the state of the response before it passes back through the handler pipeline.

Enabling logging outside and inside the pipeline enables inspection of the changes made by the other pipeline handlers. 
This may include changes to request headers or to the response status code.


There are 2 namespaces that the log messages commonly logged in: 

System.Net.Http.HttpClient.Default.LogicalHandler[100]
System.Net.Http.HttpClient.Default.LogicalHandler[102]


```log:Example
info: AuthenticationController[0]
      Yep in AUthenticate
info: System.Net.Http.HttpClient.Default.LogicalHandler[100]
      Start processing HTTP request POST https://localhost:7241/api/Authentication/login
trce: System.Net.Http.HttpClient.Default.LogicalHandler[102]
      Request Headers:
      Content-Type: application/json; charset=utf-8

info: System.Net.Http.HttpClient.Default.ClientHandler[100]
      Sending HTTP request POST https://localhost:7241/api/Authentication/login
trce: System.Net.Http.HttpClient.Default.ClientHandler[102]
      Request Headers:
      Content-Type: application/json; charset=utf-8



info: AuthenticationController[0]
      Yep in login API
info: System.Net.Http.HttpClient.Default.ClientHandler[101]
      Received HTTP response headers after 296.0313ms - 200
trce: System.Net.Http.HttpClient.Default.ClientHandler[103]
      Response Headers:
      Date: Thu, 16 Dec 2021 06:58:32 GMT
      Server: Kestrel
      Transfer-Encoding: chunked
      Content-Type: application/json; charset=utf-8

info: System.Net.Http.HttpClient.Default.LogicalHandler[101]
      End processing HTTP request after 315.1823ms - 200
trce: System.Net.Http.HttpClient.Default.LogicalHandler[103]
      Response Headers:
      Date: Thu, 16 Dec 2021 06:58:32 GMT
      Server: Kestrel
      Transfer-Encoding: chunked
      Content-Type: application/json; charset=utf-8
```



To customize, see:
https://www.stevejgordon.co.uk/httpclientfactory-asp-net-core-logging
https://docs.microsoft.com/en-us/dotnet/core/compatibility/aspnet-core/5.0/http-httpclient-instances-log-integer-status-codes

# Reference

HTTP Logging in ASP.NET Core
https://docs.microsoft.com/en-us/aspnet/core/fundamentals/http-logging/?view=aspnetcore-6.0
