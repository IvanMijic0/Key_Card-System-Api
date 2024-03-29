using Key_Card_System_Api.configuration;
using Key_Card_System_Api.Repositories.KeycardRepository;
using Key_Card_System_Api.Repositories.LogRepositroy;
using Key_Card_System_Api.Repositories.NotificationRepository;
using Key_Card_System_Api.Repositories.RoomRepository;
using Key_Card_System_Api.Repositories.UserRepository;
using Key_Card_System_Api.Services.EmailService;
using Key_Card_System_Api.Services.KeycardService;
using Key_Card_System_Api.Services.LogService;
using Key_Card_System_Api.Services.NotificationService;
using Key_Card_System_Api.Services.RoomService;
using Key_Card_System_Api.Services.UserService;
using Keycard_System_API.Data;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using StackExchange.Redis;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var services = builder.Services;

// Add SignalR service
services.AddSignalR();

string? connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

// Register EmailService
services.AddTransient<EmailService>(provider =>
{
    // Configure SMTP server details
    string smtpServer = builder.Configuration["SmtpSettings:Server"] ?? string.Empty;
    int smtpPort = Convert.ToInt32(builder.Configuration["SmtpSettings:Port"]);
    string smtpUsername = builder.Configuration["SmtpSettings:Username"] ?? string.Empty;
    string smtpPassword = builder.Configuration["SmtpSettings:Password"] ?? string.Empty;

    return new EmailService(smtpServer, smtpPort, smtpUsername, smtpPassword);
});

services.AddSingleton<IConnectionMultiplexer>(sp =>
{
    var redisUrl = "localhost";

    var options = ConfigurationOptions.Parse(redisUrl);

    return ConnectionMultiplexer.Connect(options);
});


if (connectionString != null)
{
    services.AddDbContext<ApplicationDbContext>(options =>
        options.UseMySQL(connectionString));
}

// Register repositories
services.AddScoped<IUserRepository, UserRepository>();

services.AddScoped<IKeycardRepository, KeycardRepository>();

services.AddScoped<IRoomRepository, RoomRepository>();

services.AddScoped<ILogRepository, LogRepository>();

services.AddScoped<INotificationRepository, NotificationRepository>();

// Register services
services.AddScoped<IUserService, UserService>();

services.AddScoped<IKeycardService, KeycardService>();

services.AddScoped<IRoomService, RoomService>();

services.AddScoped<ILogService, LogService>();

services.AddScoped<INotificationService, NotificationService>();

services.AddCors(options =>
{
    options.AddPolicy("AllowSpecificOrigin",
        builder =>
        {
            builder.WithOrigins("http://localhost:3000")
                .AllowAnyHeader()
                .AllowAnyMethod();
        });
});

// Configure JWT Authentication
string jwtKey = builder.Configuration["JwtSettings:Key"] ?? "DefaultJwtKey";

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

var port = Environment.GetEnvironmentVariable("PORT") ?? "5000"; // Get port from environment variable or use default 5000

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

// Map the SignalR endpoint
app.MapHub<MyHub>("/myhub");

app.Run($"http://0.0.0.0:{port}"); // Listen on all interfaces
