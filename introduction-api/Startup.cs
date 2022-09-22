using IdentityServer4.AccessTokenValidation;
using introduction_api.Configuration;
using introduction_api.Models;
using Microsoft.EntityFrameworkCore;

public class Startup
{
    public IConfiguration Configuration
    {
        get;
    }
    public Startup(IConfiguration configuration)
    {
        Configuration = configuration;
    }
    /// <summary>
    /// Register dependencies and services
    /// </summary>
    /// <param name="services"></param>
    /// <param name="dbPassword"></param>
    public void ConfigureServices(IServiceCollection services, string? dbPassword)
    {
        string connectionStringTemplate = Configuration.GetConnectionString("DefaultConnection");
        string connectionString = String.Format(connectionStringTemplate, dbPassword);

        // Add services to the container.
        services.AddControllers();
        // Add Db Context to all the controllers via DI
        services.AddDbContext<TodoContext>(ops => ops.UseSqlServer(connectionString));
        // Add Authorization server via DI
        services.AddAuthentication(IdentityServerAuthenticationDefaults.AuthenticationScheme).
            AddIdentityServerAuthentication(options =>
            {
                // Load settings from appsettingss
                var settings = Configuration.GetSection("IdentityServer").
                    Get<IdentiyServerSettings>();
                options.Authority = settings.AuthorizationServerBaseUrl;
                options.ApiName = settings.ApiName; // API Resource Id
                options.RequireHttpsMetadata = false; // only for development
                options.EnableCaching = true;
                options.CacheDuration = TimeSpan.FromMinutes(10); // that's the default 
            });
        // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
        services.AddEndpointsApiExplorer();
        services.AddSwaggerGen();
    }
    /// <summary>
    /// Register middlewares
    /// </summary>
    /// <param name="app"></param>
    /// <param name="env"></param>
    public void Configure(WebApplication app, IWebHostEnvironment env)
    {
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
    }
}