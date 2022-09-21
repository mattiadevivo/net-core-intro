using Microsoft.EntityFrameworkCore;
using introduction_api.Models;
using Microsoft.Extensions.Configuration;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

string connectionStringTemplate = builder.Configuration.GetConnectionString("DefaultConnection");
//var password = Environment.GetEnvironmentVariable("MSSQL_PASSWORD"); // This will also work
string password = builder.Configuration.GetValue<String>("MSSQL_PASSWORD");
string connectionString = String.Format(connectionStringTemplate, password);


builder.Services.AddControllers();
builder.Services.AddDbContext<TodoContext>(ops => ops.UseSqlServer(connectionString));
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
    // app.UseSwagger();
    // app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();