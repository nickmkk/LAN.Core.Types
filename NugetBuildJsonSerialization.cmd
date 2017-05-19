@echo off

set /p apiKey=Enter API Key:%=%
.nuget\nuget setApiKey %apiKey% -Source https://www.nuget.org/api/v2/package
 
@echo ---------------------------------------------------
.nuget\nuget pack LAN.Core.Types.JsonSerialization\LAN.Core.Types.JsonSerialization.csproj -IncludeReferencedProjects -ExcludeEmptyDirectories -Build -Symbols -Properties Configuration=Release
@echo Finished Building: LAN.Core.Types.JsonSerialization
@echo ---------------------------------------------------

@echo Pushing To Nuget and SymbolSource...
set /p jsonSerializationVersion=Enter LAN.Core.Types.JsonSerialization Package Version:%=%
.nuget\nuget push LAN.Core.Types.JsonSerialization.%jsonSerializationVersion%.nupkg -Source https://www.nuget.org/api/v2/package
@echo ---------------------------------------------------