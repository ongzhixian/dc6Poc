# dotnet CLI

## Templates

The default `dotnet new` command now only show a limited number of common templates.

Use:

1.  `dotnet new --list`
    List all the templates available

2.  `dotnet new --update-check`
    Check for new templates

3.  `dotnet new --update-apply`
    Install template packages


## Create new project


Create new empty web project (suitable for minimal WebApi projects):

`dotnet new web -n Dn6Poc.TravalApi`

Create new WebApi project:

`dotnet new webapi -n Dn6Poc.TravelApi`

## Add project to solution

`dotnet sln .\dn6Poc.sln add .\Dn6Poc.TravelApi\`

## Build

`dotnet build`

Build all projects

`dotnet build .\Dn6Poc.TravelApi\`

Build specific project

## Run project 

`dotnet run --project .\Dn6Poc.TravelApi\`

Run specific project.

`dotnet watch run --project .\Dn6Poc.TravelApi\`

Run specific project with hot-reload.

## Add nuget packages 

`dotnet add .\Dn6Poc.TravalApi\ package Microsoft.Extensions.DependencyInjection.Abstractions`
`dotnet add .\Dn6Poc.TravalApi\ package Microsoft.EntityFrameworkCore.SqlServer`
`dotnet add .\Dn6Poc.TravalApi\ package Microsoft.EntityFrameworkCore.Design`

Add nuget package to project.

