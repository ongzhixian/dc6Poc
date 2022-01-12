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

To specify both Client and Server:

```xml
 <Protobuf Include="Protos\greet.proto" GrpcServices="Both" /> 
 ```

```xml
  <ItemGroup>
    <Protobuf Include="**/*.proto" />
  </ItemGroup>
```

```xml
    <Protobuf Include="..\MiniTools.Web\ProtoBuf\Messages\Requests\hello_request.proto">
      <Link>Protos\hello_request.proto</Link>
    </Protobuf>
    <Protobuf Include="Protos\greet.proto" GrpcServices="Client" />

```

```xml : if placing protos in a folder (ProtoBuf) outside of project, use this:
  <ItemGroup>
    <Protobuf Include="../ProtoBuf/**/*.proto" ProtoRoot=".." Link="ProtoBuf/%(RecursiveDir)%(FileName)%(Extension)" />
	  <!--
	  <Protobuf Include="../ProtoBuf/Services/greet_service.proto" ProtoRoot=".." Link="Protos\greet_service.proto" />
	  <Protobuf Include="../ProtoBuf/Messages/hello_request.proto" ProtoRoot=".." Link="Protos\hello_request.proto" />
      <Protobuf Include="../ProtoBuf/Messages/Requests/hello_request.proto" Link="ProtoBuf/%(RelativeDir)hello_request.proto" />-->
  </ItemGroup>
```

```xml: syntax explaination
	<ItemGroup>
		<Protobuf Include="../ProtoBuf/**/*.proto" ProtoRoot=".." Link="ProtoBuf/%(RecursiveDir)%(FileName)%(Extension)" GrpcServices="Client" />
	</ItemGroup>
```

Attributes:   
Include       : The path to the proto file.
ProtoRoot     : The root folder of the proto files; important for resolving imports 
Link          : For display in Visual Studio
GrpcServices  : Client, Server, Both

`proto` files do not use relative paths.
So when we state `Include="../ProtoBuf/**/*.proto" ProtoRoot=".."`,
We are saying add all files in the ProtoBuf folder to the project.
But when ".." when resolving imports we define using "ProtoBuf/Messages/Responses/hello_response.proto"

A more concrete example is:
```
	  <Protobuf Include="../ProtoBuf/Services/greet_service.proto" ProtoRoot=".." Link="Protos\greet_service.proto" />
	  <Protobuf Include="../ProtoBuf/Messages/hello_request.proto" ProtoRoot=".." Link="Protos\hello_request.proto" />
```

```proto: 
syntax = "proto3";

// Disagree with this; `package` works the same.
option csharp_namespace = "MiniTools.Services";

// What I really want is customised the awful names
// class Greeter : GreetService.GreetServiceBase

//package MiniTools.Services;
import "ProtoBuf/Messages/Requests/hello_request.proto";
import "ProtoBuf/Messages/Responses/hello_response.proto";

// The greeting service definition.

service GreetService {
    // Sends a greeting
    rpc SayHello (MiniTools.Messages.Requests.HelloRequest) returns (MiniTools.Messages.Responses.HelloResponse);

    // 
    rpc StreamingFromClient (stream MiniTools.Messages.Requests.HelloRequest) returns (MiniTools.Messages.Responses.HelloResponse);
}
```



services.AddGrpcClient<Greeter.GreeterClient>(o =>
{
    o.Address = new Uri("https://localhost:5001");
});
```

# Protobuf

Each field in the message definition has a unique number. 
Field numbers are used to identify fields when the message is serialized to Protobuf. Serializing a small number is faster than serializing the entire field name. 
Because field numbers identify a field it is important to take care when changing them. 

int64 units = 1;
sfixed32 nanos = 2;
repeated string roles = 8;
map<string, string> attributes = 9;


```proto
package CustomTypes ;

