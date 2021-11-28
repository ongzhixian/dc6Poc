using Dn6Poc.TravalApi.Models;

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
        app.MapGet("/country", () => "TODO: list countries");
        app.MapGet("/country/{id}", () => "TODO: return country");
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