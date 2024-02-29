using Microsoft.EntityFrameworkCore;
using RESTWithNET8.Models.Context;
using RESTWithNET8.Businesses;
using RESTWithNET8.Businesses.Implementations;
using RESTWithNET8.Repositories;
using EvolveDb;
using MySqlConnector;
using Serilog;
using RESTWithNET8.Repositories.Generic.Implementation;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

var connection = builder.Configuration["MySQLConnection:MySQLConnectionString"];
builder.Services.AddDbContext<MySQLContext>(options => options.UseMySql(connection, new MySqlServerVersion(new Version(8, 0, 36))));

if (builder.Environment.IsDevelopment())
{
    MigrateDatabase(connection);
}

// API versioning
builder.Services.AddApiVersioning();

// Dependency injection
builder.Services.AddScoped<IPersonBusiness, PersonBusinessImplementation>();
builder.Services.AddScoped<IBookBusiness, BookBusinessImplementation>();
builder.Services.AddScoped(typeof(IRepository<>), typeof(GenericRepository<>));

var app = builder.Build();

// Configure the HTTP request pipeline.

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();

void MigrateDatabase(string connection)
{
    try
    {
        var evolveConnection = new MySqlConnection(connection);

        var evolve = new Evolve(evolveConnection, Log.Information)
        {
            Locations = new List<string> { "Database/Migrations", "Database/Datasets" },
            IsEraseDisabled = true,
        };

        evolve.Migrate();
    }
    catch (Exception ex)
    {
        Log.Error("Database migration failed", ex);
        throw;
    }
}