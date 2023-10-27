using HealthChecks.UI.Client;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using SalonManager.Application.Configurations;
using SalonManager.Application.IoC;
using SalonManager.Domain.Entities;
using SalonManager.Infra.Data.Context;
using Serilog;
using Serilog.Events;
using System.Text;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRegisterServices();

builder.Services.AddApiVersioning(x =>
{
    x.DefaultApiVersion = new ApiVersion(1, 0);
    x.ReportApiVersions = true;
    x.AssumeDefaultVersionWhenUnspecified= true;
});

builder.Services.AddControllers().AddJsonOptions(x =>
                x.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles);


//builder.Services.AddHealthChecks()
//    .AddSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"), tags: new[]
//    {
//        "database"
//    }); //HealthCheck SqlServer

builder.Services.AddHealthChecks(); //HealthCheck

builder.Services.AddHealthChecksUI()
    .AddInMemoryStorage(); //HealthCheck InMemoryDb

builder.Host.UseSerilog((hostBuilderContext, loggerConfiguration) => loggerConfiguration
    .WriteTo.Console(LogEventLevel.Debug));
//.WriteTo.File("log.txt",
//LogEventLevel.Warning,
//rollingInterval: RollingInterval.Day));


var tokenConfigurations = new TokenConfiguration();
var tokenConfigurationString = builder.Configuration.GetSection("TokenConfigurations");

new ConfigureFromConfigurationOptions<TokenConfiguration>(
        tokenConfigurationString
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

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors(
    options => options
    .AllowAnyOrigin()
    .AllowAnyHeader()
    .AllowAnyMethod()
    );

app.MapHealthChecks("/health", new HealthCheckOptions
{
    ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse
}); //HealthCheck

app.MapHealthChecksUI(); //HealthCheck

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
