# Microsoft.Web.LibraryManager.Cli

Using Microsoft.Web.LibraryManager.Cli

dotnet tool install -g Microsoft.Web.LibraryManager.Cli

libman install @microsoft/signalr@latest -p unpkg -d wwwroot/js/signalr --files dist/browser/signalr.js --files dist/browser/signalr.min.js

What this do:
Use the unpkg provider.
Copy files to the wwwroot/js/signalr destination.
Copy only the specified files.


