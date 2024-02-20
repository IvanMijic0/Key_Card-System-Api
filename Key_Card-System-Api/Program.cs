using Keycard_System_API.Data;
using Keycard_System_API.Repositories;
using Keycard_System_API.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using StackExchange.Redis;
using System;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var services = builder.Services;

// Configure Redis
// Adjust connection string as needed
//services.AddSingleton<IConnectionMultiplexer>(ConnectionMultiplexer.Connect("localhost"));

/*services.AddStackExchangeRedisCache(options =>
{
    options.Configuration = "localhost"; // Adjust connection string as needed
    options.InstanceName = "KeyCard-Redis-"; // Optional
});*/

// Configure DbContext
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
services.AddDbContext<ApplicationDbContext>(options =>
    options.UseMySQL(connectionString));

// Register repositories
services.AddScoped<IUserRepository, UserRepository>();

services.AddScoped<IRoomRepository, RoomRepository>();

services.AddScoped<ILogRepository, LogRepository>();

// Register services
services.AddScoped<IUserService, UserService>();

services.AddScoped<IRoomService, RoomService>();

services.AddScoped<ILogService, LogService>();

services.AddCors(options =>
{
    options.AddDefaultPolicy(
               builder =>
               {
            builder.WithOrigins("http://localhost:3000")
                .AllowAnyHeader()
                .AllowAnyMethod();
        });
});

// Configure JWT Authentication
var jwtKey = "E8TmjOvUoSMkvbvw3nU7nMps1T+8W+mBc9s+7/X9SG0=";
services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = false,
            ValidateAudience = false,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(jwtKey))
        };
    });

// Add controllers and other services
services.AddControllers();
services.AddEndpointsApiExplorer();
services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "KeyCard-Api", Version = "v1" });
    // Add JWT Authentication to Swagger
    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Description = "JWT Authorization header using the Bearer scheme",
        Type = SecuritySchemeType.Http,
        Scheme = "bearer"
    });
    c.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            Array.Empty<string>()
        }
    });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Your API V1"));
}

app.UseHttpsRedirection();

app.UseAuthentication(); // Authentication setup
app.UseAuthorization(); // Authorization setup goes here
app.UseCors("AllowSpecificOrigin"); // Cors setup

app.MapControllers();

app.Run();
