using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);


// Code examples for reference of reading configuration from the appsettings.json file and user-secrets

var safeTravelConnectionString = builder.Configuration["ConnectionStrings:SafeTravel"];
Console.WriteLine($"SafeTravel connectionString is: [{safeTravelConnectionString}]");
// var author = builder.Configuration["Application:Author"];
// Console.WriteLine("Author: " + author);
// var environment = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");
// Console.WriteLine($"Environment is: [{environment}]");



// Setup dependencies for injection

// var optionsBuilder = new DbContextOptionsBuilder<Dn6Poc.TravalApi.DbContexts.SafeTravelContext>();
// optionsBuilder.UseSqlServer(@"Server=(localdb)\MSSQLLocalDB;Database=SafeTravel;Trusted_Connection=True;MultipleActiveResultSets=true");
// // optionsBuilder.UseSqlServer(ConfigurationManager.ConnectionStrings["BloggingDatabase"].ConnectionString);

// builder.Services.AddDbContext<Dn6Poc.TravalApi.DbContexts.SafeTravelContext>(b =>
//     b.UseSqlServer("asd")
// );
// builder.Services.AddSingleton<Dn6Poc.TravalApi.DbContexts.SafeTravelContext>();
// builder.Services.AddDbContext<Dn6Poc.TravalApi.DbContexts.SafeTravelContext>(
//     options => options.UseSqlServer(safeTravelConnectionString)
// );
// builder.Services.AddDbContext<Dn6Poc.TravalApi.DbContexts.SafeTravelContext>(
//     options => options.);

builder.Services.AddDbContext<Dn6Poc.TravalApi.DbContexts.SafeTravelContext>(
    o => o.UseSqlServer(safeTravelConnectionString));

builder.Services.AddScoped<GreetingService>();
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
