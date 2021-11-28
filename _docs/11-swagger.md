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

# Reference


https://swagger.io/docs/open-source-tools/swagger-ui/usage/cors/
https://swagger.io/docs/specification/2-0/paths-and-operations/


https://stackoverflow.com/questions/29701573/how-to-omit-methods-from-swagger-documentation-on-webapi-using-swashbuckle