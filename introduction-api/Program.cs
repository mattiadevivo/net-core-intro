using Microsoft.EntityFrameworkCore;
using introduction_api.Models;
using Microsoft.Extensions.Configuration;
using IdentityServer4.AccessTokenValidation;
using introduction_api.Configuration;

var builder = WebApplication.CreateBuilder(args);

# region Load ENV variables
var password = Environment.GetEnvironmentVariable("MSSQL_PASSWORD");
#endregion

string connectionStringTemplate = builder.Configuration.GetConnectionString("DefaultConnection");
string connectionString = String.Format(connectionStringTemplate, password);

// Add services to the container.
builder.Services.AddControllers();
// Add Db Context to all the controllers via DI
builder.Services.AddDbContext<TodoContext>(ops => ops.UseSqlServer(connectionString)); 
// Add Authorization server via DI (Bearer)
builder.Services.AddAuthentication(IdentityServerAuthenticationDefaults.AuthenticationScheme).
    AddIdentityServerAuthentication(options =>
    {
        // Load settings from appsettingss
        var settings = builder.Configuration.GetSection("IdentityServer").
            Get<IdentiyServerSettings>();
        options.Authority = settings.AuthorizationServerBaseUrl;
        options.ApiName = settings.ApiName; // API Resource Id
        options.RequireHttpsMetadata = false; // only for development
        options.EnableCaching = true;
        options.CacheDuration = TimeSpan.FromMinutes(10); // that's the default 
    });
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();