# Degug NuGet

Step by step debug NuGet package

## Step - 1 : In .csproj

Add this two line 

```XML
 <DebugSymbols>true</DebugSymbols>
 <DebugType>full</DebugType>
```
## Step - 2 : Azre Pipeline

Release pipeline is not woring. It's debug pipeline, here is the .yml

```yml
# ASP.NET Core (.NET Framework)
# Build and test ASP.NET Core projects targeting the full .NET Framework.
# Add steps that publish symbols, save build artifacts, and more:
# https://docs.microsoft.com/azure/devops/pipelines/languages/dotnet-core

trigger: none

pool:
  vmImage: 'windows-2022'

variables:
  solution: '**/*.sln'
  buildPlatform: 'Any CPU'
  buildConfiguration: 'Release'

steps:
- task: NuGetToolInstaller@1

- task: NuGetCommand@2
  inputs:
    feedsToUse: config
    nugetConfigPath: 'nuget.config'
    restoreSolution: '$(solution)'

- task: DotNetCoreCLI@2
  displayName: 'Build'
  inputs:
    command: 'build'
    projects: '**/*.csproj'

- task: DotNetCoreCLI@2
  displayName: 'Package'
  inputs:
    command: 'pack'
    packagesToPack: '**/*.csproj'
    includesymbols: true
    includesource: true
    versioningScheme: 'off'

- task: PublishSymbols@2
  displayName: "Index Symbols in azure devops"
  inputs:
    SearchPattern: '**/bin/**/*.pdb'
    SymbolServerType: 'TeamServices'
    TreatNotIndexedAsWarning: true
    IndexSources: true
    PublishSymbols: true
    SymbolsArtifactName: 'Symbols_$(BuildConfiguration)'

- task: NuGetCommand@2
  displayName: 'Push NuGet'
  inputs:
    command: 'push'
    packagesToPush: '$(Build.ArtifactStagingDirectory)/**/*.nupkg;!$(Build.ArtifactStagingDirectory)/**/*.symbols.nupkg'
    nuGetFeedType: 'internal'
    publishVstsFeed: '82b48f98-1136-426a-9297-1ed9e9c77643/a4df63e7-7dca-4791-8818-cc315dcdccac'
```

## Step - 3: Set up Visual Studio

Setup azure artifact symbol server in Visual Studio

Visual Studio -> Tools -> Debugging -> Symbols

More Details: https://docs.microsoft.com/en-us/azure/devops/pipelines/artifacts/symbols?view=azure-devops#set-up-visual-studio
