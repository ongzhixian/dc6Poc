# ASP.NET MVC


# Calling Web APIs

Assuming that the url to API is: `https://localhost:7241/api/Authentication/login`

When using HttpClient, the base address should end with slash `/`, as in:

`client.BaseAddress = new Uri("https://localhost:7241/api/Authentication/");`

Then when calling the PostAsJson method, just state `login`:

`var postTask = client.PostAsJsonAsync<LoginModel>("login", postData);`

Base With `/`:              Yes     No
Post with `/login` (yes):   NG      NG 
Post with `login` (no):     OK      404

The NG results return a HTTP 200, but contents will actually say model is not passed in correctly (missing data).


```cs:Example
using (var client = new HttpClient())
{
    client.BaseAddress = new Uri("https://localhost:7241/api/Authentication/");

    LoginModel postData = new LoginModel
    {
        Username = "yayay",
        Password = "yayayPassword"
    };

    //HTTP POST
    var postTask = client.PostAsJsonAsync<LoginModel>("login", postData);
    postTask.Wait();

    var result = postTask.Result;
    if (result.IsSuccessStatusCode)
    {
        // return RedirectToAction("Index");
    }
}
```

## Create HttpClients

Use IHttpClientFactory!
It has logging built in.

See also:
https://docs.microsoft.com/en-us/dotnet/core/compatibility/aspnet-core/5.0/http-httpclient-instances-log-integer-status-codes




## IHttpContextAccessor registration

No service for type 'Microsoft.AspNetCore.Http.IHttpContextAccessor' has been registered.

IHttpContextAccessor is no longer wired up by default, you have to register it yourself

services.AddHttpContextAccessor();
--equivalent to--
services.TryAddSingleton<IHttpContextAccessor, HttpContextAccessor>();


services.AddScoped<IActionContextAccessor, ActionContextAccessor>()


https://stackoverflow.com/questions/37371264/invalidoperationexception-unable-to-resolve-service-for-type-microsoft-aspnetc



## Temp data --  TempData vs ViewData vs ViewBag vs Session

TempData is used to transfer data from view to controller, controller to view, or from one action method to another action method of the same or a different controller. 
The data only lasts for a single round-trip: set in one request, removed after the next request.

ViewData/ViewBag transfers data from Controller to View. 
Only difference is ViewData is of Dictionary type, whereas ViewBag is of dynamic type. 
Both store data to the same dictionary internally. 

Session


## TempData outside controller


```cs
var tempDataDictionaryFactory = context.RequestServices.GetRequiredService<ITempDataDictionaryFactory>();
var tempDataDictionary = tempDataDictionaryFactory.GetTempData(context);
if (tempDataDictionary.TryGetValue(MyClaims.Claim1, out object value))
{
    return (string)value;
}

```

To save data to TempData, you will also have to call `TempDataDictionary.Save()`


## Cache

HttpRuntime.Cache vs. HttpContext.Current.Cache

HttpRuntime.Cache is the recommended technique.
HttpRuntime.Cache is always available, even in console apps like nunit!


## Cookies


https://docs.microsoft.com/en-us/aspnet/core/security/gdpr?view=aspnetcore-6.0#tempdata-provider-and-session-state-cookies-arent-essential

Revoking GDPR
https://www.joeaudette.com/blog/2018/08/28/gdpr---adding-a-revoke-consent-button-in-aspnet-core


## Data Protection

Packages

    Microsoft.AspNetCore.DataProtection contains the core implementation of the data protection system, including core cryptographic operations, key management, configuration, and extensibility. To instantiate the data protection system (for example, adding it to an IServiceCollection) or modifying or extending its behavior, reference Microsoft.AspNetCore.DataProtection.

    Microsoft.AspNetCore.DataProtection.Extensions contains additional APIs which developers might find useful but which don't belong in the core package. For instance, this package contains factory methods to instantiate the data protection system to store keys at a location on the file system without dependency injection (see DataProtectionProvider). It also contains extension methods for limiting the lifetime of protected payloads (see ITimeLimitedDataProtector).

    Microsoft.AspNetCore.Cryptography.KeyDerivation provides an implementation of the PBKDF2 password hashing routine and can be used by systems that must handle user passwords securely. For more information, see Hash passwords in ASP.NET Core.



https://docs.microsoft.com/en-us/aspnet/core/security/data-protection/introduction?view=aspnetcore-6.0


## Web API

```cs
builder.Services.AddControllers()
    .AddJsonOptions(
        options => options.JsonSerializerOptions.PropertyNamingPolicy = null);

...
[BsonElement("Name")]
[JsonPropertyName("Name")]
public string BookName { get; set; } = null!;
```

https://docs.microsoft.com/en-us/aspnet/core/tutorials/first-mongo-app?view=aspnetcore-6.0&tabs=visual-studio

# References


Partials
https://docs.microsoft.com/en-us/aspnet/core/mvc/views/partial?view=aspnetcore-6.0

Example: Can track cookies
https://github.com/dotnet/AspNetCore.Docs/blob/main/aspnetcore/mvc/views/partial/sample/PartialViewsSample/Views/Shared/_CookieConsentPartial.cshtml


IHttpClientFactory
https://www.stevejgordon.co.uk/introduction-to-httpclientfactory-aspnetcore
https://www.stevejgordon.co.uk/httpclientfactory-asp-net-core-logging

https://josef.codes/tidy-up-your-httpclient-usage/

Polly
https://github.com/App-vNext/Polly

Refit 
https://github.com/reactiveui/refit

Response Compression
https://docs.microsoft.com/en-us/aspnet/core/performance/response-compression?view=aspnetcore-6.0

Response Caching
https://docs.microsoft.com/en-us/aspnet/core/performance/caching/response?view=aspnetcore-6.0


https://stackoverflow.com/questions/683646/asp-net-mvc-donut-caching-and-tempdata

https://weblogs.asp.net/pjohnson/httpruntime-cache-vs-httpcontext-current-cache
