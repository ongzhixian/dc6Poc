using Microsoft.Extensions.DependencyInjection;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddSingleton<GreetingService>(new GreetingService());

var app = builder.Build();

app.MapGet("/", () => "Hello World!");

app.MapGet("/hello", (HttpContext context, GreetingService greetingService) => greetingService.SayHello(context.Request.Query["name"].ToString()));

app.Run();
