# Degug NuGet

Step by step debug NuGet package

## Step - 1 : Azre Pipeline

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

- task: VSBuild@1
  inputs:
    solution: '$(solution)'
    msbuildArgs: '/p:DeployOnBuild=true /p:WebPublishMethod=FileSystem /p:publishUrl="$(build.artifactstagingdirectory)" /p:PackageAsSingleFile=false /p:SkipInvalidConfigurations=true'
    platform: '$(buildPlatform)'
    configuration: '$(buildConfiguration)'

- task: ArchiveFiles@2
  displayName: 'Archive task'
  inputs:
    rootFolderOrFile: '$(Build.ArtifactStagingDirectory)/'
    includeRootFolder: false
    archiveFile: '$(Build.ArtifactStagingDirectory)/Release/CardSetOnline$(Version).zip'

- task: PublishBuildArtifacts@1    
  displayName: 'Publish Artifact: drop'
  inputs:
    PathtoPublish: '$(build.artifactstagingdirectory)/Release'
```

## Step - 2: Set up Visual Studio

Setup azure artifact symbol swerver in Visual Studio

Visual Studio -> Tools -> Debugging -> Symbols

More Details: https://docs.microsoft.com/en-us/azure/devops/pipelines/artifacts/symbols?view=azure-devops#set-up-visual-studio
