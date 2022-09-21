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

### Tools

`dotnet new tool-manifest` creates a new tool manifest named `dotnet-tools.json`
 for the project inside `.config` directory of the project.

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

### EFCore Migrations

For development purposes you can use [Create and drop APIs](https://learn.microsoft.com/en-us/ef/core/managing-schemas/ensure-created)
but for production env is better we need to use **EF Core command-line tools**.

Install *dotnet ef* tool with `dotnet ef migrations add InitialCreate`, this can
be done globally or locally:
```bash
# Globally
dotnet tool install --global dotnet-ef

# Locally
dotnet new tool-manifest # create the manifest of tools needed by the app
dotnet tool install <tool> # install the tool
dotnet tool restore # used by other devs to install needed local tools
dotnet tool list # list all the tools in the manifest
dotnet tool run <tool># run local tool # or
dotnet <tool>
dotnet tool uninstall <tool> # remove the tool

# For both
dotnet add package Microsoft.EntityFrameworkCore.Design # package needed for ef tool
```

- Create first migration `dotnet dotnet-ef migrations add InitialCreate`, this
will create a directory called **Migrations** inside the project.
- Create db and schema from the migration `dotnet dotnet-ef database update`
- after we do changes in our models we can apply them with
`dotnet dotnet-ef migrations add <AddedNewThingsMessage` then apply migration
with `dotnet dotnet-ef database update`
- `dotnet dotnet-ef migrations list` list all existing migrations
- `dotnet dotnet-ef migrations remove` to remove the migration

**To specify the configuration** that migration tool need to use pass
`--configuration Development` to the cli commands.

**To run the migration remember to temporary add sensitive info to
the connectionStrin**.


## TODO

- [*] Placeholder in db configuration string, set Password with env var
- [*] implement other CRUD methods
- create migrations
- implement unit tests
- [publish to Azure](https://learn.microsoft.com/en-us/azure/app-service/quickstart-dotnetcore)