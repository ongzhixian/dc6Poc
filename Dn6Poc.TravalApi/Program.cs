using Microsoft.Extensions.DependencyInjection;

var builder = WebApplication.CreateBuilder(args);

// Code examples of reading configuration from the appsettings.json file and user-secrets
// var movieApiKey = builder.Configuration["Movies__ServiceApiKey2"];
// var author = builder.Configuration["Application:Author"];
// Console.WriteLine("Author: " + author);

// Setup dependencies for injection
// builder.Services.AddSingleton<GreetingService>(new GreetingService(builder.Configuration));
builder.Services.AddSingleton<GreetingService>();

var app = builder.Build();

app.MapGet("/", () => "Hello World!");

app.MapGet("/hello", (HttpContext context, GreetingService greetingService) => greetingService.SayHello(context.Request.Query["name"].ToString()));

app.Run();
