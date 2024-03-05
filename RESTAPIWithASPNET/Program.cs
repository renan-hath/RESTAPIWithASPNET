using Microsoft.EntityFrameworkCore;
using RESTWithNET8.Models.Context;
using RESTWithNET8.Businesses;
using RESTWithNET8.Businesses.Implementations;
using RESTWithNET8.Repositories;
using EvolveDb;
using MySqlConnector;
using Serilog;
using System.Net.Http.Headers;
using RESTWithNET8.Hypermedia.Filters;
using RESTWithNET8.Hypermedia.Enricher;
using Microsoft.OpenApi.Models;
using Microsoft.AspNetCore.Rewrite;
using RESTWithNET8.Services;
using RESTWithNET8.Services.Implementation;
using RESTWithNET8.Repositories.Implementation;
using RESTWithNET8.Configurations;
using Microsoft.Extensions.Options;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection.Extensions;

var builder = WebApplication.CreateBuilder(args);

var appName = "REST API - From 0 to Azure with ASP.NET 8 and Docker";
var appVersion = "v1";
var urlName = $"{appName} - {appVersion}";
var appDescription = $"RESTful API developed in course '{appName}'";

// Add services to the container.

builder.Services.AddRouting(options => options.LowercaseUrls = true);

var tokenConfigurations = new TokenConfiguration();
new ConfigureFromConfigurationOptions<TokenConfiguration>(
    builder.Configuration.GetSection("TokenConfigurations")
    )
    .Configure(tokenConfigurations);

builder.Services.AddSingleton(tokenConfigurations);

builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = tokenConfigurations.Issuer,
        ValidAudience = tokenConfigurations.Audience,
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(tokenConfigurations.Secret))
    };
});

builder.Services.AddAuthorization(auth =>
{
    auth.AddPolicy("Bearer", new AuthorizationPolicyBuilder()
        .AddAuthenticationSchemes(JwtBearerDefaults.AuthenticationScheme)
        .RequireAuthenticatedUser().Build());
});


builder.Services.AddCors(options => options.AddDefaultPolicy(
                         builder => 
                         { 
                             builder.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader(); 
                         }));

builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1",
        new OpenApiInfo
        {
            Title = appName,
            Version = appVersion,
            Description = appDescription,
            Contact = new OpenApiContact
            {
                Name = "Renan Hatherly",
                Url = new Uri("https://github.com/renan-hath")
            }
        });
});

var connection = builder.Configuration["MySQLConnection:MySQLConnectionString"];
builder.Services.AddDbContext<MySQLContext>(options => options.UseMySql(connection, new MySqlServerVersion(new Version(8, 0, 36))));

if (builder.Environment.IsDevelopment())
{
    MigrateDatabase(connection);
}

builder.Services.AddMvc(options =>
{
    options.RespectBrowserAcceptHeader = true;

    options.FormatterMappings.SetMediaTypeMappingForFormat("xml", "application/xml");
    options.FormatterMappings.SetMediaTypeMappingForFormat("json", "application/json");
})
.AddXmlSerializerFormatters();

var filterOptions = new HyperMediaFilterOptions();
filterOptions.ContentResponseEnricherList.Add(new PersonEnricher());
filterOptions.ContentResponseEnricherList.Add(new BookEnricher());
builder.Services.AddSingleton(filterOptions);

// API versioning
builder.Services.AddApiVersioning();

// Dependency injection
builder.Services.TryAddSingleton<IHttpContextAccessor, HttpContextAccessor>();
builder.Services.AddScoped<IPersonBusiness, PersonBusinessImplementation>();
builder.Services.AddScoped<IBookBusiness, BookBusinessImplementation>();
builder.Services.AddScoped<ILoginBusiness, LoginBusinessImplementation>();
builder.Services.AddScoped<IFileBusiness, FileBusinessImplementation>();
builder.Services.AddScoped(typeof(IRepository<>), typeof(GenericRepository<>));
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IPersonRepository, PersonRepository>();
builder.Services.AddTransient<ITokenService, TokenService>();

var app = builder.Build();

// Configure the HTTP request pipeline.

app.UseHttpsRedirection();

app.UseCors();

app.UseSwagger();
app.UseSwaggerUI(c => { c.SwaggerEndpoint("/swagger/v1/swagger.json", urlName); });
var option = new RewriteOptions();
option.AddRedirect("^$", "swagger");
app.UseRewriter(option);

app.UseAuthorization();

app.MapControllers();

app.MapControllerRoute("DefaultApi", "{controller=values}/v{version=apiVersion}/{id?}");

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