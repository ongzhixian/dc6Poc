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
