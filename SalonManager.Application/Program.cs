using HealthChecks.UI.Client;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SalonManager.Application.IoC;
using SalonManager.Infra.Data.Context;
using Serilog;
using Serilog.Events;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRegisterServices();

builder.Services.AddDbContext<SalonManagerDbContext>(options =>
            options.UseNpgsql(builder.Configuration.GetConnectionString("Database")));

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


builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


var app = builder.Build();

// Configure the HTTP request pipeline.
//if (app.Environment.IsDevelopment())
//{

//}

app.UseSwagger();
app.UseSwaggerUI
( 
    c => {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "SalonManager.API");
        c.DocExpansion(Swashbuckle.AspNetCore.SwaggerUI.DocExpansion.None);
        }
);

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