// Example: 12345.6789 -> { units = 12345, nanos = 678900000 }
message DecimalValue {

    // Whole units part of the amount
    int64 units = 1;

    // Nano units of the amount (10^-9)
    // Must be same sign as units
    sfixed32 nanos = 2;
}
```

## Services

gRPC methods are not able to bind parts of a request to different method arguments. 
gRPC methods always have one message argument for the incoming request data. 
Multiple values can still be sent to a gRPC service by making them fields on the request.

```
message ExampleRequest {
    int32 pageIndex = 1;
    int32 pageSize = 2;
    bool isDescending = 3;
}
```


// Unary
//A unary method gets the request message as a parameter, and returns the response. 
//A unary call is complete when the response is returned.
rpc UnaryCall (ExampleRequest) returns (ExampleResponse);

// Server streaming

A server-side streaming RPC where the client sends a request to the server and gets a stream to read a sequence of messages back. 
The client reads from the returned stream until there are no more messages. 

//A server streaming method gets the request message as a parameter. 
//Because multiple messages can be streamed back to the caller, responseStream.WriteAsync is used to send response messages. 
//A server streaming call is complete when the method returns.
// The client has no way to send additional messages or data once the server streaming method has started. 
// Some streaming methods are designed to run forever. 
/ For continuous streaming methods, a client can cancel the call when it's no longer needed. 
// When cancellation happens the client sends a signal to the server and the ServerCallContext.CancellationToken is raised. 
//The CancellationToken token should be used on the server with async methods so that:
//1. Any asynchronous work is canceled together with the streaming call.
//2.  The method exits quickly.
`rpc StreamingFromServer (ExampleRequest) returns (stream ExampleResponse);`

// Client streaming
A client-side streaming RPC where the client writes a sequence of messages and sends them to the server, again using a provided stream. 
Once the client has finished writing the messages, it waits for the server to read them all and return its response.

// client streaming method starts without the method receiving a message. 
// The requestStream parameter is used to read messages from the client. 
// A client streaming call is complete when a response message is returned:
`rpc StreamingFromClient (stream ExampleRequest) returns (ExampleResponse);`

// Bi-directional streaming
//A bi-directional streaming method starts without the method receiving a message. 
The requestStream parameter is used to read messages from the client. 
The method can choose to send messages with responseStream.WriteAsync. 
A bi-directional streaming call is complete when the method returns:
`rpc StreamingBothWays (stream ExampleRequest) returns (stream ExampleResponse);`

Send message via request headers
A request message is not the only way for a client to send data to a gRPC service. 
Header values are available in a service using ServerCallContext.RequestHeaders.


# c# specifics (code-gen)

The name of the output file is derived from the .proto filename by converting it to Pascal-case, treating underscores as word separators. 
So, for example, a file called player_record.proto will result in an output file called PlayerRecord.cs 
(where the file extension can be specified using --csharp_opt, as shown above). 


```cs Each generated file takes the following form, in terms of public members
namespace [...]
{
  public static partial class [... descriptor class name ...]
  {
    public static FileDescriptor Descriptor { get; }
  }

  [... Enums ...]
  [... Message classes ...]
}
```

The namespace is inferred from the proto's package, using the same conversion rules as the file name. 
For example, a proto package of `example.high_score` would result in a namespace of Example.HighScore. 
You can override the default generated namespace for a particular .proto using the csharp_namespace file option. 

# Reference

https://referbruv.com/blog/posts/implementing-stream-based-communication-with-grpc-and-aspnet-core

https://github.com/grpc/grpc/blob/master/doc/PROTOCOL-HTTP2.md

https://docs.microsoft.com/en-us/dotnet/standard/threading/overview-of-synchronization-primitives?redirectedfrom=MSDN
https://marekblog.wordpress.com/2007/02/05/net-framework-synchronization-primitives/

https://www.reddit.com/r/csharp/comments/rvqoct/interview_question_other_than_linq_and_static/

https://developers.google.com/protocol-buffers/docs/reference/csharp-generated

https://docs.microsoft.com/en-us/aspnet/core/grpc/protobuf?view=aspnetcore-6.0

https://developers.google.com/protocol-buffers/docs/reference/csharp-generated?hl=en#compiler_options

https://chromium.googlesource.com/external/github.com/grpc/grpc/+/HEAD/src/csharp/BUILD-INTEGRATION.md
https://chromium.googlesource.com/external/github.com/grpc/grpc/+/HEAD/src/csharp/BUILD-INTEGRATION.md

https://docs.microsoft.com/en-us/visualstudio/msbuild/msbuild-well-known-item-metadata?view=vs-2022
