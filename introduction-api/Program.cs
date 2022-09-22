using Microsoft.EntityFrameworkCore;
using introduction_api.Models;
using Microsoft.Extensions.Configuration;
using IdentityServer4.AccessTokenValidation;
using introduction_api.Configuration;

var builder = WebApplication.CreateBuilder(args);

# region Load ENV variables
var password = Environment.GetEnvironmentVariable("MSSQL_PASSWORD");
#endregion

var startup = new Startup(builder.Configuration);
startup.ConfigureServices(builder.Services, password);
var app = builder.Build();
startup.Configure(app, builder.Environment);
