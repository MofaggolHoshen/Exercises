# Deploy to IIS

## .Net 5 Web app

We need to follow two steps

  Step 1: Create build pipeline with yml or clasic (UI) in Azure  
  Step 2: Create release pipeline

Step 1: Create YAML file

```yml
# ASP.NET Core (.NET Framework)
# Build and test ASP.NET Core projects targeting the full .NET Framework.
# Add steps that publish symbols, save build artifacts, and more:
# https://docs.microsoft.com/azure/devops/pipelines/languages/dotnet-core

trigger: none

pool:
  vmImage: 'windows-latest'

variables:
  solution: '**/*.sln'
  buildPlatform: 'Any CPU'
  buildConfiguration: 'Release'

steps:
- task: NuGetToolInstaller@1

- task: NuGetCommand@2
  inputs:
    restoreSolution: '$(solution)'

# Publish will build first, we don't need build command
- task: DotNetCoreCLI@2
  inputs:
    command: publish
    publishWebProjects: True
    arguments: '--configuration $(BuildConfiguration) --output $(Build.ArtifactStagingDirectory)'
    zipAfterPublish: True

- task: PublishBuildArtifacts@1
  inputs:
    PathtoPublish: '$(Build.ArtifactStagingDirectory)'
    ArtifactName: 'drop'
```

Read more about yml file for [.Net 5](https://docs.microsoft.com/azure/devops/pipelines/languages/dotnet-core) [here](https://docs.microsoft.com/en-us/azure/devops/pipelines/apps/cd/deploy-webdeploy-iis-deploygroups?view=azure-devops)

Step 2: Create release pipeline

* Create Deployment Group

1. ![Postman request](https://github.com/MofaggolHoshen/Exercises/blob/master/Pictures/FileUploadPostmanRequest.png)
