using HealthChecks.UI.Client;
using Mastery.Api.Middleware;
using Mastery.Common.Presentation;
using Mastery.Common.Presentation.Endpoints;
using Mastery.Common.Presentation.OpenApi;
using Mastery.Modules.Identity.Infrastructure;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Serilog;

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

builder.Host.UseSerilog((context, loggerConfiguration) =>
    loggerConfiguration.ReadFrom.Configuration(context.Configuration));

builder.Services.AddExceptionHandler<GlobalExceptionHandler>();
builder.Services.AddProblemDetails();

builder.Configuration.AddModulesConfiguration("identity");

builder.Services.AddOpenApiPreConfigured();

string databaseConnectionString = builder.Configuration.GetConnectionString("Database")!;
string redisConnectionString = builder.Configuration.GetConnectionString("Cache")!;

builder.Services.AddCommonApplication(
    Mastery.Modules.Identity.Application.AssemblyReference.Assembly);
builder.Services.AddCommonInfrastructure(databaseConnectionString, redisConnectionString);
builder.Services.AddCommonPresentation();

builder.Services.AddIdentityModule(databaseConnectionString);

builder.Services.AddHealthChecks()
    .AddNpgSql(databaseConnectionString)
    .AddRedis(redisConnectionString);

WebApplication app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApiPreConfigured();

    app.ApplyMigrations();
    await app.SeedDataAsync();
}

app.MapHealthChecks("health", new HealthCheckOptions { ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse });
app.UseLogContext();
app.UseSerilogRequestLogging();
app.UseExceptionHandler();

app.UseAuthentication();
app.UseAuthorization();

app.MapEndpoints();

await app.RunAsync();
