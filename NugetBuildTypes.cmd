@echo off

set /p apiKey=Enter API Key:%=%
.nuget\nuget setApiKey %apiKey%

@echo ---------------------------------------------------
.nuget\nuget pack LAN.Core.Types\LAN.Core.Types.csproj -IncludeReferencedProjects -ExcludeEmptyDirectories -Build -Symbols -Properties Configuration=Release
@echo Finished Building: LAN.Core.Types
@echo ---------------------------------------------------

@echo Pushing To Nuget and SymbolSource...
set /p typesVersion=Enter LAN.Core.Types Package Version:%=%
.nuget\nuget push LAN.Core.Types.%typesVersion%.nupkg
@echo ---------------------------------------------------