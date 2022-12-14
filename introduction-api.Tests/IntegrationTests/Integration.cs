using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Moq;
using introduction_api.Models;

namespace introduction_api.Tests.IntegrationTests;

public class TodoItemControllerTests
{
    private readonly TestServer _server;
    private readonly HttpClient _client;

    public TodoItemControllerTests()
    {
        _server = new TestServer(new WebHostBuilder().UseStartup<Startup>());
        _client = _server.CreateClient();
    }

    // TODO Integration tests
}