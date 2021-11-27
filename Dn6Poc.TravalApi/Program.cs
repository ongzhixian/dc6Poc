using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using Serilog;
using ILogger = Microsoft.Extensions.Logging.ILogger;

var builder = WebApplication.CreateBuilder(args);

// Code examples for reference of reading configuration from the appsettings.json file and user-secrets
// var safeTravelConnectionString = builder.Configuration["ConnectionStrings:SafeTravel"];
// Console.WriteLine($"SafeTravel connectionString is: [{safeTravelConnectionString}]");

// var author = builder.Configuration["Application:Author"];
// Console.WriteLine("Author: " + author);
// var environment = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");
// Console.WriteLine($"Environment is: [{environment}]");

////////////////////////////////////////
// Setup dependencies for injection

// Works
// builder.Services.AddDbContext<Dn6Poc.TravalApi.DbContexts.SafeTravelContext>(
//     // o => o.UseSqlServer(safeTravelConnectionString) // Works
//     //o => o.UseSqlServer("ConnectionStrings:SafeTravel") // NG
//     // o => o.UseSqlServer("Name=ConnectionStrings:SafeTravel") // to try
//     o => o.UseSqlServer("name=ConnectionStrings:SafeTravel") // works
//     );

// Works
// builder.Services.AddLogging();
builder.Services.AddDbContext<Dn6Poc.TravalApi.DbContexts.SafeTravelContext>();
builder.Services.AddScoped<GreetingService>();
// --OR--
// builder.Services.AddSingleton<GreetingService>(new GreetingService(builder.Configuration));


// ZX: Probably don't need this for now; comment out
// string executingDirectory = executingDirectory = Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location) ?? string.Empty;
// builder.Host.UseContentRoot(executingDirectory);


builder.Host.UseSerilog((hostBuilderContext, loggerConfiguration) =>
{
    loggerConfiguration.ReadFrom.Configuration(hostBuilderContext.Configuration);
});

var app = builder.Build();


////////////////////////////////////////
// Main Script

if (app.Environment.IsDevelopment())
{
    Console.WriteLine("Development environment");
}
else
{
    Console.WriteLine("Other  environment");
}

////////////////////////////////////////
// Setup routes

app.MapGet("/", () => "Hello World!");

app.MapGet("/hello", (HttpContext context, GreetingService greetingService) => greetingService.SayHello(context.Request.Query["name"].ToString()));

ILogger log = app.Services.GetRequiredService<ILogger<Program>>();

log.LogInformation("Application start");

app.Run();

// using (ILoggerFactory loggerFactory = new LoggerFactory())
// using (IHost host = CreateHostBuilder(args).Build())
// {
//     IServiceProvider services = host.Services;

//     ILogger log = services.GetRequiredService<ILogger<Program>>();

//     log.LogInformation("Application start");

//     try
//     {
//         await host.RunAsync();
//     }
//     catch (Exception ex)
//     {
//         log.LogError(ex, string.Empty);
//         throw;
//     }
// }