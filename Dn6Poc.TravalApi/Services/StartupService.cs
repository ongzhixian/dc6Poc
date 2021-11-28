using Serilog;
using Microsoft.OpenApi.Models;
public sealed class StartupService
{
    public static void PrintConfigurationSettings(ConfigurationManager config)
    {
        // Example of printing Environment variables
        // var environment = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");
        // Console.WriteLine($"Environment is: [{environment}]");

        // Code examples for reference of reading configuration from the appsettings.json file and user-secrets
        // var safeTravelConnectionString = builder.Configuration["ConnectionStrings:SafeTravel"];
        // Console.WriteLine($"SafeTravel connectionString is: [{safeTravelConnectionString}]");

        // Example of reading from 'appsettings.json'
        Console.WriteLine($"Application: [{config["Application:Name"]}]");

        // Example of reading from 'user-secrets'
        Console.WriteLine($"ConnectionString:SafeTravel: [{config["ConnectionStrings:SafeTravel"]}]");
    }

    public static void SetupInjectionServices(IServiceCollection services)
    {
        ////////////////////////////////////////
        // Setup dependencies for injection

        services.AddEndpointsApiExplorer();
        services.AddSwaggerGen(options =>
        {
            options.SwaggerDoc("v1", new OpenApiInfo
            {
                Version = "v1",
                Title = "Travel API",
                Description = "An ASP.NET Core Web API for managing Travel API items",
                TermsOfService = new Uri("https://example.com/terms"),
                Contact = new OpenApiContact
                {
                    Name = "Example Contact",
                    Url = new Uri("https://example.com/contact")
                },
                License = new OpenApiLicense
                {
                    Name = "Example License",
                    Url = new Uri("https://example.com/license")
                }
            });

            // Old style of documenting REST WebApi via XML
            // var xmlFilename = $"{System.Reflection.Assembly.GetExecutingAssembly().GetName().Name}.xml";
            // options.IncludeXmlComments(System.IO.Path.Combine(AppContext.BaseDirectory, xmlFilename));
        });

        // Works
        // builder.Services.AddDbContext<Dn6Poc.TravalApi.DbContexts.SafeTravelContext>(
        //     // o => o.UseSqlServer(safeTravelConnectionString) // Works
        //     //o => o.UseSqlServer("ConnectionStrings:SafeTravel") // NG
        //     // o => o.UseSqlServer("Name=ConnectionStrings:SafeTravel") // to try
        //     o => o.UseSqlServer("name=ConnectionStrings:SafeTravel") // works
        //     );

        // Works
        // builder.Services.AddLogging();
        // builder.Services.AddDbContext<Dn6Poc.TravalApi.DbContexts.SafeTravelContext>();
        // builder.Services.AddScoped<GreetingService>();
        // --OR--
        // builder.Services.AddSingleton<GreetingService>(new GreetingService(builder.Configuration));

        services.AddDbContext<Dn6Poc.TravalApi.DbContexts.SafeTravelContext>();
        services.AddScoped<GreetingService>();
    }

    // public static void SetupContentRoot(ConfigureHostBuilder host)
    // {
    //     // ZX: Probably don't need this for now; comment out
    //     // string executingDirectory = Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location) ?? string.Empty;
    //     // host.UseContentRoot(executingDirectory);
    //     //
    //     // The above does not work and will give the following error:
    //     // Changing the host configuration using WebApplicationBuilder.Host is not supported. 
    //     // Use WebApplication.CreateBuilder(WebApplicationOptions) instead
    // }

    public static void SetupLogging(ConfigureHostBuilder host)
    {
        host.UseSerilog((hostBuilderContext, loggerConfiguration) =>
        {
            loggerConfiguration.ReadFrom.Configuration(hostBuilderContext.Configuration);
        });
    }


    // public static void ConfigureRoutes(WebApplication app, ILogger log)
    // {
    //     app.MapGet("/", () => "Hello World!");

    //     app.MapGet("/hello", (HttpContext context, GreetingService greetingService) => greetingService.SayHello(context.Request.Query["name"].ToString()));

    //     app.MapGet("/test", (HttpContext context) =>
    //     {
    //         Console.WriteLine("In /test");
    //         log.LogInformation("Some inform");
    //         return "In /test";

    //     });

    // }
}