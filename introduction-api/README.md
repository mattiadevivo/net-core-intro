# Introduction API

## Project Setup

- Create API project
- add EF package `dotnet add package Microsoft.EntityFrameworkCore.InMemory`

## NuGet Packages

We can add NuGet packages with:
- dotnet cli
- using Visual Studio GUI

## Dotnet cli

- install package with `dotnet add package <PACKAGE_NAME> --version <VERSION>`
- check that package has been installed inside `<project-name>.csproj` file
- you can list the package references for the project using `dotnet list package`
- remove a package using `dotnet remove package <PACKAGE_NAME>`
- to restore packages listed in project file use `dotnet restore` (automatically
done using `dotnet build` and `dotnet run` commands)

## Entity Framework Core

In EFCore data access is performed using a model which is made up of entity classes
and a context object that represents a session with the database. We can also
query and save data using models.

You need then to add a *provider* for the desired sql server you want to use,
for example for SQLServer `dotnet add Microsoft.EntityFrameworkCore.SqlServer`.

For adding a **controller**, we can mark the class with `[ApiController]` attribute,
this attribute means that the controller reponds to web API requests. Then we can
use Dependency Injection to inject the dataabase context into the controller, this
context will be used in each of the **CRUD** methods in the controller.

**Routing** is done by adding `[Route("/api/[controller]")]` to the controller
class, this will:
- replace `[controller]` with the name of the controller minus the *Controller*
suffix
- if the `[HttpGet]` attribute has a route template like `[HttpGet("products)]`
appent that to the path
- `[HttpGet("{}id")]` had `{id}` as a placeholder variable for the id.

**Return types** when is `ActionResult<T>` then ASP.NET Core automatically
serializes the object to JSON and writes JSON into the body of the message.

## TODO

- Placeholder in db configuration string, set Password with env var
- create migrations
- implement other CRUD methods
- implement unit tests