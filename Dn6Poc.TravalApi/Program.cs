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

    ////////////////////////////////////////
    // Setup routes

    RouteService.ConfigureRoutes(app, log);
    // app.MapGet("/", () => "Hello World!");
    // app.MapGet("/hello", (HttpContext context, GreetingService greetingService) => greetingService.SayHello(context.Request.Query["name"].ToString()));


    if (app.Environment.IsDevelopment())
    {
        app.UseDeveloperExceptionPage();
        app.UseSwagger();
        // app.UseSwaggerUI();
        app.UseSwaggerUI(c =>
        {
            c.SwaggerEndpoint("/swagger/v1/swagger.json", "My APIddd V1"); 
        });

        // Old style of using Swagger
        // app.UseSwagger(options =>
        // {
        //     // Uncomment below to serialize to v2 instead of v3 (OpenApi)
        //     // options.SerializeAsV2 = true;
        // });
        // app.UseSwaggerUI(options =>
        // {
        //     options.SwaggerEndpoint("/swagger/v1/swagger.json", "v1");
        //     options.RoutePrefix = string.Empty;
        // });

        Console.WriteLine("Development environment");
        // UseSwagger in development mode only
    }
    else
    {
        // app.UseExceptionHandler()
        // app.UseHsts();
        // app.UseCors();
        Console.WriteLine("Other  environment");

        // app.UseHttpsRedirection();
        // app.UseStaticFiles();

        // Add this line; you'll need `using Serilog;` up the top, too
        //app.UseSerilogRequestLogging();
    }


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
