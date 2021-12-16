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

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddHttpClient(); // Add IHttpClientFactory

builder.Services.AddHttpLogging(logging =>
{
    // Customize HTTP logging here.
    logging.LoggingFields = HttpLoggingFields.All;
    //logging.RequestHeaders.Add("My-Request-Header");
    //logging.ResponseHeaders.Add("My-Response-Header");
    //logging.MediaTypeOptions.AddText("application/javascript");
    logging.RequestBodyLogLimit = 4096;
    logging.ResponseBodyLogLimit = 4096;
});

// Place the following after all AddHttpClient registrations to implement our custom logging
builder.Services.RemoveAll<IHttpMessageHandlerBuilderFilter>();
builder.Services.AddSingleton<IHttpMessageHandlerBuilderFilter, CustomLoggingHttpMessageHandlerBuilderFilter>();

var cookiePolicyOptions = new CookiePolicyOptions
{
    MinimumSameSitePolicy = SameSiteMode.Strict,
};

//AntiForgeryConfig.CookieName = "__YourTokenName";
builder.Services.AddAntiforgery(opts => opts.Cookie.Name = "MyAntiforgeryCookie");

builder.Services.AddDistributedMemoryCache();

builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(20);
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
    //options.Cookie.SameSite = SameSiteMode.Lax;
    options.Cookie.Name = "Cookie1";
});


// Add services to the container.
builder.Services.AddControllersWithViews();

//builder.Services.AddAuthorization();
builder.Services.AddAuthorization(options =>
{
    options.FallbackPolicy = new AuthorizationPolicyBuilder()
        .RequireAuthenticatedUser()
        .Build();
});

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        // options.Authority = "https://localhost:7241";
        // options.Audience = "weatherforecast";
        options.TokenValidationParameters = new ()
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = builder.Configuration["Jwt:ValidIssuer"],
            ValidAudience = builder.Configuration["Jwt:ValidIssuer"],
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Secret"]))
        };
    });

builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(options =>
    {
        options.AccessDeniedPath = new PathString("/Account/AccessDenied");
        options.Cookie.Name = "Cookie2";
        options.Cookie.HttpOnly = true;
        options.ExpireTimeSpan = TimeSpan.FromMinutes(720);
        options.LoginPath = new PathString("/Login");
        options.ReturnUrlParameter = CookieAuthenticationDefaults.ReturnUrlParameter;
        options.SlidingExpiration = true;
        
    });


builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("RequireAdministratorRole",
         policy => policy.RequireRole("Administrator"));
});


//builder.Services.AddAuthorization(options =>
//{
//    options.AddPolicy("AtLeast21", policy =>
//        policy.Requirements.Add(new MinimumAgeRequirement(21)));
//});

var app = builder.Build();

app.Logger.LogInformation("also another messag");

ILogger logger = app.Services.GetService<ILogger<Program>>();

logger.LogInformation("ATSRYTS");

app.UseHttpLogging();

// app.UseJwtBearerAuthentication(new JwtBearerOptions()
// {
//     Audience = "http://localhost:5001/", 
//     Authority = "http://localhost:5000/", 
//     AutomaticAuthenticate = true
// });





// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();

app.UseAuthorization();

app.UseSession();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
