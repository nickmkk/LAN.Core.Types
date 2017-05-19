@echo off

set /p apiKey=Enter API Key:%=%
.nuget\nuget setApiKey %apiKey% -Source https://www.nuget.org/api/v2/package

@echo ---------------------------------------------------
.nuget\nuget pack LAN.Core.Types.BsonSerialization\LAN.Core.Types.BsonSerialization.csproj -IncludeReferencedProjects -ExcludeEmptyDirectories -Build -Symbols -Properties Configuration=Release
@echo Finished Building: LAN.Core.Types.BsonSerialization
@echo ---------------------------------------------------

@echo Pushing To Nuget and SymbolSource...
set /p bsonSerializationVersion=Enter LAN.Core.Types.BsonSerialization Package Version:%=%
.nuget\nuget push LAN.Core.Types.BsonSerialization.%bsonSerializationVersion%.nupkg -Source https://www.nuget.org/api/v2/package
@echo ---------------------------------------------------
