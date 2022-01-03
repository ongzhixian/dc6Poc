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
`dotnet new mvc -n Dn6Poc.DocuMgmtPortal`


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
By default, if we run the project with `dotnet watch run --project $args[0]`, the project runs in 'Development' mode.
But if we run the project with `dotnet watch run --project $args[0]`, the project runs in 'Production' mode.
To force the project to run in 'Development' mode with hot-reload, we should run it with:

`dotnet watch run --project $args[0] environment=development`

## Add nuget packages 

`dotnet add .\Dn6Poc.TravalApi\ package Microsoft.Extensions.DependencyInjection.Abstractions`
`dotnet add .\Dn6Poc.TravalApi\ package Microsoft.EntityFrameworkCore.SqlServer`
`dotnet add .\Dn6Poc.TravalApi\ package Microsoft.EntityFrameworkCore.Design`

`dotnet add .\Dn6Poc.TravalApi\ package Serilog`

`dotnet add .\Dn6Poc.TravalApi\ package MongoDB.Driver`

`dotnet add .\Dn6Poc.CountryApi\ package MongoDB.Driver`

`dotnet add .\Dn6Poc.DocuMgmtPortal\ package Microsoft.AspNetCore.Authentication.JwtBearer`
`dotnet add .\Dn6Poc.DocuMgmtPortal\ package Swashbuckle.AspNetCore`



dotnet add .\Dn6Poc.DocuMgmtPortal.E2eTests\ package Microsoft.Playwright
dotnet add .\Dn6Poc.DocuMgmtPortal.E2eTests\ package Microsoft.Playwright.MSTest

dotnet tool install --global Microsoft.Playwright.CLI
--Instead of--
dotnet add .\Dn6Poc.DocuMgmtPortal.E2eTests\ package Microsoft.Playwright.CLI

Add nuget package to project.


## Tools & Templates

dotnet tool install --global Microsoft.Playwright.CLI
dotnet new -i SpecFlow.Templates.DotNet
dotnet tool install --global SpecFlow.Plus.LivingDoc.CLI
dotnet tool install JetBrains.dotCover.GlobalTool -g
dotnet tool install -g JetBrains.ReSharper.GlobalTools

dotnet new -i BenchmarkDotNet.Templates

https://www.jetbrains.com/help/dotcover/Running_Coverage_Analysis_from_the_Command_LIne.html#to-install-dotcover-console-runner-as-a-net-global-tool
https://www.jetbrains.com/help/resharper/ReSharper_Command_Line_Tools.html#overview-video


find code issues in a solution, run:
jb inspectcode YourSolution.sln -o=<PathToOutputFile>
jb inspectcode --build --output=inspectcode-result.html --format=Html .\Dn6Poc.DocuMgmtPortal\Dn6Poc.DocuMgmtPortal.csproj

reformat code and fix code style in a solution, run:
jb cleanupcode YourSolution.sln

dotnet dotcover test  --dcReportType=HTML --dcOutput=dotcover.html  .\Dn6Poc.DocuMgmtPortal.Tests\

dotnet watch dotcover test  --dcReportType=HTML --dcOutput=dotcover.html  --project .\Dn6Poc.DocuMgmtPortal.Tests\
