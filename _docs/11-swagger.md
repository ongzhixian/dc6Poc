# Swagger

## Nuget packages needed

`dotnet add .\Dn6Poc.TravalApi\ package Swashbuckle.AspNetCore`

## csproj changes needed

```xml
<PropertyGroup>
  <GenerateDocumentationFile>true</GenerateDocumentationFile>
  <NoWarn>$(NoWarn);1591</NoWarn>
</PropertyGroup>
```

##


Swashbuckle.AspNetCore.Annotations	
    Includes a set of custom attributes that can be applied to controllers, actions and models to enrich the generated Swagger
Swashbuckle.AspNetCore.Cli	
    Provides a command line interface for retrieving Swagger directly from a startup assembly, and writing to file
Swashbuckle.AspNetCore.ReDoc	
    Exposes an embedded version of the ReDoc UI (an alternative to swagger-ui)

# Reference

https://github.com/domaindrivendev/Swashbuckle.AspNetCore

https://swagger.io/docs/open-source-tools/swagger-ui/usage/cors/
https://swagger.io/docs/specification/2-0/paths-and-operations/


https://stackoverflow.com/questions/29701573/how-to-omit-methods-from-swagger-documentation-on-webapi-using-swashbuckle

