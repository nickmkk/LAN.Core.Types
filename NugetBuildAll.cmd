@echo off
Setlocal EnableDelayedExpansion

set /p apiKey=Enter API Key:%=%
.nuget\nuget setApiKey %apiKey% -source https://www.nuget.org

SET /p deployCoreTypes=Would you like to deploy the LAN.Core.Types Project? (y/n) %=%
IF (!deployCoreTypes!) EQU (y) (
    .nuget\nuget pack LAN.Core.Types\LAN.Core.Types.csproj -IncludeReferencedProjects -ExcludeEmptyDirectories -Build -Symbols -Properties Configuration=Release
    @echo Finished Building: LAN.Core.Types
    set /p coreTypesVersion=Enter LAN.Core.Types Package Version:%=%
    .nuget\nuget push LAN.Core.Types.!coreTypesVersion!.nupkg -Source https://www.nuget.org/api/v2/package
)

@echo ---------------------------------------------------
SET /p deployBsonSerialization=Would you like to deploy the LAN.Core.Types.BsonSerialization Project? (y/n) %=%
IF (!deployBsonSerialization!) EQU (y) (
    .nuget\nuget pack LAN.Core.Types.BsonSerialization\LAN.Core.Types.BsonSerialization.csproj -IncludeReferencedProjects -ExcludeEmptyDirectories -Build -Symbols -Properties Configuration=Release
    @echo Finished Building: LAN.Core.Types.BsonSerialization
    set /p bsonSerializationVersion=Enter LAN.Core.Types.BsonSerialization Package Version:%=%
    .nuget\nuget push LAN.Core.Types.BsonSerialization.!bsonSerializationVersion!.nupkg -Source https://www.nuget.org/api/v2/package
)

@echo ---------------------------------------------------
SET /p deployJsonSerialization=Would you like to deploy the LAN.Core.Types.JsonSerialization Project? (y/n) %=%
IF (!deployJsonSerialization!) EQU (y) (
    .nuget\nuget pack LAN.Core.Types.JsonSerialization\LAN.Core.Types.JsonSerialization.csproj -IncludeReferencedProjects -ExcludeEmptyDirectories -Build -Symbols -Properties Configuration=Release
    @echo Finished Building: LAN.Core.Types.JsonSerialization
    set /p jsonSerializationVersion=Enter LAN.Core.Types.JsonSerialization Package Version:%=%
    .nuget\nuget push LAN.Core.Types.JsonSerialization.!jsonSerializationVersion!.nupkg -Source https://www.nuget.org/api/v2/package
)
