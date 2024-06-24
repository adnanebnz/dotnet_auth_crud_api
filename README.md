# DotNetApi Project

## Overview

This project is a .NET Core API designed to demonstrate various core functionalities including user authentication, database operations, and CRUD operations on a Person model. It utilizes Entity Framework Core for database interactions and JWT for authentication.

### Features

- Authentification (Login and Register) using JWT (Json Web Tokens)
- Crud operations on Persons

## Getting Started

### Prerequisites

- .NET 8.0 SDK
- MySQL Server

### Configuration

Before running the project, ensure you have the correct connection strings and JWT settings configured in [`appsettings.json`](command:_github.copilot.openRelativePath?%5B%7B%22scheme%22%3A%22file%22%2C%22authority%22%3A%22%22%2C%22path%22%3A%22%2Fc%3A%2FUsers%2Fskill%2FDesktop%2FDotNetApi%2Fappsettings.json%22%2C%22query%22%3A%22%22%2C%22fragment%22%3A%22%22%7D%5D "c:\\Users\skill\Desktop\DotNetApi\appsettings.json") and [`appsettings.Development.json`](command:_github.copilot.openRelativePath?%5B%7B%22scheme%22%3A%22file%22%2C%22authority%22%3A%22%22%2C%22path%22%3A%22%2Fc%3A%2FUsers%2Fskill%2FDesktop%2FDotNetApi%2Fappsettings.Development.json%22%2C%22query%22%3A%22%22%2C%22fragment%22%3A%22%22%7D%5D "c:\\Users\skill\Desktop\DotNetApi\appsettings.Development.json").

```json
{
  "ConnectionStrings": {
    "DefaultConnection": "server=localhost;port=3306;database=dotnetdb;user=root;password="
  },
  "Jwt": {
    "Key": "your_jwt_secret_key",
    "Issuer": "your_issuer",
    "Audience": "your_audience"
  }
}
```

### Building the Project

To build the project, navigate to the project directory in your terminal and run:

```sh
dotnet build
```

This command compiles the project and its dependencies.

### Running the Project

After building the project, you can run it using:

```sh
dotnet run --project DotNetApi.csproj
```

This command starts the API server. By default, it will listen on `http://localhost:5000` and `https://localhost:5001`.

### Testing the Project

To test the API endpoints, you can use the provided `DotNetApi.http` file if you are using Visual Studio Code with the REST Client extension. Alternatively, you can use any API testing tool like Postman.

## Project Structure

- `Controllers/` - Contains the API controllers.
- `Db/` - Contains the Entity Framework DbContext class.
- `Models/` - Contains the entity models.
- `Program.cs` - Entry point for the API.
- `appsettings.json` and `appsettings.Development.json` - Configuration files.
