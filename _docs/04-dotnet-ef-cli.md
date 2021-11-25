# dotnet ef CLI

## Install

`dotnet tool install --global dotnet-ef`

`dotnet tool update --global dotnet-ef`

dotnet ef CLI requires this package

`dotnet add package Microsoft.EntityFrameworkCore.Design`

## Manageing DbContext

`dotnet ef dbcontext <command>`

Commands:
  info      Gets information about a DbContext type.
  list      Lists available DbContext types.
  optimize  Generates a compiled version of the model used by the DbContext.
  scaffold  Scaffolds a DbContext and entity types for a database.
  script    Generates a SQL script from the DbContext. Bypasses any migrations.

## Scaffolding DbContext

`dotnet ef dbcontext scaffold Name=ConnectionStrings:SafeTravel Microsoft.EntityFrameworkCore.SqlServer --context-dir DbContexts --output-dir Models --project .\Dn6Poc.TravalApi\ --force`

Warning: 
    The `--force` option overwrites any existing files! Use with care. 
    The idea here is that we should never make any changes to scaffolded code.
    If we do need to make enhancements, we should add them to partial classes.


If we are using a connectionString to scaffold:

`dotnet ef dbcontext scaffold "Server=.;Database=SafeTravel;Trusted_Connection=True;" Microsoft.EntityFrameworkCore.SqlServer -o Models --project .\Dn6Poc.TravalApi\`

The following command scaffolds DbContext class in `DbContexts` folder and data modelsin `Models` folder:  

`dotnet ef dbcontext scaffold ... --context-dir DbContexts --output-dir Models`

To specify a specific namespace, use:

`dotnet ef dbcontext scaffold ... --namespace Your.Namespace --context-namespace Your.DbContext.Namespace`

To scaffold selected tables:

`dotnet ef dbcontext scaffold ... --table Artist --table Album`

`--schema` option can be used to include every table within a schema.


# Reference

https://docs.microsoft.com/en-us/ef/core/managing-schemas/scaffolding?tabs=dotnet-core-cli


https://go.microsoft.com/fwlink/?linkid=2131148. 
For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.


<CONNECTION> 	The connection string to the database. For ASP.NET Core 2.x projects, the value can be name=<name of connection string>. In that case the name comes from the configuration sources that are set up for the project.
<PROVIDER> 	The provider to use. Typically this is the name of the NuGet package, for example: Microsoft.EntityFrameworkCore.SqlServer.