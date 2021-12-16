# ASP.NET Configuration

## Inject into Views

### Method 1 

```
@using Microsoft.Extensions.Configuration
@inject IConfiguration Configuration
<link rel="stylesheet" href="@Configuration["MyConfigEntry"]/css/site.min.css">
```

### Method 2 Strongly Typed 

Assuming a strong typed `AppSettings` object:

```
@inject Microsoft.Extensions.Options.IOptions<AppSettings> AppSettingsOptions
```

Or add `@using Microsoft.Extensions.Options` to the `_ViewImports.cshtml` file.

Access value by `@AppSettingsOptions.Value.Version`.



# Reference

https://stackoverflow.com/questions/54376082/how-to-add-configuration-settings-into-layout-cshtml-shared-razor-page/54377211
