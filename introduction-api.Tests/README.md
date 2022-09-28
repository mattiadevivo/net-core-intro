# Tests with Xunit

## Unit Tests

Test are written using **xunit** tool and using an **In Memory Database (Microsoft
)

## Integration/Functional Tests

For Integration Tests ASP.NET Core offers a NuGet package `Microsoft.AspNetCore.TestHost` that
can be used to create a **test host**. When you need to test things you can
instanciate controllers by using **testing host**.

## Moq

[Moq Getting Started](https://github.com/Moq/moq4/wiki/Quickstart)

## Testing EF Core Applications

[Strategies](https://learn.microsoft.com/en-us/ef/core/testing/)
[EF Core in-memory-db](https://stackoverflow.com/questions/54219742/mocking-ef-core-dbcontext-and-dbset)

[Complete Example With Repository Pattern](https://www.infoworld.com/article/3672154/how-to-use-ef-core-as-an-in-memory-database-in-asp-net-core-6.html)

## FluentAssertions

[Fluent Assertions HomePage](https://fluentassertions.com/)

## Project creation

- Create test project using `dotnet new xunit -o introduction-api.Tests`
(new project inside `introduction-api.Tests` directory using *xUnit* as test
library and adds some Packages (Microsoft.NET.Test.Sdk, xunit, xunit.runner.visualstudio, coverlet.collector))
- Add *test project* to the solution file `dotnet sln add ./introduction-api.Tests/introduction-api.Tests.csproj`
- Add *introduction-api class library* as a dependency of
*introduction-api.Tests* `dotnet add ./introduction-api.Tests/introduction-api.Tests.csproj reference ./introduction-api/introduction-api.csproj`

## Run

Run tests with `dotnet test` command.
