# Azure Functions with Azure Core Tools

This repo contains a sample project created with Azure Core Tools and Visual Studio Code.

## Requirements

- [Install Azure CLI](https://docs.microsoft.com/en-us/cli/azure/install-azure-cli)
- [Install .Net Core 3.1](https://dotnet.microsoft.com/learn/dotnet/hello-world-tutorial/install)
- [Install Azure Functions Core Tools](https://docs.microsoft.com/en-us/azure/azure-functions/functions-run-local)
- [Install Visual Studio Code](https://code.visualstudio.com/)
- [Install Azure Functions VS Code extension](https://marketplace.visualstudio.com/items?itemName=ms-azuretools.vscode-azurefunctions)
- [Install C# VS Code Extension](https://marketplace.visualstudio.com/items?itemName=ms-dotnettools.csharp)

## Functions Project

This project contains a CRUD for SQL Server DB using functions for each operation. In order to run locally the project you should configure a DB with a simple table that will contains 2 columns: id (integer and auto incremental) and nombre (varchar).

### Connect to DB in your local environment

Be sure that in your local environment you have the fille local.settings.json. Within that file you should add the connection string, like this sample:

```json
{
  "IsEncrypted": false,
  "Values": {
    "AzureWebJobsStorage": "",
    "FUNCTIONS_WORKER_RUNTIME": "dotnet"
  },
  "ConnectionString": "<Your Connection String>"
}
```

### Connect to DB in your Azure Cloud environment

Go to your Azure App Function in Azure and add an environment variable at the instance's Settings -> Configuration -> Application settings.

- Name: ConnectionString
- Value: < Your Connection String >

After add the connection string, save changes.

### Run the project

You can run the project with VS Code. Open the folder with VS Code, go to options panel at the top and click in "Run" then you can select "Start debugging" or "Run without debugging".

For reference check my [sesion in Code Camp 2021 at Youtube](https://youtu.be/vtAKCn82_F0).
