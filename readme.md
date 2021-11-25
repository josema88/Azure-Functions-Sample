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

This project contains a CRUD for SQL Server DB using functions for each operation. In order to run locally the project you should configure a DB with a simple table that will contains 4 columns.

### Database setup

At your SQL Server instance, create a new DB and create a simple table that will contains 4 columns: id, name, description and enabled. You can use the following SQL Server Script:

```SQL
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Departments](
    [id] [int] IDENTITY(1,1) NOT NULL,
    [name] [varchar](255) NOT NULL,
    [description] [varchar](255) NOT NULL,
    [enabled] [bit] NOT NULL DEFAULT 1
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[Departments] ADD PRIMARY KEY CLUSTERED 
(
    [id] ASC
)WITH (STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
```

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

### Run the project

You can run the project with VS Code. Open the folder with VS Code, go to options panel at the top and click in "Run" then you can select "Start debugging" or "Run without debugging".

For reference check my [sesion in Code Camp 2021 at Youtube](https://youtu.be/vtAKCn82_F0).

## Create Function App and Deploy your Azure function

In order to run your Function in the cloud, you should create an Azure Function App instance. You can create it within Visual Studio code using the Azure Functions plugin.

Once your Function App instance is created, now you can deploy it with Visual Studio Code using the Azure Functions plugin.

For more information you can check the [official documentation](https://docs.microsoft.com/en-us/azure/azure-functions/functions-develop-vs-code?tabs=csharp)

### Connect to DB in your Azure Cloud environment

Go to your Azure App Function in Azure and add an environment variable at the instance's Settings -> Configuration -> Application settings.

- Name: ConnectionString
- Value: < Your Connection String >

After add the connection string, save changes.
