param name string
param location string = resourceGroup().location
param tags object = {}

param allowedOrigins array = []
param appServicePlanId string
@secure()
param appSettings object = {}
param serviceName string = 'api'
param storageAccountName string

module api './functions.bicep' = {
  name: '${serviceName}-functions-dotnet-isolated-module'
  params: {
    name: name
    location: location
    tags: union(tags, { 'azd-service-name': serviceName })
    allowedOrigins: allowedOrigins
    alwaysOn: false
    appSettings: appSettings
    appServicePlanId: appServicePlanId
    runtimeName: 'dotnet-isolated'
    runtimeVersion: '8.0'
    storageAccountName: storageAccountName
    scmDoBuildDuringDeployment: false
  }
}

output SERVICE_API_IDENTITY_PRINCIPAL_ID string = api.outputs.identityPrincipalId
output SERVICE_API_NAME string = api.outputs.name
output SERVICE_API_URI string = api.outputs.uri
