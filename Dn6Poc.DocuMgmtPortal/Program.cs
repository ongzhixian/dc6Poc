using System.Text;
using Dn6Poc.DocuMgmtPortal.Logging;
using Microsoft.AspNetCore.Antiforgery;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.HttpLogging;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Http;
using Microsoft.IdentityModel.Tokens;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Http;
using AppStartup = Dn6Poc.DocuMgmtPortal.Services.AppStartupService;
using Dn6Poc.DocuMgmtPortal.Services;
using MongoDB.Driver;
using Dn6Poc.DocuMgmtPortal.MongoEntities;
using Microsoft.AspNetCore.Http.Json;

var builder = WebApplication.CreateBuilder(args);

AppStartup.ConfigureJsonOptions(builder.Services);

AppStartup.SetupLogging(builder.Host);

AppStartup.SetupHttpLogging(builder.Configuration, builder.Services);

AppStartup.SetupAuthentication(builder.Configuration, builder.Services);

AppStartup.SetupAuthorization(builder.Services);

AppStartup.SetupHttpClient(builder.Services);

AppStartup.SetupSession(builder.Services);

AppStartup.SetupAntiForgery(builder.Services);

AppStartup.SetupCookies(builder.Services);

AppStartup.SetupCors(builder.Services);

AppStartup.SetupSwagger(builder.Services);

// Add services to the container.

//builder.Services.AddHealthChecks();

builder.Services.AddHttpContextAccessor();

builder.Services.AddDistributedMemoryCache();

// builder.Services.AddDatabaseDeveloperPageExceptionFilter(); // Only if using EF

builder.Services.AddControllersWithViews();

// ...your services here...

builder.Services.AddHttpClient<LoginService>();

builder.Services.AddScoped<UserService>();

builder.Services.AddScoped<RoleService>();

builder.Services.AddSingleton<IMongoClient>(sp =>
    new MongoClient(builder.Configuration.GetValue<string>("mongoDb:safeTravel")));

builder.Services.AddSingleton<IMongoDatabase>(sp =>
{
    string mongoConnectionUrl = builder.Configuration.GetValue<string>("mongoDb:safeTravel");
    var mongoClient = new MongoClient(mongoConnectionUrl);
    var databaseName = MongoUrl.Create(mongoConnectionUrl).DatabaseName;
    return mongoClient.GetDatabase(databaseName);
});

builder.Services.AddSingleton<IMongoCollection<User>>(sp => sp.GetRequiredService<IMongoDatabase>().GetCollection<User>("user"));
    
var app = builder.Build();

// Configure the HTTP request pipeline.
// The correct middleware order should follow:
// ExceptionHandler
// HSTS --
// HttpsRedirection
// Static Files
// Routing
// CORS
// Authentication
// Authorization

// Development-only 
if (app.Environment.IsDevelopment())
{
    // Logging sample code:
    // Method 1: app.Logger
    //app.Logger.LogInformation("Your custom log message");
    // Method 2: Get Logger via DI
    //ILogger? logger = app.Services.GetService<ILogger<Program>>() ?? throw new Exception($"Service ({nameof(ILogger<Program>)} does not exist.");
    //logger.LogInformation("Your custom log message");

    //if (app.Configuration.GetValue<bool>("Application:EnableHttpLogging"))
    //    app.UseHttpLogging();

    //app.UseSwagger();
    //app.UseSwaggerUI(c =>
    //{
    //    c.SwaggerEndpoint("/swagger/v1/swagger.json", $"{openApiInfo.Title}; {openApiInfo.Version}");
    //});
}


if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();

    //app.UseDatabaseErrorPage
}
else
{
    app.UseExceptionHandler("/Home/Error");

    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseStaticFiles();

// app.UseCookiePolicy();

app.UseRouting();

// app.UseRequestLocalization();

app.UseCors("DebugAllowAll");

app.UseAuthentication();

app.UseAuthorization();

app.UseSession();

// app.UseResponseCompression();

// app.UseResponseCaching();

//app.MapRazorPages();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");


// Other development-only middleware
if (app.Environment.IsDevelopment())
{
    // Logging sample code:
    // Method 1: app.Logger
    //app.Logger.LogInformation("Your custom log message");
    // Method 2: Get Logger via DI
    //ILogger? logger = app.Services.GetService<ILogger<Program>>() ?? throw new Exception($"Service ({nameof(ILogger<Program>)} does not exist.");
    //logger.LogInformation("Your custom log message");

    //if (app.Configuration.GetValue<bool>("Application:EnableHttpLogging"))
    //    app.UseHttpLogging();

    //app.UseSwagger();
    //app.UseSwaggerUI(c =>
    //{
    //    c.SwaggerEndpoint("/swagger/v1/swagger.json", $"{openApiInfo.Title}; {openApiInfo.Version}");
    //});
}

app.Run();
