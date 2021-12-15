# ASP.NET 

## Require authenticated users

builder.Services.AddAuthorization(options =>
{
    options.FallbackPolicy = new AuthorizationPolicyBuilder()
        .RequireAuthenticatedUser()
        .Build();
});

# Reference

https://docs.microsoft.com/en-us/aspnet/core/security/authorization/policies?view=aspnetcore-6.0
https://docs.microsoft.com/en-us/aspnet/core/security/authorization/secure-data?view=aspnetcore-6.0#rau
https://docs.microsoft.com/en-us/aspnet/core/security/cookie-sharing?view=aspnetcore-6.0


https://docs.microsoft.com/en-us/aspnet/core/fundamentals/app-state?view=aspnetcore-6.0


Middleware ordering!
https://docs.microsoft.com/en-us/aspnet/core/fundamentals/middleware/?view=aspnetcore-6.0#order


https://stackoverflow.com/questions/40684357/decrypt-aspnetcore-session-cookie-in-asp-net-core

https://www.codemag.com/Article/2105051/Implementing-JWT-Authentication-in-ASP.NET-Core-5
