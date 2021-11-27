using ILogger = Microsoft.Extensions.Logging.ILogger;
using Initial = StartupService;

var builder = WebApplication.CreateBuilder(args);

Initial.SetupInjectionServices(builder.Services);

// Initial.SetupContentRoot(builder.Host);

Initial.SetupLogging(builder.Host);

Initial.PrintConfigurationSettings(builder.Configuration);

using (var app = builder.Build())
{
    ILogger log = app.Services.GetRequiredService<ILogger<Program>>();

    log.LogInformation("Application start");

    if (app.Environment.IsDevelopment())
    {
        app.UseDeveloperExceptionPage();
        Console.WriteLine("Development environment");
    }
    else
    {
        // app.UseExceptionHandler()
        // app.UseHsts();
        // app.UseCors();
        Console.WriteLine("Other  environment");
    }

    ////////////////////////////////////////
    // Setup routes

    RouteService.ConfigureRoutes(app, log);
    // app.MapGet("/", () => "Hello World!");
    // app.MapGet("/hello", (HttpContext context, GreetingService greetingService) => greetingService.SayHello(context.Request.Query["name"].ToString()));

    app.Run();

}

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
