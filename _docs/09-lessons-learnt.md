# Lessons Learnt

## ef core `--no-onconfiguring` wrong conclusion

Previously, I wrote that `--no-onconfiguring` option which adds `OnConfiguring` to the DbContext will result in code that does not work. 
This is a wrong conclusion on my part.

### What I wrote:

The `--no-onconfiguring` option add `OnConfiguring` to the DbContext:

```cs
protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
{
    if (!optionsBuilder.IsConfigured)
    {
        optionsBuilder.UseSqlServer("Name=ConnectionStrings:SafeTravel");
    }
}
```

This is problematic code because of the following line that is generated:

`optionsBuilder.UseSqlServer("Name=ConnectionStrings:SafeTravel");`

The named-connectionString does not really work in .NET Core.
It is meant to work with the connection string defined in using 
.NET Framework styled configuration files (`app.config` or `web.config`).
This option is added in EF Core 5.0.
The following error message is what you will get when you try to use named connectionstrings in .NET Core projects:

```
InvalidOperationException: A named connection string was used, but the name 'SafeTravel' was not found in the application's configuration. 
Note that named connection strings are only supported when using 'IConfiguration' and a service provider, such as in a typical ASP.NET Core application. 
See https://go.microsoft.com/fwlink/?linkid=850912 for more information.
```

### Reason/Explanation/Fix

I did not read the error messages properly.
The problematic code was in the `Program.cs` file where I defined the depencencies to be injected.

```cs
builder.Services.AddDbContext<Dn6Poc.TravalApi.DbContexts.SafeTravelContext>();
builder.Services.AddSingleton<GreetingService>();
```

During compilation, there is no error with this code.
But come runtime, when the dependencies are injected, it will throw the following error:

```
Unhandled exception. System.AggregateException: Some services are not able to be constructed 
(Error while validating the service descriptor 'ServiceType: GreetingService Lifetime: Singleton ImplementationType: GreetingService': 
    Cannot consume scoped service 'Dn6Poc.TravalApi.DbContexts.SafeTravelContext' from singleton 'GreetingService'.)
```

The problem here is that the `GreetingService` is a singleton, but the `SafeTravelContext` is a scoped service.

Objects injected via `AddDbContext` are scoped.
See: https://github.com/dotnet/efcore/blob/main/src/EFCore/Extensions/EntityFrameworkServiceCollectionExtensions.cs#L340

So the correct way to fix this is to change scope of GreetingService to scoped:

```cs
builder.Services.AddDbContext<Dn6Poc.TravalApi.DbContexts.SafeTravelContext>();
builder.Services.AddScoped<GreetingService>();
```

See the below link documentation for more information why we cannot mix calling Singleton and Scoped services:
See: https://dotnetcoretutorials.com/2018/03/20/cannot-consume-scoped-service-from-singleton-a-lesson-in-asp-net-core-di-scopes/