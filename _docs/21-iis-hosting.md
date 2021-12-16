# IIS Hosting

Requires `.NET Core Hosting Bundle` on the IIS Server

This can be download from:
https://dotnet.microsoft.com/permalink/dotnetcore-current-windows-runtime-bundle-installer

Without the hosting bundle, IIS will not be able recoginize the `aspNetCore` element in the `web.config` file


```xml:web.config
<?xml version="1.0" encoding="utf-8"?>
<configuration>
  <location path="." inheritInChildApplications="false">
    <system.webServer>
      <handlers>
        <add name="aspNetCore" path="*" verb="*" modules="AspNetCoreModuleV2" resourceType="Unspecified" />
      </handlers>
      <aspNetCore processPath="dotnet" arguments=".\Dn6Poc.DocuMgmtPortal.dll" stdoutLogEnabled="false" stdoutLogFile=".\logs\stdout" hostingModel="inprocess" />
    </system.webServer>
  </location>
</configuration>
<!--ProjectGuid: 6AEAE535-C46D-4D36-BE51-C0648C4E84F7-->
```


# Reference:

https://docs.microsoft.com/en-us/aspnet/core/tutorials/publish-to-iis?view=aspnetcore-6.0&tabs=visual-studio
https://docs.microsoft.com/en-us/aspnet/core/host-and-deploy/iis/web-config?view=aspnetcore-6.0

https://docs.microsoft.com/en-US/troubleshoot/iis/http-error-500-19-webpage
