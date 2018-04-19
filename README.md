# **Grandmas Recipes**

*An example showing how to build an API Gateway custom authoriser using .NET Core 2.0.*


**Pre-Requisites**

- Install [.NET Core 2.0](https://www.microsoft.com/net/download/)

- Install [Visual Studio](https://www.visualstudio.com/) or [Rider](https://www.jetbrains.com/rider/)

- If you are using Windows, install the [AWS Toolkit](https://aws.amazon.com/visualstudio/) for Visual Studio

**Layout**

/src/UI - This is where the user interface resides. It is a single page application using HTML, CSS and jQuery.

/src/UI/js/recipes.js - This is the JavaScript file that needs modification in order to turn on/off authorisation.

/src/API/RecipesAPI - Backend solution lives here.

**Projects in the Solution**

- Entities: POCOs for EF code first


- DataLayer: EF data context

- RecipesAPI: ASP.NET Web API project for returning recipes from a database. Deploys as a AWS Serverless project.

- Security: Shared project between token issuer and validator, containing constants

- RecipesAPI.Tokens: ASP.NET Web API project that issues JWTs. Deploys as a AWS Serverless project.

- RecipesAPI.Authorizer: AWS Lambda function that validates JWT tokens. 