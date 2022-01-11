# Grpc


# Packages
ASP.NET
Grpc.AspNetCore

ConsoleApp
Grpc.Net.Client
Google.Protobuf
Grpc.Tools

# Server

Add proto file to csproj
Add a GreeterService.cs file
Program.cs needs:
1.  builder.Services.AddGrpc()
2.  app.MapGrpcService<GreeterService>();


service Greeter
    <Greeter>.<Greeter>Base

```cs 
//                           <service-name:in proto file>
public class GreeterService : Greeter.GreeterBase
{
    private readonly ILogger<GreeterService> _logger;
    public GreeterService(ILogger<GreeterService> logger)
    {
        _logger = logger;
    }

    public override Task<HelloReply> SayHello(HelloRequest request, ServerCallContext context)
    {
        return Task.FromResult(new HelloReply
        {
            Message = "Hello " + request.Name
        });
    }
}
```

```xml csproj (note the `GrpcServices` attribute)
  <ItemGroup>
    <Protobuf Include="Protos\greet.proto" GrpcServices="Server" />
  </ItemGroup>
```


## Things to change for client

```xml csproj
<ItemGroup>
  <Protobuf Include="Protos\greet.proto" GrpcServices="Client" />
</ItemGroup>
```

```
option csharp_namespace = "GrpcGreeterClient";
```


# Reference

https://referbruv.com/blog/posts/implementing-stream-based-communication-with-grpc-and-aspnet-core
