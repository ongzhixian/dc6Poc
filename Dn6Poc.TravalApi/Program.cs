using Microsoft.Extensions.DependencyInjection;

var builder = WebApplication.CreateBuilder(args);


// Code examples for reference of reading configuration from the appsettings.json file and user-secrets

var safeTravelConnectionString = builder.Configuration["ConnectionStrings:SafeTravel"];
Console.WriteLine($"SafeTravel connectionString is: [{safeTravelConnectionString}]");
// var author = builder.Configuration["Application:Author"];
// Console.WriteLine("Author: " + author);
// var environment = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");
// Console.WriteLine($"Environment is: [{environment}]");



// Setup dependencies for injection

builder.Services.AddSingleton<Dn6Poc.TravalApi.DbContexts.SafeTravelContext>();
builder.Services.AddSingleton<GreetingService>();
// --OR--
// builder.Services.AddSingleton<GreetingService>(new GreetingService(builder.Configuration));


var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    Console.WriteLine("Development environment");
}
else
{
    Console.WriteLine("Other  environment");
}


app.MapGet("/", () => "Hello World!");

app.MapGet("/hello", (HttpContext context, GreetingService greetingService) => greetingService.SayHello(context.Request.Query["name"].ToString()));

app.Run();
