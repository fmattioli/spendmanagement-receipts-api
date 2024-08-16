using MMLib.SwaggerForOcelot.DependencyInjection;
using Ocelot.DependencyInjection;
using Ocelot.Middleware;
using Ocelot.Provider.Polly;
using Serilog;
using SpendManagement.Receipts.Api.Extensions;
using SpendManagement.Receipts.Api.Models;

var builder = WebApplication.CreateBuilder(args);
var routes = "Routes";
var enviroment = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");

builder.Configuration
    .AddOcelotWithSwaggerSupport(options =>
    {
        options.Folder = routes;
    })
    .AddJsonFile("conf/appsettings.json", false, reloadOnChange: true)
    .AddJsonFile($"conf/appsettings.{enviroment}.json", true, reloadOnChange: true);

var applicationSettings = builder.Configuration.GetSection("Settings").Get<Settings>()!;

builder.Services
    .AddSwaggerForOcelot(builder.Configuration)
    .AddHealthCheckers(applicationSettings!)
    .AddOcelot(builder.Configuration)
    .AddPolly();

builder.Configuration
    .SetBasePath(Directory.GetCurrentDirectory())
    .AddOcelot(routes, builder.Environment)
    .AddEnvironmentVariables();

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddOpenTelemetry(applicationSettings.MltConfigsSettings);

builder.Host.UseSerilog();

var app = builder.Build();

app.UseHealthChecks();
app.UseSwagger()
   .UseAuthorization()
   .UseSwaggerForOcelotUI(options =>
   {
        options.PathToSwaggerGenerator = "/swagger/docs";
   });

app.MapGet("/", context =>
{
    context.Response.Redirect("/swagger");
    return Task.CompletedTask;
});

await app.UseOcelot();
await app.RunAsync();