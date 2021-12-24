# Benchmarking (with Benchmarkdotnet)

## Install R (for plots)

First, make sure Rscript.exe or Rscript is in your path, 
or define an R_HOME environment variable pointing to the R installation directory.


## Create a new project using template


`dotnet new benchmark --config --console-app -n Benchmarks`


```
Benchmark Project (C#)
Author: .NET Foundation and contributors
Description: A project template for creating benchmarks.
Options:

  -b|--benchmarkName  The name of the benchmark class.

                      string - Optional

                      Default: Benchmarks


  -f|--framework      The target framework for the project (e.g. netstandard2.0). Default "net5.0" if "--console-app" is true, "netstandard2.0" if "--console-app" is false
                      string - Optional


  -c|--config         Adds a benchmark config class.

                      bool - Optional

                      Default: false


  --no-restore        If specified, skips the automatic restore of the project on create.

                      bool - Optional

                      Default: false


  --console-app       If specified, the project is set up as console app.

                      bool - Optional

                      Default: false


  -v|--version        Version of BenchmarkDotNet that will be referenced.

                      string - Optional

                      Default: 0.12.1



To see help for other template languages (F#, VB), use --language option:
   dotnet new benchmark -h --language F#
```