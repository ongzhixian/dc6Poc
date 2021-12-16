# ASP.NET 

## Require authenticated users

```cs
builder.Services.AddAuthorization(options =>
{
    options.FallbackPolicy = new AuthorizationPolicyBuilder()
        .RequireAuthenticatedUser()
        .Build();
});
```


# Authorize and Razor Pages CSS

If using `_Layout.cshtml.css`, you may encouner file errors such as:

```
The stylesheet https://localhost:5001/Login?ReturnUrl=%2FDn6Poc.DocuMgmtPortal.styles.css was not loaded because its MIME type, “text/html”, is not “text/css”.
```

So we have to add some rule to skip authorization checks or just put them in static CSS files.
See:
Serving static files
https://docs.microsoft.com/en-us/aspnet/core/fundamentals/static-files?view=aspnetcore-6.0#static-file-authorization



# Reference

https://docs.microsoft.com/en-us/aspnet/core/security/authorization/policies?view=aspnetcore-6.0
https://docs.microsoft.com/en-us/aspnet/core/security/authorization/secure-data?view=aspnetcore-6.0#rau
https://docs.microsoft.com/en-us/aspnet/core/security/cookie-sharing?view=aspnetcore-6.0


https://docs.microsoft.com/en-us/aspnet/core/fundamentals/app-state?view=aspnetcore-6.0


Middleware ordering!
https://docs.microsoft.com/en-us/aspnet/core/fundamentals/middleware/?view=aspnetcore-6.0#order


https://stackoverflow.com/questions/40684357/decrypt-aspnetcore-session-cookie-in-asp-net-core

https://www.codemag.com/Article/2105051/Implementing-JWT-Authentication-in-ASP.NET-Core-5

https://alimozdemir.medium.com/asp-net-core-jwt-and-refresh-token-with-httponly-cookies-b1b96c849742
https://dev.to/moe23/refresh-jwt-with-refresh-tokens-in-asp-net-core-5-rest-api-step-by-step-3en5

https://www.yogihosting.com/jwt-refresh-token-aspnet-core/