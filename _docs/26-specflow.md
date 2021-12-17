# SpecFlow

## Default directory structure

Drivers
    Driver pattern (https://docs.specflow.org/projects/specflow/en/latest/Guides/DriverPattern.html)
    Basic offload the detail mechanics to another class that does the test execution and return result.
    So as to simplify the test (because we only test results).


Features
StepDefinitions
Support


## LivingDoc


dotnet tool install --global SpecFlow.Plus.LivingDoc.CLI
dotnet tool update --global SpecFlow.Plus.LivingDoc.CLI
dotnet tool uninstall --global SpecFlow.Plus.LivingDoc.CLI


livingdoc -h|--help
livingdoc --version
livingdoc <COMMAND> [-h|--help] [command-options] [arguments]
