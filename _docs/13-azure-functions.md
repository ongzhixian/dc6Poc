# Azure functions


## Install Azure Functions Core Tools
https://docs.microsoft.com/en-gb/azure/azure-functions/functions-run-local?tabs=v4%2Cwindows%2Ccsharp%2Cportal%2Cbash%2Ckeda#install-the-azure-functions-core-tools

## Execution Models (2)

Execution model 	Description
In-process 	        Your function code runs in the same process as the Functions host process. 
                    Supports both .NET Core 3.1 and .NET 6.0. Not supported in .NET 5?!
                    To learn more, see Develop C# class library functions using Azure Functions.
Isolated process 	Your function code runs in a separate .NET worker process. Supports both .NET 5.0 and .NET 6.0. 
                    To learn more, see Develop isolated process functions in C#.


Running out-of-process (isolated process) lets you decouple your function code from the Azure Functions runtime.

Why .NET isolated process?

Previously Azure Functions has only supported a tightly integrated mode for .NET functions, which run as a class library in the same process as the host. (This is the in-process model which is the default execution model.)

This mode provides deep integration between the host process and the functions. 
For example, .NET class library functions can share binding APIs and types. 
However, this integration also requires a tighter coupling between the host process and the .NET function. 
For example, .NET functions running in-process are required to run on the same version of .NET as the Functions runtime. 
To enable you to run outside these constraints, you can now choose to run in an isolated process. 
This process isolation also lets you develop functions that use current .NET releases (such as .NET 5.0), not natively supported by the Functions runtime. 
Both isolated process and in-process C# class library functions run on .NET 6.0. To learn more, see Supported versions.

Because these functions run in a separate process, there are some feature and functionality differences between .NET isolated function apps and .NET class library function apps.

When running out-of-process, your .NET functions can take advantage of the following benefits:

    Fewer conflicts: because the functions run in a separate process, assemblies used in your app won't conflict with different version of the same assemblies used by the host process.
    
    Full control of the process: you control the start-up of the app and can control the configurations used and the middleware started.
    
    Dependency injection: because you have full control of the process, you can use current .NET behaviors for dependency injection and incorporating middleware into your function app.


Functions runtime version 	In-process  (.NET class library) 	Out-of-process (.NET Isolated)
Functions 4.x 	            .NET 6.0 	                        .NET 6.0
Functions 3.x 	            .NET Core 3.1 	                    .NET 5.01
Functions 2.x 	            .NET Core 2.12 	                    n/a
Functions 1.x 	            .NET Framework 4.8 	                n/a


The following packages are required to run your .NET functions in an isolated process:

    Microsoft.Azure.Functions.Worker
    Microsoft.Azure.Functions.Worker.Sdk



# Reference


https://docs.microsoft.com/en-us/azure/azure-functions/dotnet-isolated-process-guide
