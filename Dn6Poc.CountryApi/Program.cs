using Microsoft.OpenApi.Models;
using Serilog;
using ILogger = Microsoft.Extensions.Logging.ILogger;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddScoped<CountryService>();

OpenApiInfo openApiInfo = builder.Configuration.GetSection("Swagger:OpenApiInfo").Get<OpenApiInfo>();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc(openApiInfo.Version, openApiInfo);

    // Set the display order of the actions here.
    // options.OrderActionsBy((apiDesc) => $"{apiDesc.ActionDescriptor.RouteValues["controller"]}_{apiDesc.HttpMethod}");
    // options.OrderActionsBy((desc) => {});
});

builder.Host.UseSerilog((hostBuilderContext, loggerConfiguration) =>
{
    loggerConfiguration.ReadFrom.Configuration(hostBuilderContext.Configuration);
});

builder.Services.AddCors(options =>
{
    options.AddPolicy(name: "DebugAllowAll",
                      builder => builder.WithOrigins("*"));
});

var app = builder.Build();

ILogger log = app.Services.GetRequiredService<ILogger<Program>>();

log.LogInformation("Application start");


// GET      /country        // "/" --or-- ""
// GET      /country/{id}   // "/country/sg"
// POST     /country        // "/country"
// PUT      /country/{id}   // "/country/sg"
// DELETE   /country/{id}   // "/country/sg"

app.MapGet("/country", async (CountryService countryService) => (await countryService.GetCountriesAsync()).Select(x => new {
    name = x.Name,
    code = x.Code
}));

// app.MapGet("/country", async (CountryService service) => await service.GetCountriesAsync());


// app.MapGet("/country", async (CountryService service) => await service.GetCountriesAsync())
//     // .Accepts<>("application/json")
//     .Produces(StatusCodes.Status200OK)
//     //.WithGroupName("Group name country") // This is the equivalent of adding the attributes at Controller-level
//     .WithDisplayName("Display name country")
//     .WithName("Namecountry") // Refers to endpoint name;  Swagger page not working
//     .WithTags("Country")
//     ;

// app.MapGet("/{id}", (int id) => {
//     return new Country {
//         Id = 2472,
//         CountryName = "Some country"
//     };
//     // return "heloo" + id.ToString();
//     // return Results.Ok; // This adds a bunch of schema object
//     // NoContent();
// })
// // .WithGroupName("Group name country id") // Controller-level equiv attributes
// // .Accepts(true, "application/json", "plain/text")
// // .Accepts(typeof(string), "application/json", "plain/text")
// .Produces<Country>(StatusCodes.Status200OK)
// .Produces(StatusCodes.Status404NotFound)
// .WithDisplayName("Display name country id")
// .WithName("Namecountry id") // Swagger page not working
// .WithTags("HTTP GET", "Get specific")
// ;



// app.MapPost("/country", () => "TODO: add new country").WithTags("Create");

//TODO
// app.MapPost("/", (Country country) => {
//     return new Country {
//         Id = country.Id,
//         CountryName = country.CountryName
//     };
// })
// // .WithGroupName("Group name country id") // Controller-level equiv attributes
// .Accepts<Country>("application/json")
// .Produces<Country>(StatusCodes.Status200OK)
// .Produces(StatusCodes.Status404NotFound)
// .WithDisplayName("Add  country")
// .WithName("addCountry") // Swagger page not working
// .WithTags("HTTP POST", "Post specific")
// ;


// TODO
// app.MapPut("/{id}", (Country country) => {
//     return new Country {
//         Id = country.Id,
//         CountryName = country.CountryName
//     };
//     // return "heloo" + id.ToString();
//     // return Results.Ok; // This adds a bunch of schema object
//     // NoContent();
// })
// // .WithGroupName("Group name country id") // Controller-level equiv attributes
// .Accepts<Country>("application/json")
// .Produces<Country>(StatusCodes.Status200OK)
// .Produces(StatusCodes.Status404NotFound)
// .WithDisplayName("Update  country")
// .WithName("updateCountry") // Swagger page not working
// .WithTags("HTTP PUT", "Put specific")
// ;



// TODO
// app.MapDelete("/{id}", () => {
//     return Results.Ok;
//     //Results.NotFound
    
//     // return "heloo" + id.ToString();
//     // return Results.Ok; // This adds a bunch of schema object
//     // NoContent();
// })
// // .WithGroupName("Group name country id") // Controller-level equiv attributes
// // .Accepts<Country>("application/json")
// .Produces(StatusCodes.Status200OK)
// .Produces(StatusCodes.Status404NotFound)
// .WithDisplayName("Delete country")
// .WithName("deleteCountry") // Swagger page not working
// .WithTags("HTTP DELETE", "Delete specific")
// ;


// app.MapPut("/country/{id}", (http) =>
// {
//     http.Response.StatusCode = 200;
//     // http.Response.Body.WriteAsync() = "TODO: update existing country";
//     return Task.CompletedTask;
// }
// )
// ;

// app.MapDelete("/country/{id}", () => Task.CompletedTask)
// .WithGroupName("COU")
// .WithTags("Delete")
// .WithDisplayName("AAA")
// .WithName("ASAS")
// ;

if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", $"{openApiInfo.Title}; {openApiInfo.Version}");
    });

    Console.WriteLine("Development environment");
}

// Console.WriteLine("Other  environment");
// app.UseExceptionHandler()
// app.UseHsts();
app.UseCors();

// app.UseHttpsRedirection();
app.UseStaticFiles();

// Add this line; you'll need `using Serilog;` up the top, too
//app.UseSerilogRequestLogging();

app.Run();
