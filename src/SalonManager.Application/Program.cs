using HealthChecks.UI.Client;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.AspNetCore.Mvc;
using SalonManager.Application.IoC;
using SalonManager.Domain.Entities;
using SalonManager.Infra.Data.Context;
using Serilog;
using Serilog.Events;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRegisterServices();

builder.Services.AddApiVersioning(x =>
{
    x.DefaultApiVersion = new ApiVersion(1, 0);
    x.ReportApiVersions = true;
    x.AssumeDefaultVersionWhenUnspecified= true;
});

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

app.MapHealthChecks("/health", new HealthCheckOptions
{
    ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse
}); //HealthCheck

app.MapHealthChecksUI(); //HealthChec

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
