# Azure Functions CLI

## Create project

func init Dn6Poc.TravelFunc
func init Dn6Poc.TravelFunc --worker-runtime dotnet-isolated


func new --name CountryApi --template "HTTP trigger" --authlevel "anonymous"

Note: 

    For some reason (maybe my bad), 
    the AzureFunctionsVersion may default to "v3" when using net6.0.

    This may result in the following error when you ran `func start`:
    Invalid combination of TargetFramework and AzureFunctionsVersion is set

    ```
    Build FAILED.

    C:\Users\zhixian\.nuget\packages\microsoft.azure.functions.worker.sdk\1.3.0\build\Microsoft.Azure.Functions.Worker.Sdk.targets(50,7): error : Invalid combination of TargetFramework and AzureFunctionsVersion is set. [D:\src\github\dn6Poc\Dn6Poc.TravelFunc\Dn6Poc_TravelFunc.csproj]
    ```

    So change it to "v4". 


    ```xml:csproj
    <PropertyGroup>
        <TargetFramework>net6.0</TargetFramework>
        <AzureFunctionsVersion>v4</AzureFunctionsVersion>
        <OutputType>Exe</OutputType>
    </PropertyGroup>
    ```

