using Dn6Poc.TravalApi.Models;
using MongoDb = Dn6Poc.TravalApi.Models.MongoDb;


public sealed class RouteService
{
    public static void ConfigureRoutes(WebApplication app, ILogger log)
    {
        app.MapGet("/", () => "Hello World!")
        .Accepts<Country>("application/json")
        .Produces<Country>(StatusCodes.Status201Created)
        .WithDisplayName("GetAllJobs2")
        .WithName("GetAllJobs")
        .WithTags("Getters");
        ;

        app.MapGet("/hello", (HttpContext context, GreetingService greetingService) => greetingService.SayHello(context.Request.Query["name"].ToString()));

        app.MapGet("/test", (HttpContext context) =>
        {
            Console.WriteLine("In /test");
            log.LogInformation("Some inform");
            return "In /test";
        });

        //app.MapGet    // Get country      /country?startsWith=si
        //app.MapGet    // Get country      /country/SG
        //app.MapPost   // Add country      /country
        //app.MapPut    // Update country   /country/SG
        //app.MapDelete // Remove country   /country/SG
        
        // string prefix = "/country";
        app.MapGet("/country", async (HttpContext http, CountryService service) => await service.Get(http));

        // app.MapGet("/country2", async (HttpContext http) => {
        //     HttpContext http
        //     var result = await countryService.GetCountriesAsync();
        //     return await http.Response.WriteAsJsonAsync<List<MongoDb.Country>>(result);
        // });
        // app.MapGet("/country2", async (CountryService service) => await service.GetCountriesAsync());
        
        // app.MapGet("/country/{id}", async (CountryService service, string id) => await service.GetCountry(id));
        
        
        app.MapPost("/country", () => "TODO: add new country").WithTags("Create");
        app.MapPut("/country/{id}", (http) => {  
            http.Response.StatusCode = 200; 
            // http.Response.Body.WriteAsync() = "TODO: update existing country";
            return Task.CompletedTask; }
        )
        ;

        app.MapDelete("/country/{id}", () => Task.CompletedTask)
        .WithGroupName("COU")
        .WithTags("Delete")
        .WithDisplayName("AAA")
        .WithName("ASAS")
        ;

    }
}