# Notes on options

Assuming

```json Configuration
"Azure": {
	"Storage": {
		"minitools": {
			"ConnectionString": "DefaultEndpointsProtocol=https;AccountName=minitools;AccountKey=<key>"
		}
	}
}
```



```cs
// Set
services.Configure<ConnectionStringSetting>(host.Configuration.GetSection("Azure:Storage:minitools"));

// Get
var setting = host.Services.GetRequiredService<IOptions<ConnectionStringSetting>>();

// Read
Console.WriteLine("setting is [{0}]", setting.Value.ConnectionString);
```


```cs IOptionsSnapshot -- OK if DI in elsewhere; Does not work from root; 
// Set Named options
services.Configure<ConnectionStringSetting>(
    "Azure:Storage:minitools",
    host.Configuration.GetSection("Azure:Storage:minitools"));

//  Cannot resolve scoped service 'Microsoft.Extensions.Options.IOptionsSnapshot`1[MiniTools.HostApp.Models.ConnectionStringSetting]' from root provider
var setting = host.Services.GetRequiredService<IOptionsSnapshot<ConnectionStringSetting>>();
ConnectionStringSetting option = setting.Get("Azure:Storage:minitools");
```

```cs OptionsMonitor

// Set Named options
services.Configure<ConnectionStringSetting>(
    "Azure:Storage:minitools",
    host.Configuration.GetSection("Azure:Storage:minitools"));


var setting = host.Services.GetRequiredService<IOptionsMonitor<ConnectionStringSetting>>();
ConnectionStringSetting option = setting.Get("Azure:Storage:minitools");


```


services.Configure<ConnectionStringSetting>(
    "Azure:Storage:minitools",
    host.Configuration.GetSection("Azure:Storage:minitools"));

//services.Configure<ConnectionStringSetting>(a =>
//{
//    a.ConnectionString = host.Configuration.GetSection("Azure:Storage:minitools:ConnectionString").Value;
//});

services.Configure<ConnectionStringSetting>(host.Configuration.GetSection("Azure:Storage:minitools"));


var setting = host.Services.GetRequiredService<IConfigureNamedOptions<ConnectionStringSetting>>();
ConnectionStringSetting option = new ConnectionStringSetting();
setting.Configure("asd", option);


