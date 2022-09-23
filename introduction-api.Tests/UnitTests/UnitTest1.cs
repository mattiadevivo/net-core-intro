using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Moq;

namespace introduction_api.Tests.UnitTests;

public class TodoItemControllerTests
{
    private readonly TestServer _server;
    private readonly HttpClient _client;

    public TodoItemControllerTests()
    {
        _server = new TestServer(new WebHostBuilder().UseStartup<Startup>());
        _client = _server.CreateClient();
    }

    [Fact]
    public async Task GetTodoListSuccess()
    {
        var mockLogger = new Mock<ILogger>();
        var mockDbContext = new Mock<DbContext>();

    }

    [Fact]
    public void Test1()
    {

    }
}