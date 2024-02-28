using Microsoft.EntityFrameworkCore;
using RESTWithNET8.Models.Context;
using RESTWithNET8.Businesses;
using RESTWithNET8.Businesses.Implementations;
using RESTWithNET8.Repositories;
using RESTWithNET8.Repositories.Implementations;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

var connection = builder.Configuration["MySQLConnection:MySQLConnectionString"];
builder.Services.AddDbContext<MySQLContext>(options => options.UseMySql(connection, new MySqlServerVersion(new Version(8, 0, 36))));

// API versioning
builder.Services.AddApiVersioning();

// Dependency injection
builder.Services.AddScoped<IPersonBusiness, PersonBusinessImplementation>();
builder.Services.AddScoped<IPersonRepository, PersonRepositoryImplementation>();

var app = builder.Build();

// Configure the HTTP request pipeline.

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
