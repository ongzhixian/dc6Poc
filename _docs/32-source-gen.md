# Source Generators

## What is it?

C# Analyzers that can emit C# source code.

NOTE! Currently only .NET Standard 2.0 assemblies can be used as Source Generators.

## Implementation

```xml:Class library csproj
<Project Sdk="Microsoft.NET.Sdk">

  <PropertyGroup>
    <TargetFramework>netstandard2.0</TargetFramework>
  </PropertyGroup>

  <ItemGroup>
    <PackageReference Include="Microsoft.CodeAnalysis.CSharp" Version="4.0.1" PrivateAssets="all" />
    <PackageReference Include="Microsoft.CodeAnalysis.Analyzers" Version="3.3.3" PrivateAssets="all" />
  </ItemGroup>

</Project>
```

## Debugger

```cs:Urgh!
#if DEBUG
if (!Debugger.IsAttached)
{
    Debugger.Launch();
}
#endif
```

New!
You need to have the .NET Compiler Platform SDK installed. 
Its an optional component in Visual Studio. (No Visual Studio?!)

You need to add <IsRoslynComponent>true</IsRoslynComponent> to the project file of your source generator.
This will make the Roslyn Component debugger type show up in Project Properties
You need to have your source generator set as the Startup Project


# Reference

The pain points of C# source generators
https://turnerj.com/blog/the-pain-points-of-csharp-source-generators

Cookbook: C# Source Generators
https://github.com/dotnet/roslyn/blob/main/docs/features/source-generators.cookbook.md


https://www.reddit.com/r/csharp/comments/mqzc4r/source_generators_has_anyone_figured_out_the_new/