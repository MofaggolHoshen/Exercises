# Deploy to IIS

To read more about Azure pipeline [task](https://docs.microsoft.com/en-us/azure/devops/pipelines/tasks/?view=azure-devops)

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

1. Go to Deployment group -> New ![DeploymentGroup](https://github.com/MofaggolHoshen/Exercises/blob/master/ThinkThrough/AzureCICDPipline/DeployToVMIIS/DeploymentGroup.PNG)

2. Copy the Script and run in virtual machine command line tool (Administrator) ![DeploymentGroup](https://github.com/MofaggolHoshen/Exercises/blob/master/ThinkThrough/AzureCICDPipline/DeployToVMIIS/DeploymentGroup2.PNG)

* Create release pipline

1. Go to Release -> New -> Start with 'Empty job'

2. Artiface, select azure artifact

3. Remove all the job from Stage, or what ervet the name

4. Select 'Add deployment group job', follow the picture ![Release](https://github.com/MofaggolHoshen/Exercises/blob/master/ThinkThrough/AzureCICDPipline/DeployToVMIIS/ReleasePipline.png)

5. Select 'Deploy IIS', follow picture ![Release](https://github.com/MofaggolHoshen/Exercises/blob/master/ThinkThrough/AzureCICDPipline/DeployToVMIIS/ReleasePipline2.PNG)
