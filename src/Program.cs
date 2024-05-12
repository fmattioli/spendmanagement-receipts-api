using MMLib.SwaggerForOcelot.DependencyInjection;
using Ocelot.DependencyInjection;
using Ocelot.Middleware;
using Ocelot.Provider.Polly;
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

var applicationSettings = builder.Configuration.GetSection("Settings").Get<Settings>();

builder.Services
    .AddHealthCheckers(applicationSettings)
    .AddOcelot(builder.Configuration)
    .AddPolly();

builder.Services
    .AddSwaggerForOcelot(builder.Configuration);

builder.Configuration
    .SetBasePath(Directory.GetCurrentDirectory())
    .AddOcelot(routes, builder.Environment)
    .AddEnvironmentVariables();

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

app.UseHealthChecks();
app.UseSwagger()
   .UseAuthorization()
   .UseSwaggerForOcelotUI(options =>
   {
        options.PathToSwaggerGenerator = "/swagger/docs";
   });

app.UseOcelot().Wait();
app.MapControllers();
app.Run();